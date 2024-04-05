using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private int coinCount = 0;
    public TextMeshProUGUI coinCountText; // Reference to the TextMeshProUGUI component

    public void IncrementCoinCount(int increment)
    {
        coinCount += increment;
        coinCountText.text = "Score: " + coinCount; // Update the text
    }
}
