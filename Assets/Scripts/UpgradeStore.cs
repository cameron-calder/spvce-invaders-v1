using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStore : MonoBehaviour
{
    public Text CoinsText;

    void OnEnable()
    {
        UpdateCoinsText();
    }

    public void UpdateCoinsText()
    {
        CoinsText.text = PlayerPrefs.GetInt("Coins").ToString("D2");
    }
}
