﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingBench : Countertop
{
    public override void Interact(PlayerController a_player)
    {
        if (a_player.IsHoldingDish())
        {
            GameManager.instance.ServeDish();
            a_player.DiscardHeldItem();
        }
    }
}
