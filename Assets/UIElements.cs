using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElements : MonoBehaviour
{
    [SerializeField]public Text CoinCounter;
    public int Coins = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CoinCounter.text = "Coins : " + Coins;
    }

    public void CoinCounterUI()
    {
        Coins++;
    }
}
