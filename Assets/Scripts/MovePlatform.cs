using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The moving platforms
public class MovePlatform : MonoBehaviour
{
    [SerializeField]
    private float moveValue;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(moveValue, 0, 0);
    }
}
