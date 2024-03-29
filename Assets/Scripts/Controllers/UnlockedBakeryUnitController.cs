using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockedBakeryUnitController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bakeryText;
    [SerializeField] private int maxStoreProductCount;
    [SerializeField] private ProductType productType;
    private int storeProductCount;

    [SerializeField] private int useProductInSecand = 10;
    [SerializeField] private Transform coinTransform;
    [SerializeField] private GameObject coinGO;

    private float time;

    [SerializeField] private ParticleSystem smokeParticle;

    private string keyBakeryProdact = "keyBakeryProdact";
    void Start()
    {
        storeProductCount = LoadProductInBakery();
        DisplayProductCount();
    }

    void Update()
    {
        if (storeProductCount > 0)
        {
            time += Time.deltaTime;
            if (time >= useProductInSecand)
            {
                time = 0.0f;
                UseProduct();
            }
        }
    }

    private void DisplayProductCount()
    {
        bakeryText.text = storeProductCount.ToString() + "/" + maxStoreProductCount.ToString();
        ControlSmokeEffect();
        SaveProductInbakery(storeProductCount);
    }

    public ProductType GetNeededProductType()
    {
        return productType;
    }

    public bool IsAvailable()
    {
        if (storeProductCount == maxStoreProductCount)
        {
            return false;
        }

        storeProductCount++;
        DisplayProductCount();
        return true;
    }

    private void UseProduct()
    {
        storeProductCount--;
        DisplayProductCount();
        CreatCoin();
    }

    private void CreatCoin()
    {
        Vector3 randomPosition = Random.insideUnitSphere * 1f;
        Vector3 InstantiatePos = coinTransform.position + randomPosition;
        Instantiate(coinGO, InstantiatePos, Quaternion.identity);
    }

    private void ControlSmokeEffect()
    {
        if (storeProductCount==0)
        {
            if (smokeParticle.isPlaying)
            {
                smokeParticle.Stop();
            }
        }
        else
        {
            if (smokeParticle.isStopped)
            {
                smokeParticle.Play();
            }
        }
    }

    private void SaveProductInbakery(int prodactCount)
    {
        PlayerPrefs.SetInt(keyBakeryProdact, prodactCount);
    }

    private int LoadProductInBakery()
    {
        int productCount = PlayerPrefs.GetInt(keyBakeryProdact,0);
        return productCount;
    }
}
