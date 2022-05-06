using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerController2DData : ScriptableObject
{
    public int playerHealth;
    public float playerFireRate;
    public float playerMoveSpeed;

    public SpriteRenderer renderer;
    public Color color;

    [Header("PowerUps")]
    public float FireRateAfterPowerUp;
    public int HealthAfterPowerUp;
    public float SpeedAfterPowerUp;
}
