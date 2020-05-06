﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DiscordWebhook;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    int finalScore;


    [Header("Canvases")]
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject sharePanel;

    [Header("Input Fields")]
    [SerializeField] TMP_InputField usernameField;
    [SerializeField] TMP_InputField discordURLField;

    [Header("Dynamic Text")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI respawnCountdownText;

    [Header("Order Card")]
    [SerializeField] TextMeshProUGUI dishName;
    [SerializeField] Image dishIcon;
    [SerializeField] TextMeshProUGUI ingredientName_a;
    [SerializeField] TextMeshProUGUI ingredientName_b;
    [SerializeField] TextMeshProUGUI crockeryName;
    [SerializeField] Image itemIcon_a;
    [SerializeField] Image itemIcon_b;
    [SerializeField] Image crockeryIcon;
    [SerializeField] Slider orderTimeSlider;

    [Header("Other UI")]
    [SerializeField] Slider timerSlider;

    #region Buttons
    public void OpenSharePanel()
    {
        sharePanel.SetActive(true);
        usernameField.text = PlayerPrefs.GetString("Username");
        discordURLField.text = PlayerPrefs.GetString("DiscordWebhookURL");
    }
    public void CloseSharePanel()
    {
        sharePanel.SetActive(false);
    }

    public void SubmitShareResult()
    {
        string username = usernameField.text;
        string discordWebhookURL = discordURLField.text;

        PlayerPrefs.SetString("Username", username);
        PlayerPrefs.SetString("DiscordWebhookURL", discordWebhookURL);

        Embed embed = new Embed
        {
            Title = "Devil's Delights",
            Color = 14177041,
            Description = username + " scored " + finalScore.ToString() + "!",
            Footer = new EmbedFooter
            {
                Text = "It was tough",
            }
        };

        StartCoroutine(SendEmbed(embed, discordWebhookURL));
        CloseSharePanel();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    #endregion Buttons

    public void UpdateRespawnCountdown(float a_currentTime)
    {
        if (a_currentTime != 0.0f)
        {
            respawnCountdownText.enabled = true;
            respawnCountdownText.text = (a_currentTime + 1.0f).ToString()[0].ToString();
        }
        else
            respawnCountdownText.enabled = false;

    }

    public void EndGame(int a_score)
    {
        finalScore = a_score;
        gameOverCanvas.SetActive(true);
        scoreText.text = "You score " + a_score.ToString() + " points";
    }

    public void SetMaxGameTime(float a_maxGameTime)
    {
        timerSlider.maxValue = a_maxGameTime;
    }
    public void SetCurrentGameTime(float a_currentGameTime)
    {
        timerSlider.value = a_currentGameTime;
    }

    public void SetCurrentOrderTime(float a_currentOrderTime)
    {
        orderTimeSlider.value = a_currentOrderTime;
    }

    public void SetActiveDish(Dish a_dish, float a_orderTimeout = 10.0f)
    {
        orderTimeSlider.maxValue = a_orderTimeout;
        dishName.text = a_dish.dishName;
        dishIcon.sprite = a_dish.dishIcon;


        for (int i = 0; i < a_dish.ingredients.Count; i++)
        {
            switch (i)
            {
                case 0:
                    ingredientName_a.text = a_dish.ingredients[i].ingredientName;
                    itemIcon_a.sprite = a_dish.ingredients[i].ingredientIcon;
                    break;
                case 1:
                    ingredientName_b.text = a_dish.ingredients[i].ingredientName;
                    itemIcon_b.sprite = a_dish.ingredients[i].ingredientIcon;
                    break;
            }
        }

        crockeryName.text = a_dish.requiredCrockery.crockeryName;
        crockeryIcon.sprite = a_dish.requiredCrockery.crockeryIcon;



        //List<Ingredient> ingredients = new List<Ingredient>(a_dish.ingredients);
        //List<string> ingredientKeys = new List<string>();
        //Dictionary<string, int> ingredientCounts = new Dictionary<string, int>();

        //for (int i = 0; i < ingredients.Count; i++)
        //{
        //    // Adds ingredient keys to key list
        //    if (!ingredientKeys.Contains(ingredients[i].ingredientName))
        //    {
        //        ingredientKeys.Add(ingredients[i].ingredientName);
        //    }


        //    // Adds ingredient to dictionary
        //    int count;
        //    if (ingredientCounts.TryGetValue(ingredients[i].ingredientName, out count))
        //    {
        //        ingredientCounts.Remove(ingredients[i].ingredientName);
        //    }

        //    count++;
        //    ingredientCounts.Add(ingredients[i].ingredientName, count);
        //}

        //for (int i = 0; i < ingredientKeys.Count; i++)
        //{
        //    switch (i)
        //    {
        //        case 0:
        //            itemQuantity_a.enabled = true;
        //            itemIcon_a.enabled = true;

        //            itemQuantity_a.text = ingredientCounts[ingredientKeys[i]] + "x";
        //            itemIcon_a.sprite = GetIngredientByName(ingredients, ingredientKeys[i]).ingredientIcon;
        //            break;
        //        case 1:
        //            itemQuantity_a.enabled = true;
        //            itemIcon_b.enabled = true;

        //            itemQuantity_b.text = ingredientCounts[ingredientKeys[i]] + "x";
        //            itemIcon_b.sprite = GetIngredientByName(ingredients, ingredientKeys[i]).ingredientIcon;
        //            break;
        //        case 2:
        //            itemQuantity_a.enabled = true;
        //            itemIcon_c.enabled = true;

        //            itemQuantity_c.text = ingredientCounts[ingredientKeys[i]] + "x";
        //            itemIcon_c.sprite = GetIngredientByName(ingredients, ingredientKeys[i]).ingredientIcon;
        //            break;
        //        case 3:
        //            itemQuantity_a.enabled = true;
        //            itemIcon_d.enabled = true;

        //            itemQuantity_d.text = ingredientCounts[ingredientKeys[i]] + "x";
        //            itemIcon_d.sprite = GetIngredientByName(ingredients, ingredientKeys[i]).ingredientIcon;
        //            break;
        //    }
        //}
    }

    #region Discord
    IEnumerator SendEmbed(Embed a_embed, string a_discordURL)
    {
        Webhook webhook = new Webhook(a_discordURL);
        List<Embed> embeds = new List<Embed>();
        embeds.Add(a_embed);


        yield return webhook.Send(string.Empty, "Devil's Delights", "https://cdn.discordapp.com/attachments/706667975171768330/706714712175411250/icon.png", false, embeds);
    }
    #endregion Discord

    public Ingredient GetIngredientByName(List<Ingredient> a_list, string a_ingredientName)
    {
        for (int i = 0; i < a_list.Count; i++)
        {
            if (a_list[i].ingredientName == a_ingredientName)
            {
                return a_list[i];
            }
        }

        return null;
    }
}
