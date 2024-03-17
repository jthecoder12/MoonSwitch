using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckItemBought : MonoBehaviour
{
	[SerializeField]
	private RawImage itemImage;

	[SerializeField]
	private GameObject soccerBallImage;

    // Start is called before the first frame update
    void Start()
    {
		GetComponent<GameManager>().isEquipable = false;
		GetComponent<Button>().enabled = true;

        foreach(byte item in GameManager.itemsBought)
		{
			if(item != 0)
			{
				if(item == 0xA)
				{
					GameManager.items[item].SetItemImage(soccerBallImage);
				}

				GameManager.items[item].GetItemImage().GetComponent<Button>().enabled = false;
				GameManager.items[item].GetItemImage().GetComponent<GameManager>().isEquipable = false;
				itemImage.color = Color.grey;
			}
		}
    }
}
