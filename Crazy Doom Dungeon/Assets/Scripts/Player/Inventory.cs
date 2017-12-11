using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{

    private InventoryGUI GUI;

    [SerializeField]
    GameObject inventoryItemPefab;

    //  public Image[] itemImages = new Image[numItemSlots];
    public const int numItemSlots = 25;
    private GameObject[] items = new GameObject[numItemSlots];

    public static int Currency = 8;

    private void Start()
    {
        GUI = InventoryGUI.instance;
   //     GUI.SetInventorySize(numItemSlots);
  //      InventoryGUI.ItemAction += useItem;
    }

    public void AddItem(int item)
    {
        DatabaseItem DBItem = ItemLoader.instance.getItem(item);
        //  DatabaseItem item = ItemLoader.instance.getItem(id)
        if (DBItem is DatabaseWeapon)
        {
            EquipmentGUI.instance.AddWeapon(DBItem as DatabaseWeapon);
        }
        else
        {
            GameObject temp = Instantiate(inventoryItemPefab, GUI.transform);
            temp.GetComponent<DragItem>().Item = DBItem;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    items[i] = temp;
                    GUI.AddItem(i, temp);
                    //   itemImages[i].sprite = itemToAdd.sprite;
                    //   itemImages[i].enabled = true;
                    break;
                }
                /*else if(items[i].Id == id && item.stackable)
                {
                    GUI.IncreaseCount(i);
                    break;
                }*/
            }
        }
    }

    public static void ChangeMoneyAmount(int moneyChange)
    {
        Currency += moneyChange;
        InventoryGUI.instance.UpdateCurrencyCounter(Currency);
    }
/*
    public void RemoveItem(DatabaseItem itemToRemove)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemToRemove)
            {
                if (itemToRemove.stackable)
                {
                    GUI.DecreaseCount(i);
                    if(GUI.getCount(i) <= 0)
                    {
                        items[i] = null;
                        GUI.RemoveItem(i);
                    }
                }
            //    itemImages[i].sprite = null;
            //    itemImages[i].enabled = false;
                return;
            }
        }
    }
    */
 /*   public void useItem(int id)
    {
        items[id].Use();
        RemoveItem(items[id]);
    }*/
}