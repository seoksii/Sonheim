using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CraftRecipe", menuName = "ScriptableObject/CraftRecipe", order = 0)]
public class CraftRecipe : ScriptableObject
{
    [SerializeField] public ItemSlot[] requiredItems;
    [SerializeField] public ItemSlot resultItem;

    public CraftRecipe()
    {
        requiredItems = new ItemSlot[4];
        for (int i = 0; i < requiredItems.Length; i++)
        {
            requiredItems[i] = new ItemSlot();
        }
        resultItem = new ItemSlot();
    }
}