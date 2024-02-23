using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockedUnitController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int price;

    [Header("Objects")]
    [SerializeField] private TextMeshPro priceText;
    [SerializeField] private GameObject lockedUnit;
    [SerializeField] private GameObject unlockedUnit;

    private bool isPurchased;

    // Start is called before the first frame update
    void Start()
    {
        priceText.text = price.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPurchased)
        {
            UnLockedUnits();
        }
    }


    private void UnLockedUnits()
    {
        if (CashManager.instance.TryBuyThisUnit(price))
        {
            Unlocked();
        }
    }

    private void Unlocked()
    {
        isPurchased = true;
        lockedUnit.SetActive(false);
        unlockedUnit.SetActive(true);
    }
}
