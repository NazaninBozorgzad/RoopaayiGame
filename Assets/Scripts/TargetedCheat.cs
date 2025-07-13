using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedCheat : MonoBehaviour
{
    int clicks;
    bool enabledInfiniteLives;

    void Awake()
    {
        enabledInfiniteLives = false;
    }
    void OnMouseDown()
    {
        if (enabledInfiniteLives == true) return;
        clicks += 1;
    }

    void Update()
    {
        if (clicks >= 5)
        {
            GameManager.instance.EnableInfiniteLives();
            enabledInfiniteLives = true;
        }
    }
}
