// 08/10/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Port", menuName = "Port Data", order = 51)]
[System.Serializable]
public class PortSO : ScriptableObject
{
    public string portName;
    public Vector3 portLocation;
    public List<Product> productList;

    [System.Serializable]
    public class Product
    {
        public string productName;
        public int productPrice;
    }
}