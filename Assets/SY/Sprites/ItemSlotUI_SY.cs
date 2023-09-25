using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI_SY : MonoBehaviour
{
    public Button button;
    public Image icon;
    public TextMeshProUGUI quantityText;
    private ItemSlot_SY curSlot;
    private Outline outline;

    public int index;
    public bool isEquipped;
    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        outline.enabled = isEquipped;
    }

    public void Set(ItemSlot_SY slot)
    {
        curSlot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;
        quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;

        if ( outline != null )
        {
            outline.enabled = isEquipped;
        }
    }

    public void Clear()
    {
        curSlot = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    public void OnButtonClick()
    {
        Inventory_SY.instance.SelectItem(index);
    }
}
