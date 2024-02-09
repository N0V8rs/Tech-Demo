using Unity.VisualScripting;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int coinValue = 1; 
    public AudioClip collectSound; 
    public float rotationSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject collector)
    {
      if (collectSound != null)
      {
          AudioSource.PlayClipAtPoint(collectSound, transform.position);
      }

        CoinCounter coinCounter = collector.GetComponent<CoinCounter>();
        if (coinCounter != null)
        {
            coinCounter.IncrementCoinCount(coinValue);
        }
        Destroy(gameObject);
    }
}
