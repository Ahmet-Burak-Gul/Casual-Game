using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProductPlantController : MonoBehaviour
{
    private bool isReadyToPick;
    private Vector3 originalScale;

    [SerializeField] private ProductData productData;
    private BagControllar bagController;

    // Start is called before the first frame update
    void Start()
    {
        isReadyToPick = true;
        originalScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isReadyToPick)
        {
            bagController = other.GetComponent<BagControllar>();
            bagController.AddProductToBag(productData.productPrefab);

            isReadyToPick = false;
            StartCoroutine(ProductPicked());
        }
    }

    IEnumerator ProductPicked()
    {
        float duration = 0.5f;
        float timer = 0;

        Vector3 targetScale = originalScale / 3;

        while (timer < duration)
        {
            float T = timer / duration;
            transform.localScale = Vector3.Lerp(originalScale , targetScale, T);
            timer += Time.deltaTime;

            yield return null;
        }
        //Fide küçüldü
        yield return new WaitForSeconds(5f);

        timer = 0;
        float growBackDuration = 1f;

        while (timer < growBackDuration)
        {
            float T = (timer / growBackDuration);
            transform.localScale = Vector3.Lerp(targetScale, originalScale, T);
            timer += Time.deltaTime;

            yield return null;
        }
        //Fidan büyüdü
        transform.localScale = originalScale;
        isReadyToPick = true;

        yield return null;
    }
}
