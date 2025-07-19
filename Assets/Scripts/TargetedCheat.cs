using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedCheat : MonoBehaviour
{
    int clicks;
    bool enabledInfiniteLives;
    private GameManager gm;

    void Awake()
    {
        enabledInfiniteLives = false;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void OnMouseDown()
    {
        if (enabledInfiniteLives == true) return;
        clicks += 1;
    }

    void Update()
    {
        if (clicks >= 5 && enabledInfiniteLives == false)
        {
            gm.EnableInfiniteLives();
            enabledInfiniteLives = true;
        }
    }
}
