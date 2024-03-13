using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType { bagBooster };
[CreateAssetMenu(fileName = "Power Up Data", menuName = "Scriptable Object/Power Up Data", order = 1)]
public class PowerUpData : ScriptableObject
{
    public PowerUpType powerUpType;
    public int boostCount;
}
