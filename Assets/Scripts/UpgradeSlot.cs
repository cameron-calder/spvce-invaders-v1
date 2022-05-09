using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
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
        CostText.text = "UPGRADE\n" + powerupData.ScoreCost.ToString("D2") + " SCORE";
        Icon.sprite = powerupData.Icon;

        if (powerupData.Owned == true)
            CostText.text = "OWNED";
    }

    public void Buy()
    {
        if (powerupData.Owned == true)
            return;

        bool CanBuy = false;
        for (int i = 0; i < scoreManager.sd.scores.Count; i++)
        {
            if (scoreManager.sd.scores[i].score >= powerupData.ScoreCost)
                CanBuy = true;
        }

        if (CanBuy == false)
            return;

        powerupData.Owned = true;

        //Rapid Fire
        if (powerupData.PowerupType == 0)
        {
            playerController2DData.FireRateAfterPowerUp = 0.5f;
            UpdateUI();
            return;
        }

        //Additional Health
        if (powerupData.PowerupType == 1)
        {
            playerController2DData.HealthAfterPowerUp = 4;
            UpdateUI();
            return;
        }

        //Additional Health
        if (powerupData.PowerupType == 2)
        {
            playerController2DData.SpeedAfterPowerUp = 7;
            UpdateUI();
            return;
        }
    }
}
