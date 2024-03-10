using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI counterText;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bad"))
        {
            gameManager.UpdateScore(-1);
        }
        else
        {
            gameManager.UpdateScore(1);
        }
    }
}
