using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PowerupData : ScriptableObject
{
    public int PowerupType;
    public string Name;
    public Sprite Icon;
    public int CoinsCost;
    public int CurrentLevel;
    public int MaxLevel;
}
