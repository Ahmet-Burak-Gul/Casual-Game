using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BagControllar : MonoBehaviour
{

    [SerializeField] private Transform bagTransform;
    public List<ProductData> productDataList;

    [SerializeField] private TextMeshPro maxText;
    private int maxBagCapacity;

    private Vector3 productSize;
    private string keyBagCapacity = "keyBagCapacity";

    private void Start()
    {
        maxBagCapacity = LoadBagCapacity();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShopPoint"))
        {
            PlayShopSound();
            for (int i = productDataList.Count - 1; i >= 0; i--)
            {
                SellProductToShop(productDataList[i]);
                Destroy(bagTransform.transform.GetChild(i).gameObject);
                productDataList.RemoveAt(i);
            }
            ControlBagCapacity();
        }

        if (other.CompareTag("UnlockBakeryUnit"))
        {
            UnlockedBakeryUnitController bakeryUnit = other.GetComponent<UnlockedBakeryUnitController>();

            ProductType neededType = bakeryUnit.GetNeededProductType();
            PlayShopSound();

            for (int i = productDataList.Count - 1; i >= 0; i--)
            {
                if (productDataList[i].productType == neededType)
                {
                    if (bakeryUnit.IsAvailable())
                    {
                        Destroy(bagTransform.GetChild(i).gameObject);
                        productDataList.RemoveAt(i);
                    }
                }
            }

            StartCoroutine(PutProductIsOrder());
            ControlBagCapacity();
        }
    }

    private void SellProductToShop(ProductData productData)
    {
        CashManager.instance.ExchangeProduct(productData);
    }

    public void AddProductToBag(ProductData productData)
    {
        GameObject boxProduct = Instantiate(productData.productPrefab, Vector3.zero, Quaternion.identity);
        boxProduct.transform.SetParent(bagTransform, true);

        CalculateObjectSize(boxProduct);
        float yPosition = CalculateNewBoxPosition();
        boxProduct.transform.localPosition = new Vector3(0, yPosition, 0);
        boxProduct.transform.localRotation = Quaternion.identity;

        productDataList.Add(productData);
        ControlBagCapacity();
    }

    private float CalculateNewBoxPosition()
    {
        //�r�n�n Sahnedeki y�ksekli�i * �r�n�n adedi.
        float newPosition = productSize.y * productDataList.Count;

        return newPosition;
    }

    private void CalculateObjectSize(GameObject gameObject)
    {
        if (productSize == Vector3.zero)
        {
            MeshRenderer Renderer = gameObject.GetComponent<MeshRenderer>();
            productSize = Renderer.bounds.size;
        }
    }

    private void ControlBagCapacity()
    {
        if (productDataList.Count == maxBagCapacity)
        {
            SetMaxTextOn();
        }
        else
        {
            SetMaxTextOff();
        }
    }

    private void SetMaxTextOn()
    {
        if (!maxText.isActiveAndEnabled)
        {
            maxText.gameObject.SetActive(true);
        }
    }

    private void SetMaxTextOff()
    {
        if (maxText.isActiveAndEnabled)
        {
            maxText.gameObject.SetActive(false);
        }
    }

    public bool IsEmptySpace()
    {
        if (productDataList.Count < maxBagCapacity)
        {
            return true;
        }
        return false;
    }

    private IEnumerator PutProductIsOrder()
    {
        yield return new WaitForSeconds(0.15f);
        for (int i = 0; i < bagTransform.childCount; i++)
        {
            float newYPos = productSize.y * i;
            bagTransform.GetChild(i).localPosition = new Vector3(0, newYPos, 0);
        }
    }

    private void PlayShopSound()
    {
        if (productDataList.Count > 0)
        {
            AudioManager.instance.PlayAudio(AudioClipType.shopClip);
        }
    }

    public void BoostBagCapacity(int boostCount)
    {
        maxBagCapacity += boostCount;
        PlayerPrefs.SetInt(keyBagCapacity,maxBagCapacity);
        ControlBagCapacity();
    }

    private int LoadBagCapacity()
    {
        int bagCapacity = PlayerPrefs.GetInt(keyBagCapacity,5);
        return bagCapacity;
    }
}
