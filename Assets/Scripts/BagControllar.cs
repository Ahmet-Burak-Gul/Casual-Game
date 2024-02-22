using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagControllar : MonoBehaviour
{

    [SerializeField] private Transform bagTransform;
    public List<GameObject> productList;

    private Vector3 productSize;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShopPoint"))
        {
            for (int i = productList.Count-1; i >= 0; i--)
            {
                Destroy(bagTransform.transform.GetChild(i).gameObject);
                productList.RemoveAt(i);
            }
        }
    }

    public void AddProductToBag(GameObject boxGo)
    {
        GameObject boxProduct = Instantiate(boxGo, Vector3.zero, Quaternion.identity);
        boxProduct.transform.SetParent(bagTransform, true);

        CalculateObjectSize(boxProduct);
        float yPosition = CalculateNewBoxPosition();
        boxProduct.transform.localPosition = new Vector3(0, yPosition, 0);
        boxProduct.transform.localRotation = Quaternion.identity;

        productList.Add(boxProduct);
    }

    private float CalculateNewBoxPosition()
    {
        //ürünün Sahnedeki yüksekliði * ürünün adedi.
        float newPosition = productSize.y * productList.Count;

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
}
