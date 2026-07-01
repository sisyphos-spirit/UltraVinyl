using System;
using TMPro;
using UnityEngine;

/*public class EventTester : MonoBehaviour
{
    private EventBinding<CoinTestEvent> coinEventBinding;
    
    private TextMeshProUGUI coinText;
    private int coins;
    private string basicText = "Coins: ";
    
    void OnEnable()
    {
        coinEventBinding = new EventBinding<CoinTestEvent>(HandleCoinEvent);
        EventBus<CoinTestEvent>.Register(coinEventBinding);
    }

    void OnDisable()
    {
        EventBus<CoinTestEvent>.Deregister(coinEventBinding);
    }

    private void Awake()
    {
        coinText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Moneda insertada");

            EventBus<CoinTestEvent>.Raise(new CoinTestEvent 
            {
                Amount = 1
            });    
        }else if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Moneda retirada");
            
            EventBus<CoinTestEvent>.Raise(new CoinTestEvent 
            {
                Amount = -1
            });    
        }
    }

    private void HandleCoinEvent(CoinTestEvent coinEvent)
    {
        coins += coinEvent.Amount;
        RefreshUI();
    }

    private void RefreshUI()
    {
        coinText.text = basicText + coins;
    }
}*/