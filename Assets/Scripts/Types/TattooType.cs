//Written By Gabriel Tupy3-8-2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newTattoo", menuName = "Type/Tattoo")]
public class TattooType : ScriptableObject
{
    public enum OutlineColor { Black, Gray, White }

    [SerializeField] private Sprite sprite = null;
    [SerializeField] private OutlineColor RequiredColor = OutlineColor.Black;
    [SerializeField] private bool isHairy = false;
    [SerializeField] private DifficultyEnum.Difficulty difficulty = DifficultyEnum.Difficulty.Easy;
    [SerializeField] private GameObject prefab = null;

    public Sprite GetSprite()
    {
        return sprite;
    }

    public OutlineColor GetRequiredColor()
    {
        return RequiredColor;
    }

    public DifficultyEnum.Difficulty GetDifficulty()
    {
        return difficulty;
    }

    public bool NeedsShaving()
    {
        return isHairy;
    }

    public GameObject GetPrefab()
    {
        return prefab;
    }
}
