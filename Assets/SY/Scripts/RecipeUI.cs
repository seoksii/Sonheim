using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public enum CraftState { Craftable, EnoughInInventory, NotEnough }
public class RecipeUI : MonoBehaviour
{
    public CraftRecipe curRecipe;

    public ItemSlotUI[] requiredItemSlotui;
    public ItemSlotUI resultItemSlotui;

    [Header("Recipe")]
    public Image background;
    public bool isReady;
    public int myindex;
    public Button button;

    public bool isEmpty;
    public CraftState curRecipeState;

    private void Awake()
    {
        background = GetComponent<Image>();
        button = GetComponent<Button>();
    }


    public void Set(ItemSlot[] _inventory, ItemSlot[] _craftBox, CraftRecipe newRecipe)
    {
        curRecipe = newRecipe;
        gameObject.SetActive(true);
        for (int i = 0; i < requiredItemSlotui.Length; i++)
        {
            if (newRecipe.requiredItems[i] != null)
            {
                requiredItemSlotui[i].Set(newRecipe.requiredItems[i]);
                requiredItemSlotui[i].gameObject.SetActive(true);
            }
            else
            {
                requiredItemSlotui[i].Clear();
                requiredItemSlotui[i].gameObject.SetActive(false);
            }
        }
        curRecipeState = SetState(_inventory, _craftBox, newRecipe);
    }
    public void Clear()
    {
        curRecipe = null;
        curRecipeState = CraftState.NotEnough;
        SetColor();
        SetButton();

        for (int i = 0; i < requiredItemSlotui.Length; i++)
        {
            requiredItemSlotui[i].Clear();
            requiredItemSlotui[i].gameObject.SetActive(false);
        }
        isEmpty = true;
        gameObject.SetActive(false);
    }

    public void OnClickButton()
    {
        CraftPanelUI.instance.OnClickRecipe(curRecipe);
    }

    public CraftState SetState(ItemSlot[] _inventory, ItemSlot[] _craftBox, CraftRecipe _recipe)
    {
        CraftState state = CraftState.Craftable;
        int need;
        int total_Inventory;
        int total_CraftBox;

        for (int i = 0; i < _recipe.requiredItems.Length; i++)
        {
            ItemSlot requireItem = _recipe.requiredItems[i];
            need = requireItem.quantity;
            total_Inventory = 0;
            total_CraftBox = 0;

            for (int j = 0; j < _craftBox.Length; j++)
            {
                if (requireItem.item == _craftBox[j].item)
                {
                    total_CraftBox += _craftBox[j].quantity;
                }
            }
            
            for (int j = 0; j < _inventory.Length; j++)
            {
                if (requireItem.item == _inventory[j].item)
                {
                    total_Inventory += _inventory[j].quantity;
                }
            }

            if (need > total_CraftBox && state == CraftState.Craftable)
            {
                state = CraftState.EnoughInInventory;
            }

            if (need > total_Inventory + total_CraftBox)
            {
                state = CraftState.NotEnough;
                break;
            }
        }
        SetColor();
        SetButton();
        return state;
    }

    public void SetColor()
    {
        if (curRecipeState == CraftState.Craftable) background.color = Color.white;
        if (curRecipeState == CraftState.EnoughInInventory) background.color = Color.yellow;
        if (curRecipeState == CraftState.NotEnough) background.color = Color.grey;
    }

    public void SetButton()
    {
        if (curRecipeState == CraftState.Craftable) button.interactable = true;
        if (curRecipeState == CraftState.EnoughInInventory) button.interactable = true;
        if (curRecipeState == CraftState.NotEnough) button.interactable = false;
    }
    //public int myindex;
    //public Button button;
    //private Outline outline;


    //private void Awake()
    //{
    //    background = GetComponent<Image>();
    //    outline = GetComponent<Outline>();
    //}


    //public void Set(ItemSlot slot)
    //{
    //    curSlot = slot;
    //    icon.gameObject.SetActive(true);
    //    icon.sprite = slot.item.Icon;
    //    quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;

    //    if (curSlot.isEquipped) { background.color = Color.yellow; }
    //    else { background.color = Color.white; }
    //    if (outline != null) { outline.enabled = curSlot.isEquipped; }
    //}

    //public void Clear()
    //{
    //    curSlot = null;
    //    icon.gameObject.SetActive(false);
    //    quantityText.text = string.Empty;

    //    background.color = Color.white;
    //    if (outline != null) { outline.enabled = false; }
    //}

    //public void OnButtonClick()
    //{
    //    Inventory.instance.SelectItem(myindex);
    //}
}
