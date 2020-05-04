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
    public PlayerController playerController;
    string discordWebhookURL = "https://discordapp.com/api/webhooks/706712743100809288/oCQfgBBKFGgBuStZVPQ1bcJMe7P9lsIHexoBFpeVK4KBrBE_oQ5Qn3tuFQ49224n1dir";


    [Header("Score")]
    [SerializeField] float totalGameTime;
    Timer timer;
    float currentScore;

    [Header("Dishes")]
    [SerializeField] List<Dish> dishes = new List<Dish>();
    Dish activeDish = null;

    [Header("Benches")]
    [SerializeField] List<PreperationBench> prepBenches = new List<PreperationBench>();


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        SelectNextDish();

        timer = new Timer(totalGameTime, EndGame);
    }

    // Update is called once per frame
    void Update()
    {
        timer.Tick(Time.deltaTime);
    }

    void SelectNextDish()
    {
        activeDish = dishes[Random.Range(0, dishes.Count)];
        Debug.Log("Active Dish: " + activeDish.dishName);
        // Tell PrepBenches (delegate?)
        // TEMPORARY
        for (int i = 0; i < prepBenches.Count; i++)
        {
            prepBenches[i].SelectActiveDish(activeDish);
        }
        // Tell UI
    }

    public void ServeDish(float a_timeRemaining)
    {
        float score = a_timeRemaining / 3.0f;
        if (score < 1.0f)
            score = 1.0f;

        currentScore += (int)score;

        SelectNextDish();
    }

    public Dish GetActiveDish()
    {
        return activeDish;
    }

    void EndGame()
    {
        Debug.Log("Game Over with a score of: " + currentScore);

        Embed embed = new Embed
        {
            Title = "Blazing Kitchen Project",
            Color = 14177041,
            Description = "I scored " + currentScore.ToString() + "!",
            Footer = new EmbedFooter
            {
                Text = "It was tough"

            }
        };

        StartCoroutine(SendEmbed(embed));
    }
    #region DiscordWebhook
    IEnumerator SendEmbed(Embed a_embed)
    {
        Webhook webhook = new Webhook(discordWebhookURL);
        List<Embed> embeds = new List<Embed>();
        embeds.Add(a_embed);


        yield return webhook.Send(string.Empty, "Blazing Kitchen Project", "https://cdn.discordapp.com/attachments/706667975171768330/706714712175411250/icon.png", false, embeds);
    }
    #endregion
}
