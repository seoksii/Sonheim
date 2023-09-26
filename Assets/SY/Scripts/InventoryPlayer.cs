using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPlayer : MonoBehaviour
{
    [SerializeField] Sprite apple;
    [SerializeField] Sprite sword;

    [SerializeField] ItemData[] items;
    void Start()
    {

    }

    public void OnClickInventoryToggleButton()
    {
        Inventory.instance.Toggle();
    }

    public void OnClickButtonAddItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            Inventory.instance.AddItem(items[i]);
        }
    }
}
