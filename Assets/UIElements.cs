using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    public Text coinText; // Reference to the UI text element
    private int coinCount = 0; // Counter for the number of coins collected

    void Start()
    {
        // Initialize coin count text
        UpdateCoinText();
    }

    // Update the UI text element to display the current coin count
    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
    }

    // Call this method whenever the player collects a coin to increment the coin count
    public void IncrementCoinCount(int value)
    {
        coinCount += value;
        UpdateCoinText();
    }
}
