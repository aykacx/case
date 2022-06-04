using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "IncrementalData", menuName = "ScriptableObjects/IncrementalData")]
public class Incremental_SO : ScriptableObject
{
    public ItemData jetpack;
    public ItemData jumpForce;
    public ItemData gold;
    public Color color;
}
[Serializable]
public struct ItemData
{
    public int level;
    public int cost;
    public float jetpackFuel;
    public Vector3 mainForce;
    public int gold;
}
