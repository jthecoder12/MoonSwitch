using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Moon rock item script
public class Collectable : MonoBehaviour
{
    private Text counterText;

    private bool collided = false;

    private void Start()
    {
        counterText = GameObject.FindGameObjectWithTag("mrc").GetComponent<Text>();

        short currentScore = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlobalValues>().score;

        counterText.text = $"Moon Rocks Collected: {currentScore}";

        // print(PlayerPrefs.GetInt("Score"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!collided)
            {
                short currentScore = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlobalValues>().score;

                // print(currentScore);

                currentScore += 1;

                SaveScore(currentScore);

                counterText.text = $"Moon Rocks Collected: {currentScore}";

                collided = true;
            }

            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        SaveScore(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlobalValues>().score);
    }

    private void OnApplicationFocus(bool focus)
    {
        SaveScore(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlobalValues>().score);
    }

    private void SaveScore(short currentScore)
    {
        PlayerPrefs.SetInt("Score", currentScore);
        PlayerPrefs.Save();
    }
}
