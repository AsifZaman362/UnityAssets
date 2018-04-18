using System.Collections;
using UnityEngine;
using TMPro;


public struct Item{
    public string name;
    public int amount;
}


public class Inventory : MonoBehaviour{
    
    [Header("Inventory Properties")]
    [SerializeField] private int capacity;

    [Header("Inventory UI Objects")]
    [SerializeField] private TextMeshProUGUI[] ItemUIObjects;


    private ArrayList items;
    private int selectedItem = 0;

    void Start(){
        items = new ArrayList();
    }

    public void OpenInventory(){

    }

    public void CloseInventory(){
        
    }

    public void AddItem(string _name, int _amount){
        int existing_item_pos = GetItemPos(_name);
        if(existing_item_pos!=-1){
            Item existing_item = (Item) items[existing_item_pos];
            existing_item.amount += _amount;
        }
        else{
            if(items.Capacity<capacity){
                items.Add(new Item{name = _name, amount = _amount});
            }
        }
    }

    public void RemoveItem(string name = "null", int pos = -1){
        if(name=="null"){
            if(pos!=-1){
                items.RemoveAt(pos);
            }
        }
        else{
            items.RemoveAt(GetItemPos(name));
        }
    }

    public Item GetItem(string name){
        foreach(Item item in items){
            if(item.name == name){
                return item;
            }
        }
        return new Item{name = "null", amount = -1};
    }

    public int GetItemPos(string name){
        int i = 0;
        foreach(Item item in items){
            if(item.name == name){
                return i;
            }
            i++;
        }
        return -1;
    }

}