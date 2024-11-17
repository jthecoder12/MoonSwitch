using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The shop is the most complicated part of the entire game
public class ShopItem
{
    private string itemName;
    private string type;
    private short price;
    private byte itemID;
	private GameObject itemImage;

    public ShopItem(string itemName, string type, short price, byte itemID, GameObject itemImage)
    {
        this.itemName = itemName;
        this.type = type;
        this.price = price;
        this.itemID = itemID;
        this.itemImage = itemImage;
    }

    public string GetItemName()
    {
        return itemName;
    }

    public new string GetType()
    {
        return type;
    }

    public short GetPrice()
    {
        return price;
    }

    public byte GetItemID()
    {
        return itemID;
    }

    public GameObject GetItemImage()
    {
        return itemImage;
    }

	public void SetItemImage(GameObject itemImage)
	{
		this.itemImage = itemImage;
	}
}
