using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreperationBench : Countertop
{
    Dish activeDish = null;
    [SerializeField] List<Ingredient> remainingIngredients = new List<Ingredient>();

    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact(PlayerController a_player)
    {
        playerController = a_player;

        // Check if player is holding ingredient
        if (playerController.IsHoldingIngredient())
        {
            // Check if player is holding desired ingredient
            if (ContainsIngredientByName(remainingIngredients, playerController.GetHeldItem().GetComponent<IngredientObject>().ingredient.ingredientName))
            {
                // If so, take ingredient and remove from 'remainingIngredients'
                RemoveIngredientByName(remainingIngredients, playerController.GetHeldItem().GetComponent<IngredientObject>().ingredient.ingredientName);
                playerController.DiscardHeldItem();

                // Check if dish is complete
                if (remainingIngredients.Count < 1)
                {
                    // If so, spawn DishObject on countertop
                    CompleteAndPickupDish();
                }

            }
        }
    }

    void CompleteAndPickupDish()
    {
        activeDish = GameManager.instance.GetActiveDish();

        GameObject dishObject = Instantiate(activeDish.dishPrefab, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        DishObject dishObjComponent = dishObject.AddComponent<DishObject>();
        dishObjComponent.dish = activeDish;
        dishObject.name = activeDish.dishName;

        if (playerController.IsHoldingItem())
            playerController.DiscardHeldItem();

        playerController.PickupItem(dishObject);
    }

    public void SelectActiveDish(Dish a_dish)
    {
        // Clones the ingredients list of 'a_dish'
        remainingIngredients = new List<Ingredient>(a_dish.ingredients);
    }





    public bool ContainsIngredientByName(List<Ingredient> a_list, string a_ingedientName)
    {
        bool result = false;

        for (int i = 0; i < a_list.Count; i++)
        {
            if (a_list[i].ingredientName == a_ingedientName)
            {
                result = true;
                break;
            }
        }

        return result;
    }

    public void RemoveIngredientByName(List<Ingredient> a_list, string a_ingredientName)
    {
        int index = -1;

        for (int i = 0; i < a_list.Count; i++)
        {
            if (a_list[i].ingredientName == a_ingredientName)
            {
                index = i;
                break;
            }
        }

        if (index > -1)
        {
            a_list.RemoveAt(index);
        }
        else
            Debug.Log("Ingredient not in list");
    }
}
