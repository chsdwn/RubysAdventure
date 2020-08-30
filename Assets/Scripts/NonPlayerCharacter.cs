﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public GameObject dialogBox;
    public float displayTime = 4f;

    float timerDisplay;

    void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -1f;
    }

    private void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;

            if (timerDisplay < 0)
                dialogBox.SetActive(false);
        }
    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }
}
