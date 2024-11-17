using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An entire script just to play the title screen music at the same position it was when you were in the other scene
public class StartMusicAtCurrentPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		PlayerPrefs.DeleteKey("BoughtItem_1");

		GetComponent<AudioSource>().time = GlobalValues.musicPosition;
    }
}
