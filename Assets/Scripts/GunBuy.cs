using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBuy : MonoBehaviour
{
    [SerializeField] int gunCount;
    [SerializeField] int gunPrice;
    [SerializeField] GameObject gun;

    private void OnTriggerEnter(Collider other)
    {
        GameManager gameManager = GameManager.Instance;

        if (gameManager.money >= gunPrice)
        {
            MoneySystem.Instance.MoneyTextRevork(-1 * gunPrice);
            MarketSystem.Instance.GunBuy(gunCount);
            gun.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
