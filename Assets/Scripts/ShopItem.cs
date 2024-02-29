using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem
{
    private string itemName;
    private string type;
    private short price;
    private byte itemID;
#nullable enable
    private GameObject? itemImage;

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
#pragma warning disable CS8603 // Possible null reference return.
        return itemImage;
#pragma warning restore CS8603 // Possible null reference return.
    }
}
