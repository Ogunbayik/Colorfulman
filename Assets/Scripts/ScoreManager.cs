using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreManager : MonoBehaviour
{
    private PlayerInteract playerInteract;
    private PunchCollider punchCollider;

    [Header(" Settings ")]
    [SerializeField] private int destroyScore;
    [SerializeField] private int pickUpScore;
    [SerializeField] private Text scoreText;

    private int currentScore;
    private void Awake()
    {
        playerInteract = FindObjectOfType<PlayerInteract>();
        punchCollider = FindObjectOfType<PunchCollider>();
    }
    void Start()
    {
        currentScore = 0;
        scoreText.text = currentScore.ToString();
    }

    private void OnEnable()
    {
        playerInteract.OnBoxPickUp += AddPickUpScore;
        punchCollider.OnBoxDestroyed += AddDestroyScore;
    }
    private void OnDestroy()
    {
        playerInteract.OnBoxPickUp -= AddPickUpScore;
        punchCollider.OnBoxDestroyed -= AddDestroyScore;
    }

    private void AddDestroyScore()
    {
        currentScore += destroyScore;
        scoreText.text = currentScore.ToString();
    }

    private void AddPickUpScore()
    {
        currentScore += pickUpScore;
        scoreText.text = currentScore.ToString();
    }

}
