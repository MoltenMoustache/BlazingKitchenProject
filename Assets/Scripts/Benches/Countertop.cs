using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countertop : MonoBehaviour
{
    GameObject heldItem = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Interact(PlayerController a_player)
    {
        // Check if player is holding ingredient
        // If so, take ingredient and place it on countertop
    }

    public GameObject GetHeldItem()
    {
        return heldItem;
    }

    public bool isHoldingItem()
    {
        return heldItem != null;
    }

}
