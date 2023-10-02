using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager _instance;

    void Awake()
    {
        _instance = this;
    }
    
    public void DropNewItem(Vector3 position, ItemData itemData)
    {
        Instantiate(itemData.DropPrefab, position, Random.rotation);
    }
}
