using System.Collections;
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

    [Header("Sound Effects")]
    [SerializeField] AudioClip musicClip;
    [SerializeField] AudioClip pickupSound;
    [SerializeField] AudioClip correctDishSound;
    [SerializeField] AudioClip wrongDishSound;
    Dictionary<string, AudioClip> sfxDictionary = new Dictionary<string, AudioClip>();
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<UIManager>();

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        SelectNextDish();

        timer = new Timer(totalGameTime, EndGame);

        if (uiManager != null)
            uiManager.SetMaxGameTime(totalGameTime);

        // Adds SFX to dictionary
        audioSource = GetComponent<AudioSource>();

        sfxDictionary.Add("Pickup", pickupSound);
        sfxDictionary.Add("Correct", correctDishSound);
        sfxDictionary.Add("Incorrect", wrongDishSound);
        sfxDictionary.Add("Music", musicClip);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer != null)
            timer.Tick(Time.deltaTime);

        if (uiManager != null)
            uiManager.SetCurrentGameTime(timer.CurrentTimer);

        if (dishTimer != null)
        {
            dishTimer.Tick(Time.deltaTime);
            if (uiManager != null)
                uiManager.SetCurrentOrderTime(dishTimer.CurrentTimer);
        }


        if (respawnTimer != null)
        {
            respawnTimer.Tick(Time.deltaTime);
            uiManager.UpdateRespawnCountdown(respawnTimer.CurrentTimer);
        }
        else
        {
            uiManager.UpdateRespawnCountdown(0.0f);
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
        respawnTimer = null;
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
        PlaySoundEffect("Incorrect");
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

        PlaySoundEffect("Correct");
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

    public void PlaySoundEffect(string a_soundKey)
    {
        if (sfxDictionary.ContainsKey(a_soundKey))
        {
            audioSource.PlayOneShot(sfxDictionary[a_soundKey]);
        }
    }
}
