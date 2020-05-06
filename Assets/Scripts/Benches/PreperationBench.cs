using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreperationBench : Countertop
{
    Dish activeDish = null;
    [SerializeField] Transform ingredientPlacement_a;
    [SerializeField] Transform ingredientPlacement_b;
    List<Ingredient> totalIngredients = new List<Ingredient>();
    List<Ingredient> remainingIngredients = new List<Ingredient>();

    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ClearBench()
    {
        // Clear bench
        if (ingredientPlacement_a.childCount > 0)
            Destroy(ingredientPlacement_a.GetChild(0).gameObject);
        if (ingredientPlacement_b.childCount > 0)
            Destroy(ingredientPlacement_b.GetChild(0).gameObject);
    }

    public override void Interact(PlayerController a_player)
    {
        if (remainingIngredients.Count < 1)
        {
            if (a_player.IsHoldingCrockery())
            {
                GameObject dishObject = Instantiate(activeDish.dishPrefab, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                DishObject dishObjComponent = dishObject.AddComponent<DishObject>();
                dishObjComponent.dish = activeDish;
                dishObject.name = activeDish.dishName;

                if (a_player.IsHoldingItem())
                    a_player.DiscardHeldItem();

                a_player.PickupItem(dishObject);
                ClearBench();
                remainingIngredients = new List<Ingredient>(totalIngredients);
            }
        }


        // Check if player is holding ingredient
        if (a_player.IsHoldingIngredient())
        {
            // Check if player is holding desired ingredient
            if (ContainsIngredientByName(remainingIngredients, a_player.GetHeldItem().GetComponent<IngredientObject>().ingredient.ingredientName))
            {

                // Places ingredient on bench

                Transform placementPosition = null;
                if (remainingIngredients.Count == 2)
                {
                    placementPosition = ingredientPlacement_a;
                }
                else if (remainingIngredients.Count == 1)
                {
                    placementPosition = ingredientPlacement_b;
                }


                GameObject ingredientObject = Instantiate(a_player.GetHeldItem(), placementPosition.position, Quaternion.identity);
                ingredientObject.transform.localScale = a_player.GetHeldItem().transform.localScale;
                ingredientObject.transform.parent = placementPosition;


                // If so, take ingredient and remove from 'remainingIngredients'
                RemoveIngredientByName(remainingIngredients, a_player.GetHeldItem().GetComponent<IngredientObject>().ingredient.ingredientName);

                a_player.DiscardHeldItem();

                // Check if dish is complete
                if (remainingIngredients.Count < 1)
                {
                    // If so, spawn DishObject on countertop
                    activeDish = GameManager.instance.GetActiveDish();
                }

            }
        }
    }

    public void SelectActiveDish(Dish a_dish)
    {
        // Clones the ingredients list of 'a_dish'
        totalIngredients = new List<Ingredient>(a_dish.ingredients);
        remainingIngredients = new List<Ingredient>(a_dish.ingredients);
        ClearBench();
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
