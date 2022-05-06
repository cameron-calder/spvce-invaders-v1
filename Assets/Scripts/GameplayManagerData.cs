using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameplayManagerData : ScriptableObject
{
    [Header("PlayerStats")]
    public int playerHealth;
    public int playerScore;
    public int playerWave;

    [Header("HighScore")]
    public int highScore;

    [Header("PowerUps")]
    public bool isHealthAquired;
    public bool isRapidFire;
    public float rapidFireTimer;

}
