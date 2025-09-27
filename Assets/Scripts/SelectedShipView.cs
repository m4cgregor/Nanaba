// 27/09/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.UI;

public class SelectedShipView : MonoBehaviour
{
    [Header("UI Elements")]
    public Text shipNameText;
    public Text shipDestinationText;
    public Text shipStateText;
    public Text shipFuelText;

 
    public static SelectedShipView Instance { get; internal set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SelectedShipController.Instance.ShipSelectedEvent += UpdateUI;
    }

    private void OnDisable()
    {
        SelectedShipController.Instance.ShipSelectedEvent -= UpdateUI;
    }

    

    public void UpdateUI(ShipSO shipData)
    {
        if (shipData != null)
        {
            shipNameText.text = shipData.shipName;

            // Handle null destination port

            Debug.Log("Selected Ship Destination Port: " + (shipData.destinationPort != null ? shipData.destinationPort.name : "None"));

            shipDestinationText.text = shipData.destinationPort != null
                ? shipData.destinationPort.GetComponent<PortController>().portName
                : "No destination";
            shipStateText.text = shipData.myShipState.ToString();
            shipFuelText.text = shipData.fuel.ToString();
        }
        else
        {
            shipNameText.text = "No Ship Selected";
            shipDestinationText.text = "-";
            shipStateText.text = "-";
            shipFuelText.text = "-";
        }
    }
}