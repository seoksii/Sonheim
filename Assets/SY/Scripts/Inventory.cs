using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public enum ITEMTYPE_SY { CONSUMABLE, EQUIPMENT, }
public class ItemSlot
{
    public ItemData_SY item;
    public int quantity;
}


public class ItemData_SY
{
    public Sprite icon;
    public bool isStackable;
    public GameObject dropPrefab;
    public int maxStackAmount;
    public string displayName;
    public string description;

    public ITEMTYPE_SY itemType;
}

public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform dropPosition;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatNames;
    public TextMeshProUGUI selectedItemStatValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unequipButton;
    public GameObject dropButton;

    private int curEquipIndex;

    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;

    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventoryWindow.SetActive(false);
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
        if (inventoryWindow.activeInHierarchy)
        {
            inventoryWindow.SetActive(false);
            onCloseInventory?.Invoke();
            
        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpenInventory?.Invoke();
        }
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem(ItemData_SY item)
    {
        if (item.isStackable)
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

    public void ThrowItem(ItemData_SY item)
    {
        // Instantiate(item.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360f));
        Debug.Log("발사" + item.displayName);
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

    public ItemSlot GetItemStack(ItemData_SY item)
    {
        for( int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item && slots[i].quantity < item.maxStackAmount)
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

        selectedItemName.text = selectedItem.item.displayName;
        selectedItemDescription.text = selectedItem.item.description;

        selectedItemStatNames.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        //for( int i = 0; i < selectedItem)

        useButton.SetActive(selectedItem.item.itemType == ITEMTYPE_SY.CONSUMABLE);
        equipButton.SetActive(selectedItem.item.itemType == ITEMTYPE_SY.EQUIPMENT && !uiSlots[index].isEquipped);
        unequipButton.SetActive(selectedItem.item.itemType == ITEMTYPE_SY.EQUIPMENT && uiSlots[index].isEquipped);
        dropButton.SetActive(true);
    }
    public void ClearSelectedItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        selectedItemStatNames.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unequipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void OnUseButton()
    {

    }
    public void OnEquipButton()
    {

    }
    public void OnUnequipButton()
    {

    }

    public void OnDropButton()
    {
        ThrowItem(selectedItem.item);
        RemoveSelectedItem(selectedItem.item);
    }

    public void RemoveSelectedItem(ItemData_SY item)
    {
        selectedItem.quantity--;
        if(selectedItem.quantity <= 0)
        {
            if (uiSlots[selectedItemIndex].isEquipped)
            {
                Unequip(selectedItemIndex);
            }

            selectedItem.item = null;
            ClearSelectedItemWindow();
        }
    }
    public bool HasItems(ItemData_SY item, int quantity)
    {
        return false;
    }

    public void Unequip(int index)
    {

    }
}
