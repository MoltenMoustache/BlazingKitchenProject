﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dish", menuName = "New Dish")]
public class Dish : ScriptableObject
{
    public string dishName;
    public Sprite dishIcon;
    public List<Ingredient> ingredients = new List<Ingredient>();
    public GameObject dishPrefab;
    public Crockery requiredCrockery;
}
