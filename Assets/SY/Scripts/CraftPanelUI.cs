using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;

    public GameObject addButton;
    public GameObject removeButton;

    // 조합대의 고유 아이템 공간임, 여기에 넣는 순간 플레이어 인벤토리에서 차감됨
    [Header("current inputted items in crafting box")]
    public ItemSlot[] craftBox;
    public CraftState craftBoxState;

    [Header("need to connect")]
    public ItemSlotUI[] resourcesUI;
    public ItemSlotUI[] craftBoxUI;
    public ItemSlotUI craftBoxResultUI;

    public Image craftBoxBackground;

    public ItemSlot selectedItem;
    public int selectedItemIndex;

    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;

    public Dictionary<ItemData, List<CraftRecipe>> recipeDictionary;
    public List<CraftRecipe> curRecipesList;

    public RecipeUI[] curRecipesUI;

    private void Awake()
    {
        instance = this;
        craftBox = new ItemSlot[craftBoxUI.Length];
        for(int i = 0; i < craftBoxUI.Length; i++)
        {
            craftBox[i] = new ItemSlot();
            craftBoxUI[i].myindex = i + resourcesUI.Length;
        }
        for(int i = 0; i < resourcesUI.Length; i++)
        {
            resourcesUI[i].myindex = i;
        }
        recipeDictionary = new Dictionary<ItemData, List<CraftRecipe>>();
        MakeDictionaries();
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
            }
        }
        UpdateResourcesUI();
        gameObject.SetActive(false);
        ClearSelectedItemWindow();
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
                    index++;
                }
            }
        }
        for (int i = 0; i < craftBoxUI.Length; i++)
        {
            if (craftBox[i].item != null)
            {
                craftBoxUI[i].Set(craftBox[i]);
            }
            else
            {
                craftBoxUI[i].Clear();
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
        UpdateResourcesUI();
        Debug.Log("여기 호출돼?");
        ResetCurRecipes();
    }

    public bool HasItem(ItemData item)
    {
        for (int i = 0; i < craftBox.Length; i++)
        { if (craftBox[i].item == item && craftBox[i].quantity > 0) return true; }
        return false;
    }
    public bool IsOpen()
    {
        return gameObject.activeInHierarchy;
    }

    public void SelectItem(int index)
    {
        if (index >= resourcesUI.Length)
        {
            if (craftBox[index-resourcesUI.Length] == null) return;
            selectedItem = craftBox[index-resourcesUI.Length];
            selectedItemIndex = index;

            selectedItemName.text = selectedItem.item.DisplayName;
            selectedItemDescription.text = selectedItem.item.Description;

            if(Inventory.instance.HasItem(selectedItem.item)) addButton.SetActive(true);
            if(instance.HasItem(selectedItem.item)) removeButton.SetActive(true);
        }
        else
        {
            if (resourcesUI[index].curSlot.item == null) return;

            selectedItem = resourcesUI[index].curSlot;
            selectedItemIndex = index;

            selectedItemName.text = selectedItem.item.DisplayName;
            selectedItemDescription.text = selectedItem.item.Description;

            if (Inventory.instance.HasItem(selectedItem.item)) addButton.SetActive(true);
            if (instance.HasItem(selectedItem.item)) removeButton.SetActive(true);
        }
    }

    public bool AddItemToCraftBox(ItemData item)
    {
        if (item.IsStackable)
        {
            ItemSlot slotToStackTo = GetItemStackInCraftBox(item);
            if (slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                UpdateResourcesUI();
                UpdateCraftBoxUI();
                return true;
            }
        }
        ItemSlot emptySlot = GetEmptySlotInCraftBox();

        if (emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            UpdateResourcesUI();
            UpdateCraftBoxUI();
            return true;
        }
        return false;
    }

    public void UpdateResourcesUI()
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
    public void UpdateCraftBoxUI()
    {
        for (int i = 0; i < craftBoxUI.Length; i++)
        {
            if (craftBox[i].item != null)
            {
                craftBoxUI[i].Set(craftBox[i]);
            }
            else
            {
                craftBoxUI[i].Clear();
            }
        }
    }
    public ItemSlot GetItemStackInCraftBox(ItemData item)
    {
        for (int i = 0; i < craftBoxUI.Length; i++)
        {
            if (craftBox[i] == null)
            {
                if (craftBox[i].item == item && craftBox[i].quantity < item.MaxStackAmount)
                    return craftBox[i];
            }
        }
        return null;
    }

    public ItemSlot GetEmptySlotInCraftBox()
    {
        for (int i = 0; i < craftBoxUI.Length; i++)
        {
            if (craftBox[i].item == null)
            {
                return craftBox[i];
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
        UpdateResourcesUI();
        UpdateCraftBoxUI();
    }
    public bool HasItems(ItemData item, int quantity)
    {
        return false;
    }

    public void OnClickAddButton()
    {
        if (Inventory.instance.RemoveItem(selectedItem.item))
        {
            AddItemToCraftBox(selectedItem.item);
            ResetCurRecipes();
            ResetCurRecipesUI();
        }
        UpdateCraftBoxUI();
        UpdateResourcesUI();
    }

    public void OnClickRemoveButton()
    {
        if (RemoveItemInCraftBox(selectedItem.item))
        {
            Inventory.instance.AddItem(selectedItem.item);
            ResetCurRecipes();
            ResetCurRecipesUI();
        }
        UpdateCraftBoxUI();
        UpdateResourcesUI();
    }


    public void MakeDictionaries()
    {
        for (int i=0; i < craftRecipes.Length; i++)
        {
            for (int j=0; j < craftRecipes[i].requiredItems.Length; j++)
            {
                if (craftRecipes[i].requiredItems[j].item != null)
                {
                    ItemData itemData = craftRecipes[i].requiredItems[j].item;
                    if (recipeDictionary.ContainsKey(itemData))
                    {
                        if (recipeDictionary[itemData].Contains(craftRecipes[i]))
                        {
                            continue;
                        }
                        else
                        {
                            recipeDictionary[itemData].Add(craftRecipes[i]);
                        }
                    }
                    else
                    {
                        List<CraftRecipe> newList = new List<CraftRecipe>();
                        newList.Add(craftRecipes[i]);
                        recipeDictionary.Add(itemData, newList);
                    }
                }
            }
        }
    }

    public void OnClickRecipe(CraftRecipe _recipe)
    {

    }

    public void SetCraftBoxState(ItemSlot resultItem)
    {
        if ( craftBoxState == CraftState.NotEnough )
        {
            craftBoxBackground.color = Color.white;
            craftBoxResultUI.Set(resultItem);
        }
    }

    public void ResetCurRecipesUI()
    {
        Debug.Log("여긴 리셋 컬 레시피유아이");
        CraftRecipe[] temp = curRecipesList.ToArray();
        for (int i=0; i < curRecipesUI.Length; i++)
        {
            if (temp.Length > i + 1)
            {
                if (temp[i] != null)
                {
                    curRecipesUI[i].Set(userInventory.slots, craftBox, temp[i]);
                }
                else
                {
                    curRecipesUI[i].Clear();
                }
            }  
            else
            {
                curRecipesUI[i].Clear();
            }
        }
    }

    public void ResetCurRecipes()
    {
        Debug.Log("여긴 리셋 컬 레시피");
        curRecipesList.Clear();
        for (int i = 0; i < craftBox.Length; i++)
        {
            if (craftBox[i].item != null)
            {
                curRecipesList = curRecipesList.Intersect(recipeDictionary[craftBox[i].item]).ToList();
            }
        }
        ResetCurRecipesUI();
    }

    public bool RemoveItemInCraftBox(ItemData item)
    {
        ItemSlot temp = FindSlotInCraftBox(item);
        if (temp != null && temp.quantity > 0) 
        {
            temp.quantity--;
            if (temp.quantity <= 0)
            {
                temp.item = null;
                ClearSelectedItemWindow();
            }
            UpdateResourcesUI();
            UpdateCraftBoxUI();
            return true;
        }
        return false;
    }

    public ItemSlot FindSlotInCraftBox(ItemData item)
    {
        for (int i = 0; i < craftBox.Length; i++)
        {
            if (craftBox[i].item == item && craftBox[i].quantity > 0) return craftBox[i];
        }
        return null;
    }
}
