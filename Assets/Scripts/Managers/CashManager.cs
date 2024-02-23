using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    public static CashManager instance;
    private int coins = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    public bool TryBuyThisUnit(int price)
    {
        if (GetCoins() >= price)
        {
            SpendCoin(price);
            return true;
        }
        return false;
    }

    public void ExchangeProduct(ProductData productData)
    {
        AddCoin(productData.productPrice);
    }

    public void AddCoin(int price)
    {
        coins += price;
        DisplayShow();
    }

    public void SpendCoin(int price)
    {
        coins -= price;
        DisplayShow();
    }

    public void DisplayShow()
    {
        UIManager.Instance.ShowCoinCountOnScreen(coins);
    }

    public int GetCoins()
    {
        return coins;
    }
}
