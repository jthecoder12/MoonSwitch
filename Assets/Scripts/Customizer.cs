using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Customizer : MonoBehaviour
{
    [SerializeField]
    private Text itemsBoughtText;

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
            catch (System.NullReferenceException e) { }
        }

        List<byte> _ = new List<byte>();

        foreach (byte item in GameManager.itemsBought)
        {
            //print(item);
            _.Add(item);
        }

        List<ShopItem> __ = new List<ShopItem>();

        foreach (ShopItem item in GameManager.items.Values.ToArray())
        {
            if (_.Contains(item.GetItemID()))
            {
                //print(item.GetItemName());
                __.Add(item);
            }
        }

        List<GameObject> ___ = new List<GameObject>();
        List<ShopItem> shopBalls = new List<ShopItem>();

        foreach (ShopItem item in __)
        {
            try
            {
                ___.Add(Instantiate(item.GetItemImage(), GameObject.FindGameObjectWithTag("itemsCanvas").transform));
            } catch(System.Exception) {}

            if (item.GetType() == "Ball")
            {
                shopBalls.Add(item);
            }
        }
    }

    public void Test()
    {
        //print("Test");
    }
}
