using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TattooRandomizer : MonoBehaviour
{
    [SerializeField] private TattooType curTattoo = null;
    private GameObject prefabInstance = null;
    public List<TattooType> tattoos = new List<TattooType>();

    private void Awake()
    {
        RandomizeTattoo();
    }

    public void RandomizeTattoo()
    {
        if (tattoos.Count > 0)
        {
            curTattoo = tattoos[Random.Range(0, tattoos.Count - 1)];
            if (prefabInstance != null)
            {
                Destroy(prefabInstance);
            }
            prefabInstance = Instantiate(curTattoo.GetPrefab(), transform.position, transform.rotation);
        }
    }

}
