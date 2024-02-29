using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverConfiguration : MonoBehaviour
{
    public static bool isOver = false;

    public void EnterOver()
    {
        isOver = true;
    }

    public void ExitOver()
    {
        isOver = false;
    }
}
