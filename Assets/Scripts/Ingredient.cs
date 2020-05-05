using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "New Ingredient")]
public class Ingredient : ScriptableObject
{
    [Header("Ingredient Stats")]
    public string ingredientName;
    public Sprite ingredientIcon;
    public GameObject modelPrefab;
}
