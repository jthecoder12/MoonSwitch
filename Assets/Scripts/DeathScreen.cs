using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

// The death screen script
public class DeathScreen : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text distanceLabel;

    [SerializeField]
    private UnityEngine.UI.Text moonRocksLabel;

    private UnityEngine.UI.Text distanceCounter;

    // Start is called before the first frame update
    void Start()
    {
        distanceCounter = GameObject.FindGameObjectWithTag("distanceCounter").GetComponent<UnityEngine.UI.Text>();

        float _ = float.Parse(Regex.Replace(distanceCounter.text, "[^0-9.+-]", ""));

        moonRocksLabel.text = $"Total Moonrocks: {PlayerPrefs.GetInt("Score")}";
        distanceLabel.text = $"Final Distance: {Regex.Replace(distanceCounter.text, "[^0-9.+-]", "")} cm";
        if(Mathf.Sign(_) == -1 && _ != 0)
        {
            distanceLabel.color = Color.red;
        }

		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoonRockScript>().enabled = false;
    }
}
