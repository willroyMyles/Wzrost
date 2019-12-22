using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    //private Inventory inventory;
    //Transform itemSlotContainer;
    //Transform itemSlotTemplate;

    //private void Awake()
    //{
    //    itemSlotContainer = transform.Find("itemSlotContainer");
    //    itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    //}

    //public void SetInventory(Inventory inventory)
    //{
    //    this.inventory = inventory;
    //    refreshInventory();
    //}

    //public void refreshInventory()
    //{
    //    int x = 0;
    //    int y = 0;
    //    float cellSize = 130;
    //    foreach(var item in inventory.getItemList())
    //    {
    //        RectTransform itemSlotTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
    //        itemSlotTransform.gameObject.SetActive(true);
    //        itemSlotTransform.anchoredPosition = new Vector2(x * cellSize, y * cellSize);
    //        x++;
    //        if(x > 4)
    //        {
    //            x = 0;
    //            y++;
    //        }
    //    }
    //}
}
