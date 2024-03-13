using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private PowerUpData powerUpData;
    [SerializeField] private int lockedUnitID;
    private bool isPowerUpUsed;

    private string powerUpStatusKey = "powerUpStatusKey";

    private void Start()
    {
        isPowerUpUsed = GetPowerUpStatus();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (powerUpData.powerUpType == PowerUpType.bagBooster && !isPowerUpUsed)
            {
                isPowerUpUsed = true;
                BagControllar bagControllar = other.GetComponent<BagControllar>();
                AudioManager.instance.PlayAudio(AudioClipType.shopClip);

                bagControllar.BoostBagCapacity(powerUpData.boostCount);
                PlayerPrefs.SetString(powerUpStatusKey,"used");

            }
        }
    }
    private bool GetPowerUpStatus()
    {
        string status = PlayerPrefs.GetString(powerUpStatusKey, "ready");
        if (status.Equals("ready"))
        {
            return false;
        }
        return true;
    }
}
