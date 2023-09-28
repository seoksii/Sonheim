using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public enum RecipeState { Craftable, EnoughInInventory, NotEnough}
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
    public RecipeState state;

    private void Awake()
    {
        background = GetComponent<Image>();
        button = GetComponent<Button>();
    }


    public void Set(ItemSlot[] _inventory, ItemSlot[] _craftBox, CraftRecipe newRecipe)
    {
        curRecipe = newRecipe;
        
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
        SetState(_inventory,_craftBox,newRecipe);
    }
    public void Clear()
    {
        curRecipe = null;

        for (int i = 0; i < requiredItemSlotui.Length;i++)
        {
            requiredItemSlotui[i].Clear();
            requiredItemSlotui[i].gameObject.SetActive(false);
        }
        isEmpty = true;
    }

    public void OnClickButton()
    {

    }

    public void SetState(ItemSlot[] _inventory, ItemSlot[] _craftBox, CraftRecipe _recipe)
    {
        
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
