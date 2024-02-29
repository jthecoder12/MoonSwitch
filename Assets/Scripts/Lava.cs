using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private bool isEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if(isEnabled)
        {
            transform.localScale += new Vector3(0, speed, 0);
        }
    }

    public bool GetEnabled()
    {
        return isEnabled;
    }

    public void SetEnabled(bool newVal)
    {
        isEnabled = newVal;
    }
}
