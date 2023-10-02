using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData item;


    public void OnInteract()
    {
        // Inventory.instance.AddItem(item);
        Destroy(gameObject);
    }
}
