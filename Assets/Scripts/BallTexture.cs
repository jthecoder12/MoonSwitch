using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTexture : MonoBehaviour
{
    void Update()
    {
        GetComponent<SpriteRenderer>().color = transform.parent.gameObject.GetComponent<SpriteRenderer>().color;
    }
}
