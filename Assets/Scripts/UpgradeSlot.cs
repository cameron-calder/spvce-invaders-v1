using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    public UpgradeStore upgradeStore;
    public ScoreManager scoreManager;
    public PlayerController2DData playerController2DData;
    public PowerupData powerupData;

    public Text NameText;
    public Text CostText;
    public Image Icon;

    void OnEnable()
    {
        UpdateUI();   
    }

    void UpdateUI()
    {
        NameText.text = powerupData.Name;
        CostText.text = "UPGRADE\n" + powerupData.CoinsCost.ToString("D2") + " COINS\nLEVEL " + powerupData.CurrentLevel.ToString("D2") + " / " + powerupData.MaxLevel.ToString("D2");
        Icon.sprite = powerupData.Icon;

        if (powerupData.CurrentLevel >= powerupData.MaxLevel)
            CostText.text = "MAX";
    }

    public void Buy()
    {
        if (powerupData.CurrentLevel >= powerupData.MaxLevel)
            return;

        if (PlayerPrefs.GetInt("Coins") < powerupData.CoinsCost)
            return;

        powerupData.CurrentLevel++;

        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - powerupData.CoinsCost);
        upgradeStore.UpdateCoinsText();

        //Rapid Fire
        if (powerupData.PowerupType == 0)
        {
            playerController2DData.FireRateAfterPowerUp = 1 - (0.2f * powerupData.CurrentLevel);
            UpdateUI();
            return;
        }

        //Additional Health
        if (powerupData.PowerupType == 1)
        {
            playerController2DData.HealthAfterPowerUp = powerupData.CurrentLevel + 3;
            UpdateUI();
            return;
        }

        //Additional Health
        if (powerupData.PowerupType == 2)
        {
            playerController2DData.SpeedAfterPowerUp = 5 + (1.5f * powerupData.CurrentLevel);
            UpdateUI();
            return;
        }
    }
}
