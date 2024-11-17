using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Professional looking code
public class Customizer : MonoBehaviour
{
    [SerializeField]
    private Text itemsBoughtText;

	[SerializeField]
	private Text equipText;

    // Start is called before the first frame update
    private void Start()
    {
        GameManager.InitArray();

        for (byte i = 0; i < PlayerPrefs.GetInt("ItemsBoughtCount"); i++)
        {
            try
            {
                itemsBoughtText.text += $" {GameManager.items[GameManager.itemsBought[i]].GetItemName()},";
            }
            catch (System.NullReferenceException) { }
        }

        List<byte> _ = new List<byte>();

        foreach (byte item in GameManager.itemsBought)
        {
            print(item);
            _.Add(item);
        }

        List<ShopItem> __ = new List<ShopItem>();

        foreach (ShopItem item in GameManager.items.Values.ToArray())
        {
			print(GameManager.items.Values.ToArray());
			print(_.Contains(item.GetItemID()));

            if (_.Contains(item.GetItemID()))
            {
                print(item.GetItemName());
                __.Add(item);
            }
        }

        foreach (ShopItem item in __)
        {
			print(__.ToArray());
			print(item.GetItemID());
			print(GameObject.FindGameObjectWithTag("backbutton").
					GetComponent<GameManager>().soccerBallImageRaw);
			if(item.GetItemID() == 0xA)
			{
				item.SetItemImage(GameObject.FindGameObjectWithTag("backbutton").
					GetComponent<GameManager>().soccerBallImageRaw);
			}
			print(item.GetItemImage());
			item.GetItemImage().GetComponent<GameManager>().isEquipable = true;
			item.GetItemImage().GetComponent<Button>().enabled = true;
			item.GetItemImage().GetComponent<GameManager>().equipText = equipText;
			Instantiate(item.GetItemImage(), GameObject.FindGameObjectWithTag("itemsCanvas").transform);
        }
    }

    public void Test()
    {
        //print("Test");
    }
}
