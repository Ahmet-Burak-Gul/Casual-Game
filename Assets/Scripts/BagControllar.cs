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

    private void Start()
    {
        maxBagCapacity = 5;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShopPoint"))
        {
            for (int i = productDataList.Count-1; i >= 0; i--)
            {
                SellProductToShop(productDataList[i]);
                Destroy(bagTransform.transform.GetChild(i).gameObject);
                productDataList.RemoveAt(i);
            }
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
        //ürünün Sahnedeki yüksekliði * ürünün adedi.
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
}
