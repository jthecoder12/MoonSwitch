using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessCollision : MonoBehaviour
{
    public bool isEnabled = true;

    [SerializeField]
    private float distanceBetween;

    [SerializeField]
    private GameObject[] obstacleSets;

    [SerializeField]
    private Transform firstObstacle;

    [SerializeField]
    private GameObject loseScreen;

    [SerializeField]
    private GameObject CMVCam;

    [SerializeField]
    private GameObject CMVCamBottom;

    [SerializeField]
    private GameObject CMVCamTop;

    [Header("Sound Controllers")]
    [SerializeField]
    private AudioSource deathSoundController;

    [SerializeField]
    private AudioSource beepSoundController;

    private bool created = false;

    private Transform temp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // print("Collision");

        if (collision.CompareTag("colorChange"))
        {
            GetComponent<SpriteRenderer>().color = collision.GetComponent<SpriteRenderer>().color;
            if (!created)
            {
                Invoke("DestroyFirstObstacle", 3.5f);
                SpawnPlatform();
                created = true;
            }
            isEnabled = false;
            CMVCamBottom.transform.localScale = new Vector3(GameObject.Find("CM vcam1/Bottom").transform.localScale.x, 6.927564f, GameObject.Find("CM vcam1/Bottom").transform.localScale.z);
            CMVCamTop.transform.localScale = new Vector3(GameObject.Find("CM vcam1/Top").transform.localScale.x, 8.294118f, GameObject.Find("CM vcam1/Top").transform.localScale.z);
            Invoke("Enable", 2);
            Destroy(collision.gameObject);
        }

        if (!enabled) return;

        if(collision.CompareTag("obstacle") && 
            ColorUtility.ToHtmlStringRGB(GetComponent<SpriteRenderer>().color) != 
            ColorUtility.ToHtmlStringRGB(collision.GetComponent<SpriteRenderer>().color))
        {
            ShowLoseScreen();
        }

        if(collision.CompareTag("Respawn"))
        {
            ShowLoseScreen();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("colorChange"))
        {
            created = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("standplatform"))
        {
            beepSoundController.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        beepSoundController.Stop();
    }

    private void ShowLoseScreen()
    {
        deathSoundController.Play();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Stop();
        loseScreen.SetActive(true);
        Destroy(CMVCam);
        Destroy(gameObject);
    }

    private void SpawnPlatform()
    {
        GameObject obstacleSet = obstacleSets[Random.Range(0, obstacleSets.Length)];

        // print(obstacleSets.Length);

        temp = Instantiate(obstacleSet, new Vector2(firstObstacle.position.x, firstObstacle.position.y + distanceBetween), Quaternion.identity).transform;
        Invoke("ChangeCurrent", 3.5f);
    }

    private void DestroyFirstObstacle()
    {
        if (firstObstacle == null) return;

        Destroy(firstObstacle.gameObject);
    }

    private void ChangeCurrent()
    {
        firstObstacle = temp;
    }

    private void Enable()
    {
        isEnabled = true;
    }
}
