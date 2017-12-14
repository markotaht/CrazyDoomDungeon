using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionButton : MonoBehaviour {

    public static PotionButton instance;
    [SerializeField]
    private long potionId;

    [SerializeField]
    private Text number;

    private int counter = 0;

    private DatabaseConsumable potion;
	// Use this for initialization
	void Start () {
        instance = this;
        potion = ItemLoader.instance.getItem(potionId) as DatabaseConsumable;
	}
	
	public void Use()
    {
        if(counter > 0)
        {
            counter--;
            number.text = counter.ToString();
            potion.Use();
        }
    }

    public void AddPotion()
    {
        counter++;
        number.text = counter.ToString();
    }
}
