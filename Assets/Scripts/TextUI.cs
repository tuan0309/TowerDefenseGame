using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;

    void Update()
    {
        moneyText.text = "$" + PlayerStats.Money.ToString();
        livesText.text = PlayerStats.Lives.ToString();
    }
}
