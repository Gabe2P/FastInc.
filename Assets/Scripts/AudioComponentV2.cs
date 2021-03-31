//Written By Gabriel Tupy 3-15-2021

//A large portion of this is based off of the code provide by Serj-TM on StackOverflow at this link; That section of Code I do not claim as my own.
//https://stackoverflow.com/questions/9753366/subscribing-an-action-to-any-event-type-via-reflection

using System;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using UnityEngine;

public class AudioComponentV2 : MonoBehaviour
{
    [Serializable]
    public class AudioContainer
    {
        public string eventName = null;
        public SoundType sound = null;
        public bool PlayasOneShot = true;
        private Tuple<object, Delegate> eventHandler = null;

        public void SetEventHandler(Tuple<object, Delegate> eventHandler)
        {
            this.eventHandler = eventHandler;
        }

        public Tuple<object, Delegate> GetEventHandler()
        {
            return eventHandler;
        }

        public void PlaySound()
        {
            if (PlayasOneShot)
            {
                sound.PlayOneShot();
            }
            else
            {
                sound.Play();
            }
        }
    }

    public GameObject ReferenceGameObject = null;
    private INeedAudio reference;
    public List<AudioContainer> AudioContainers = new List<AudioContainer>();

    private void Awake()
    {
        reference = ReferenceGameObject.GetComponent<INeedAudio>();
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        object o = reference.GetObject();
        Type t = o.GetType();

        foreach (AudioContainer ac in AudioContainers)
        {
            Action action = () => ac.PlaySound();
            EventInfo eInfo = t.GetEvent(ac.eventName);
            AddActionEventHandler(eInfo, o, action);
        }
    }

    private void OnDisable()
    {
        object o = reference.GetObject();
        Type t = o.GetType();
        foreach (AudioContainer ac in AudioContainers)
        {
            EventInfo eInfo = t.GetEvent(ac.eventName);
            if (ac.GetEventHandler() != null)
            {
                eInfo.RemoveEventHandler(ac.GetEventHandler().Item1, ac.GetEventHandler().Item2);
            }
        }
    }

    public void PlayEventSound(string eventName)
    {
        AudioContainer ac = (from AudioContainer in AudioContainers where AudioContainer.eventName == eventName select AudioContainer) as AudioContainer;
        if (ac != null)
        {
            ac.PlaySound();
        }    
    }

    //The below code is provided by Serj-Tm from the link provided above. This is how we where able to subscribe to the event and play the sound.
    static Tuple<object, Delegate> AddActionEventHandler(EventInfo eventInfo, object item, Action action)
    {
        var parameters = eventInfo.EventHandlerType.GetMethod("Invoke").GetParameters().Select(parameter => Expression.Parameter(parameter.ParameterType)).ToArray();
        var handler = Expression.Lambda(eventInfo.EventHandlerType, Expression.Call(Expression.Constant(action), "Invoke", Type.EmptyTypes), parameters).Compile();
        eventInfo.AddEventHandler(item, handler);
        return new Tuple<object, Delegate>(item, handler);
    }

    static Tuple<object, Delegate> AddActionEventHandler(EventInfo eventInfo, object item, Action<object, EventArgs> action)
    {
        var parameters = eventInfo.EventHandlerType.GetMethod("Invoke").GetParameters().Select(parameter => Expression.Parameter(parameter.ParameterType)).ToArray();
        var invoke = action.GetType().GetMethod("Invoke");
        var handler = Expression.Lambda(eventInfo.EventHandlerType, Expression.Call(Expression.Constant(action), invoke, parameters[0], parameters[1]), parameters).Compile();
        eventInfo.AddEventHandler(item, handler);
        return new Tuple<object, Delegate>(item, handler);
    }
}
