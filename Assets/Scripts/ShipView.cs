// 27/09/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.UI;

public class ShipView : MonoBehaviour
{
    public Text shipNameText;
    public Text shipFuelText;
    public Text shipDestinationText;
    public Renderer shipRenderer;

    public void UpdateUI(ShipSO shipData)
    {
        shipNameText.text = shipData.shipName;
        shipFuelText.text = shipData.fuel.ToString();
        shipDestinationText.text = shipData.destinationPort != null
            ? shipData.destinationPort.name
            : "No destination";
    }

    public void UpdateVisuals(Color color)
    {
        if (shipRenderer != null)
        {
            shipRenderer.material.color = color;
        }
    }
}