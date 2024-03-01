using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    public static CashManager instance;
    private int coins = 0;

    private string keyCoins = "keyCoins";

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

    void Start()
    {
        LoadCash();
        DisplayShow();
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
        SaveCash();
    }

    public int GetCoins()
    {
        return coins;
    }

    private void SaveCash()
    {
        PlayerPrefs.SetInt(keyCoins, coins);
    } 

    private void LoadCash()
    {
        coins = PlayerPrefs.GetInt(keyCoins, 0);
    }
}
