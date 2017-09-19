using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{

    private InventoryGUI GUI;

  //  public Image[] itemImages = new Image[numItemSlots];
    private Item[] items = new Item[numItemSlots];
    public const int numItemSlots = 40;

    private void Start()
    {
        GUI = GetComponent<InventoryGUI>();
        GUI.SetInventorySize(numItemSlots);
    }

    private void Update()
    {
        if(Input.GetKeyUp("o")){
            AddItem(GameObject.Find("SwordAnimated").GetComponent<MeleeWeapon>() as Item);
        }
    }

    public void AddItem(Item itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                GUI.AddItem(i, itemToAdd);
             //   itemImages[i].sprite = itemToAdd.sprite;
             //   itemImages[i].enabled = true;
                return;
            }
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemToRemove)
            {
                items[i] = null;
                GUI.RemoveItem(i);
            //    itemImages[i].sprite = null;
            //    itemImages[i].enabled = false;
                return;
            }
        }
    }
}