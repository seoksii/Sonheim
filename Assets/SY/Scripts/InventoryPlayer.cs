using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPlayer : MonoBehaviour
{
    [SerializeField] Sprite apple;
    [SerializeField] Sprite sword;
    ItemData_SY item_apple;
    ItemData_SY item_sword;
    void Start()
    {
        item_apple = new ItemData_SY();
        item_apple.icon = apple;
        item_apple.isStackable = true;
        item_apple.maxStackAmount = 3;
        item_apple.displayName = "사과";
        item_apple.description = "잘익은 사과";
        item_apple.itemType = ITEMTYPE_SY.CONSUMABLE;

        item_sword = new ItemData_SY();
        item_sword.icon = sword;
        item_sword.isStackable = false;
        item_sword.maxStackAmount = 1;
        item_sword.displayName = "단검";
        item_sword.description = "초보자도 사용할 수 있는 단검";
        item_sword.itemType = ITEMTYPE_SY.EQUIPMENT;
    }

    public void OnClickInventoryToggleButton()
    {
        Inventory.instance.Toggle();
    }

    public void InputApple()
    {
        Inventory.instance.AddItem(item_apple);
    }
    public void InputSword()
    {
        Inventory.instance.AddItem(item_sword);
    }
}
