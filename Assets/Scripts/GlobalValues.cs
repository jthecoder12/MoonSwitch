using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValues : MonoBehaviour
{
    public short score = 0;

    private void Awake()
    {
        if(PlayerPrefs.HasKey("Score"))
        {
            score = (short)PlayerPrefs.GetInt("Score");
        }
    }
}
