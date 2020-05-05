using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBin : Countertop
{
    [Header("Bin Stats")]
    [SerializeField] Ingredient ingredient;

    // Picks up held ingredient, if player is already holding an ingredient... discard it and pick up this one.
    public override void Interact(PlayerController a_player)
    {
        PlayerController player = a_player;

        if (player.IsHoldingItem())
            player.DiscardHeldItem();

        // Alter spawn pos?
        GameObject ingredientObject = Instantiate(ingredient.modelPrefab, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        ingredientObject.AddComponent<IngredientObject>().ingredient = ingredient;
        a_player.PickupItem(ingredientObject);
    }
}
