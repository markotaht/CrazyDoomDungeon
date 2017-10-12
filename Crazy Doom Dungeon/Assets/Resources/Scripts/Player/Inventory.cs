using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{

    private InventoryGUI GUI;

    //  public Image[] itemImages = new Image[numItemSlots];
    public const int numItemSlots = 25;
    private DatabaseItem[] items = new DatabaseItem[numItemSlots];

    private void Start()
    {
        GUI = InventoryGUI.instance;
   //     GUI.SetInventorySize(numItemSlots);
        InventoryGUI.ItemAction += useItem;
    }

    public void AddItem(long id)
    {
        DatabaseItem item = ItemLoader.instance.getItem(id);
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                GUI.AddItem(i, item);
                //   itemImages[i].sprite = itemToAdd.sprite;
                //   itemImages[i].enabled = true;
                break;
            }else if(items[i].Id == id && item.stackable)
            {
                GUI.IncreaseCount(i);
                break;
            }
        }
    }

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

    public void useItem(int id)
    {
        items[id].Use();
        RemoveItem(items[id]);
    }
}