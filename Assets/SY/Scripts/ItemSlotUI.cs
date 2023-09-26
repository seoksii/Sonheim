using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Button button;
    public Image icon;
    public TextMeshProUGUI quantityText;
    private ItemSlot curSlot;
    private Outline outline;

    public int myindex;
    public bool isEquipped;
    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        outline.enabled = isEquipped;
    }

    public void Set(ItemSlot slot)
    {
        Debug.Log(myindex);
        curSlot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.Icon;
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
        int i = myindex;
        Debug.Log("slot selected : " + i.ToString());
        Inventory.instance.SelectItem(myindex);
    }
}
