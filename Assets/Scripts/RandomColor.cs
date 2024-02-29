using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RandomColor : MonoBehaviour
{
    [SerializeField]
    private Color color;

    private void Awake()
    {
        Color[] colors = { Color.white, color };

        Color ranColor = colors[Random.Range(0, colors.Length)];

        GetComponent<SpriteRenderer>().color = ranColor;

        if (GetComponent<SpriteRenderer>().color == GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color || GetComponent<SpriteRenderer>().color == Color.black)
        {
            List<Color> _ = new List<Color>(colors);
            _.Remove(ranColor);
            colors = _.ToArray();
            ranColor = colors[Random.Range(0, colors.Length)];
            GetComponent<SpriteRenderer>().color = ranColor;
        }
    }
}
