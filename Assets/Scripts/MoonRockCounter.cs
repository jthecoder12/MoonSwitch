using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoonRockCounter : MonoBehaviour
{
    private void Awake()
    {
        if(PlayerPrefs.HasKey("Score"))
        {
            UpdateMoonrockCounter();
        }
    }

    public static void UpdateMoonrockCounter()
    {
        GameObject.FindGameObjectWithTag("mrc").GetComponent<Text>().text = $"Moon Rocks Collected: {PlayerPrefs.GetInt("Score")}";
    }
}
