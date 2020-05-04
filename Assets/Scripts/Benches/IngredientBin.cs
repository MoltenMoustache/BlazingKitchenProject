using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBin : Countertop
{
    [Header("Bin Stats")]
    [SerializeField] GameObject ingredientPrefab;
    Ingredient ingredient;

    // Start is called before the first frame update
    void Start()
    {
        ingredient = ingredientPrefab.GetComponent<Ingredient>();
    }

    // Picks up held ingredient, if player is already holding an ingredient... discard it and pick up this one.
    public override void Interact(PlayerController a_player)
    {
        PlayerController player = GameManager.instance.playerController;

        if (player.IsHoldingItem())
            player.DiscardHeldItem();

        // Alter spawn pos?
        GameObject ingredientObject = Instantiate(ingredientPrefab, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        GameManager.instance.playerController.PickupItem(ingredientObject);
    }
}
