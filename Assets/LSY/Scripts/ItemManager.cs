using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public void DropNewItem(Vector3 position, ItemData itemData)
    {
        Instantiate(itemData.DropPrefab, position, Random.rotation);
    }

    // public void DropFromInventory(Vector3 position, GameObject item)
    // {
    //     // position : 애니메이션이면 손 위치, 아니면 플레이어의 앞에
    // }
}
