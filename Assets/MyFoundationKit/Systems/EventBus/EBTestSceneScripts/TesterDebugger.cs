using UnityEngine;

/*public class TesterDebugger : MonoBehaviour
{
    private EventBinding<CoinTestEvent> coinEventBinding;
    
    void OnEnable()
    {
        coinEventBinding = new EventBinding<CoinTestEvent>(HandleCoinEvent);
        EventBus<CoinTestEvent>.Register(coinEventBinding);
    }

    void OnDisable()
    {
        EventBus<CoinTestEvent>.Deregister(coinEventBinding);
    }
    
    private void HandleCoinEvent(CoinTestEvent coinEvent)
    {
        Debug.Log($"Coin Event recibido! Amount: {coinEvent.Amount}");
    }
}*/