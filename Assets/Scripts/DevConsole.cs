using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dev console. Press Z and M together at the same time in varius places such as the shop to open.
public class DevConsole : MonoBehaviour
{
    [SerializeField]
    private GameObject devConsole;

    [SerializeField]
    private GameObject easterEgg;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && Input.GetKeyDown(KeyCode.M))
        {
            if(devConsole.activeInHierarchy == true)
            {
                devConsole.SetActive(false);
            } else
            {
                devConsole.SetActive(true);
            }
        }


        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.Q))
        {
            if(easterEgg is not null)
            {
                if(easterEgg.activeInHierarchy)
                {
                    easterEgg.SetActive(false);
                }
                else
                {
                    easterEgg.SetActive(true);
                }
            }
        }
    }
}
