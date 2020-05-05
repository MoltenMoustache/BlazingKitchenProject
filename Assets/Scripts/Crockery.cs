using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crockery", menuName = "BKP/New Crockery")]
public class Crockery : ScriptableObject
{
    public string crockeryName;
    public Sprite crockeryIcon;
    public GameObject modelPrefab;
}
