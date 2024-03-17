using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusicAtCurrentPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		PlayerPrefs.DeleteKey("BoughtItem_1");

		GetComponent<AudioSource>().time = GlobalValues.musicPosition;
    }
}
