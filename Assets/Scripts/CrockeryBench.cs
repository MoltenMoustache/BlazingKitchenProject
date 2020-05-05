using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrockeryBench : Countertop
{
    [SerializeField] Crockery crockeryType;

    public override void Interact(PlayerController a_player)
    {
        if (a_player.IsHoldingItem())
            a_player.DiscardHeldItem();

        // Alter spawn pos?
        GameObject crockeryObject = Instantiate(crockeryType.modelPrefab, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        crockeryObject.AddComponent<CrockeryObject>().crockery = crockeryType;
        a_player.PickupItem(crockeryObject);
    }
}
