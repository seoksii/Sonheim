using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public class ItemSlot
{
    public ItemData item;
    [HideInInspector]
    public bool isEquipped;
    public int quantity;

    public ItemSlot()
    {
        isEquipped = false;
    }
}


public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;
    public ItemSlot[] equips;

    public GameObject inventoryPanel;
    public Transform dropPosition;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatNames;
    public TextMeshProUGUI selectedItemStatValues;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unequipButton;
    public GameObject dropButton;

    private int curEquipIndex;
    private Player player;

    private bool isExistEquipInventory;
    private EquipInventory equipInventory;

    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;

    public static Inventory instance;

    private void Awake()
    {
        instance = this;
        player = GetComponent<Player>();
        if(TryGetComponent(out EquipInventory equipInventory))
        {
            isExistEquipInventory = true;
        }
        else
        {
            equips = new ItemSlot[3];
        }
    }

    private void Start()
    {
        inventoryPanel.SetActive(false);
        slots = new ItemSlot[uiSlots.Length];

        for(int i= 0; i <slots.Length ; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].myindex = i;
            Debug.Log(uiSlots[i].myindex.ToString());
            uiSlots[i].Clear();
        }

        ClearSelectedItemWindow();
        
    }

    //public void OnInventoryButton(InputAction.CallbackContext callbackContext)
    //{
    //    if(callbackContext.phase == InputActionPhase.Started)
    //    {
    //        Toggle();
    //    }
    //}
    
    public void Toggle()
    {
        if (inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(false);
            onCloseInventory?.Invoke();
            
        }
        else
        {
            inventoryPanel.SetActive(true);
            onOpenInventory?.Invoke();
        }
    }

    public bool IsOpen()
    {
        return inventoryPanel.activeInHierarchy;
    }

    public void AddItem(ItemData item)
    {
        if (item.IsStackable)
        {
            ItemSlot slotToStackTo = GetItemStack(item);
            if(slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                UpdateUI();
                return;
            }
        }
        ItemSlot emptySlot = GetEmptySlot();
        
        if (emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }
        ThrowItem(item);
    }

    public void ThrowItem(ItemData item)
    {
        // Instantiate(item.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360f));
        Debug.Log("아이템을 던졌다. :  " + item.DisplayName);
    }

    public void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                uiSlots[i].Set(slots[i]);
            }
            else
            {
                uiSlots[i].Clear();
            }
        }
    }

    public ItemSlot GetItemStack(ItemData item)
    {
        for( int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item && slots[i].quantity < item.MaxStackAmount)
                return slots[i];
        }
        return null;
    }

    public ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;

        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.item.DisplayName;
        selectedItemDescription.text = selectedItem.item.Description;

        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;

        if(selectedItem.item.Consumables != null) 
        {
            for (int i = 0; i < selectedItem.item.Consumables.Length; i++)
            {
                selectedItemStatNames.text += selectedItem.item.Consumables[i].Type.ToString() + "\n";
                selectedItemStatValues.text += selectedItem.item.Consumables[i].Value.ToString() + "\n";
            }
        }


        useButton.SetActive(selectedItem.item.Type == ItemType.Consumable);
        equipButton.SetActive(selectedItem.item.Type == ItemType.Equipable && !slots[index].isEquipped);
        unequipButton.SetActive(selectedItem.item.Type == ItemType.Equipable && slots[index].isEquipped);
        dropButton.SetActive(true);
    }
    public void ClearSelectedItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unequipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void OnUseButton()
    {
        //if (selectedItem.item.itemType == ITEMTYPE_SY.CONSUMABLE)
        //{
        //    for( int i= 0; i < selectedItem.item.consumables.Length; i++)
        //    {
        //        switch (selectedItem.item.consumables[i].type)
        //        {
        //            case ConsumableType.Health:
        //                // 플레이어쪽 살펴보기
        //                break;

        //            case ConsumableType.Hunger:
        //                // 플레이어쪽 살펴보기
        //                break;
        //            case ConsumableType.Thirsty:
        //                // 플레이어쪽 살펴보기
        //                break;
        //        }
        //    }
        //}
        RemoveSelectedItem();
    }
    public void OnEquipButton()
    {
        if (!isExistEquipInventory)
        {
            EquipHere();
        }
    }
    public void OnUnequipButton()
    {
        if (!isExistEquipInventory)
        {
            UnequipHere();
        }
    }

    public void OnDropButton()
    {
        ThrowItem(selectedItem.item);
        RemoveSelectedItem();
    }

    public void RemoveSelectedItem()
    {
        selectedItem.quantity--;
        if(selectedItem.quantity <= 0)
        {
            if (slots[selectedItemIndex].isEquipped)
            {
                UnequipHere();
            }
            selectedItem.item = null;
            ClearSelectedItemWindow();
        }
        UpdateUI();
    }
    public bool HasItems(ItemData item, int quantity)
    {
        return false;
    }

    public void UnequipHere()
    {
        selectedItem.isEquipped = false;
        UpdateUI();
        SelectItem(selectedItemIndex);
    }

    public void EquipHere()
    {
        selectedItem.isEquipped = true;
        if (equips[0] != null) equips[0].isEquipped = false;
        equips[0] = selectedItem;
        UpdateUI();
        SelectItem(selectedItemIndex);
    }

}
