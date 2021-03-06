﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiscordWebhook;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance = null;
    private void Awake()
    {
        instance = this;
    }
    #endregion Singleton

    // References
    [HideInInspector] public PlayerController playerController;
    UIManager uiManager;

    [Header("Score")]
    [SerializeField] float totalGameTime;
    Timer timer;
    float currentScore;

    [Header("Dishes")]
    [SerializeField] List<Dish> dishes = new List<Dish>();
    [SerializeField] float orderTimeout = 10.0f;
    Dish activeDish = null;
    Timer dishTimer = null;

    [Header("Benches")]
    [SerializeField] List<PreperationBench> prepBenches = new List<PreperationBench>();

    [Header("Respawning")]
    [SerializeField] float respawnTime = 5.0f;
    Timer respawnTimer = null;
    [SerializeField] Transform respawnPoint = null;


    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<UIManager>();

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        SelectNextDish();

        timer = new Timer(totalGameTime, EndGame);

        if (uiManager != null)
            uiManager.SetMaxGameTime(totalGameTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer != null)
            timer.Tick(Time.deltaTime);

        if (uiManager != null)
            uiManager.SetCurrentGameTime(timer.CurrentTimer);

        if (respawnTimer != null)
            respawnTimer.Tick(Time.deltaTime);

        if (dishTimer != null)
        {
            dishTimer.Tick(Time.deltaTime);
            if (uiManager != null)
                uiManager.SetCurrentOrderTime(dishTimer.CurrentTimer);
        }
    }

    #region Respawn
    public void KillPlayer()
    {
        respawnTimer = new Timer(respawnTime, RespawnPlayer);
    }

    void RespawnPlayer()
    {
        playerController.RespawnPlayer(respawnPoint.position);
    }
    #endregion Respawn

    #region Dishes
    void SelectNextDish()
    {
        activeDish = dishes[Random.Range(0, dishes.Count)];

        dishTimer = new Timer(orderTimeout, OrderTimeout);

        // TEMPORARY
        for (int i = 0; i < prepBenches.Count; i++)
        {
            prepBenches[i].SelectActiveDish(activeDish);
        }


        // Tell UI
        if (uiManager != null)
            uiManager.SetActiveDish(activeDish, orderTimeout);
    }

    void OrderTimeout()
    {
        currentScore -= 1;
        SelectNextDish();
    }

    public void ServeDish()
    {
        float score = dishTimer.CurrentTimer / 2.0f;
        if (score < 1.0f)
            score = 1.0f;

        currentScore += (int)score;

        SelectNextDish();
    }

    public Dish GetActiveDish()
    {
        return activeDish;
    }
    #endregion

    void EndGame()
    {
        Debug.Log("Game Over with a score of: " + currentScore);

        if (uiManager != null)
            uiManager.EndGame((int)currentScore);
    }
}
