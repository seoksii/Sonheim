using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CraftPanelUI : MonoBehaviour
{
    public static CraftPanelUI instance;

    public CraftRecipe[] craftRecipes;
    public Inventory userInventory;

    public GameObject recipePrefab;

    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;

    public GameObject addButton;
    public GameObject removeButton;

    // 조합대의 고유 아이템 공간임, 여기에 넣는 순간 플레이어 인벤토리에서 차감됨
    [Header("current inputted items in crafting box")]
    public ItemSlot[] craftingInputBox;

    [Header("need to connect")]
    public ItemSlotUI[] resourcesUI;
    public ItemSlotUI[] inputsUI;
    public ItemSlotUI[] outputUI;

    public Image craftBoxBackground;

    public ItemSlot selectedItem;
    public int selectedItemIndex;

    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;


    private void Awake()
    {
        instance = this;

        craftingInputBox = new ItemSlot[4];
        for(int i = 0; i < 4; i++)
        {
            craftingInputBox[i] = new ItemSlot();
        }
    }

    public void Init(Inventory _inventory)
    {
        userInventory = _inventory;
        int index = 0;
        for (int i = 0; i < _inventory.slots.Length; i++)
        {
            if(_inventory.slots[i].item.Type == ItemType.Resource)
            {
                resourcesUI[index].Set(_inventory.slots[i]);
                resourcesUI[index].myindex = index;
                index++;
            }
        }
        UpdateUI();
        gameObject.SetActive(false);
        ClearSelectedItemWindow();
        MakeDictionaries();
    }

    public void Toggle()
    {
        int index = 0;
        for (int i = 0; i < userInventory.slots.Length; i++)
        {
            if (userInventory.slots[i].item != null)
            {
                if (userInventory.slots[i].item.Type == ItemType.Resource)
                {
                    resourcesUI[index].Set(userInventory.slots[i]);
                    resourcesUI[index].myindex = index;
                    index++;
                }
            }
        }
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            onCloseInventory?.Invoke();

        }
        else
        {
            gameObject.SetActive(true);
            onOpenInventory?.Invoke();
        }
        UpdateUI();
    }

    public bool IsOpen()
    {
        return gameObject.activeInHierarchy;
    }

    public void SelectItem(int index)
    {
        if (resourcesUI[index].curSlot.item == null) return;

        selectedItem = resourcesUI[index].curSlot;
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.item.DisplayName;
        selectedItemDescription.text = selectedItem.item.Description;

        addButton.SetActive(true);
        removeButton.SetActive(true);
    }

    public bool AddItem(ItemData item)
    {
        if (item.IsStackable)
        {
            ItemSlot slotToStackTo = GetItemStack(item);
            if (slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                UpdateUI();
                return true;
            }
        }
        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            UpdateUI();
            return true;
        }
        return false;
    }

    public void UpdateUI()
    {
        for (int i = 0; i < resourcesUI.Length; i++)
        {
            if (resourcesUI[i].curSlot == null) continue;
            if (resourcesUI[i].curSlot.item != null)
            {
                resourcesUI[i].Set(resourcesUI[i].curSlot);
                resourcesUI[i].gameObject.SetActive(true);
            }
            else
            {
                resourcesUI[i].Clear();
                resourcesUI[i].gameObject.SetActive(false);
            }
        }
    }

    public ItemSlot GetItemStack(ItemData item)
    {
        for (int i = 0; i < resourcesUI.Length; i++)
        {
            if (resourcesUI[i].curSlot.item == item && resourcesUI[i].curSlot.quantity < item.MaxStackAmount)
                return resourcesUI[i].curSlot;
        }
        return null;
    }

    public ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < resourcesUI.Length; i++)
        {
            if (resourcesUI[i].curSlot.item == null)
            {
                return resourcesUI[i].curSlot;
            }
        }
        return null;
    }

    public void ClearSelectedItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        addButton.SetActive(false);
        removeButton.SetActive(false);
    }

    public void RemoveSelectedItem()
    {
        selectedItem.quantity--;
        if (selectedItem.quantity <= 0)
        {
            selectedItem.item = null;
            ClearSelectedItemWindow();
        }
        UpdateUI();
    }
    public bool HasItems(ItemData item, int quantity)
    {
        return false;
    }

    public void OnClickAddButton()
    {

    }

    public void OnClickRemoveButton()
    {

    }

    public void MakeDictionaries()
    {

    }
}
