using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRockScript : MonoBehaviour
{
    [SerializeField]
    private GameObject moonRock;

    private bool spawnStarted;


    // Update is called once per frame
    void Update()
    {
        if(!spawnStarted)
        {
            StartCoroutine(SpawnMoonrock());
        }
    }

    private IEnumerator SpawnMoonrock()
    {
        spawnStarted = true;
        yield return new WaitForSeconds(3);
        Instantiate(moonRock, new Vector2(Random.Range(-1.774232f, 1.68f), Random.Range(-3.08f, 3.951373f)), Quaternion.identity);
        spawnStarted = false;
    }
}
