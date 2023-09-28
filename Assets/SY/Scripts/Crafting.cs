using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public CraftPanelUI craftPanel;
    
    private Inventory inventory;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();    
    }

    private void Start()
    {
        craftPanel.Init(inventory);
    }
}
