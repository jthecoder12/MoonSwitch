using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The bullet script
public class DestroyObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("obstacle"))
        {
            print("Collision");

            Destroy(collision.gameObject);
        }
    }
}
