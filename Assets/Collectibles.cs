using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int coinValue = 1; // Value of the collectible (e.g., how many coins it's worth)
    public AudioClip collectSound; // Sound played when collected

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject collector)
    {
        // Play collect sound
      //if (collectSound != null)
      //{
      //    AudioSource.PlayClipAtPoint(collectSound, transform.position);
      //}

        // Update coin count in UI
        CoinCounter coinCounter = collector.GetComponent<CoinCounter>();
        if (coinCounter != null)
        {
            coinCounter.IncrementCoinCount(coinValue);
        }

        // Destroy the collectible object
        Destroy(gameObject);
    }
}
