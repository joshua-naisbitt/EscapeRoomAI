using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
public static InventoryManager Instance;
public List<Item> Items = new List<Item>();

public Transform itemContent;
public GameObject inventoryItem;

private void Awake(){
    Instance = this;
}

public void Add(Item item){ //adding new items call me 
    Items.Add(item);
}


public void Remove(Item item){  // removing for any reason call me 
Items.Remove(item);
}

public void ListItems(){
    //clean content before open
    foreach( Transform item in itemContent){
        Destroy(item.gameObject);
    }

    foreach(var item in Items){
        GameObject obj = Instantiate(inventoryItem, itemContent);
        var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
        var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

        itemName.text = item.itemName;
        itemIcon.sprite = item.icon;
    }
}
}


