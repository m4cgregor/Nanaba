// 27/09/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// add this to create a new ScriptableObject via the Unity Editor

[CreateAssetMenu(fileName = "New Ship", menuName = "Ship Data", order = 51)]

[System.Serializable]
public class ShipSO : ScriptableObject
{
    public int shipID;
    public string shipName;
    public int fuel;
    public int maxFuel;
    public int fuelConsumption;
    public float shipSpeed;
    public int shipCapacity;
    public Color selectedColor;
    public Color unSelectedColor;

    // Nuevo campo para el estado del barco
    public ShipState myShipState;
    public PortController destinationPort;

    // Enum para los estados del barco
    public enum ShipState
    {
        Travelling,
        Arrived,
        Loading,
        Unloading
    }
}