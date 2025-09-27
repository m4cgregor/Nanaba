// 27/09/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class SelectedShipController : MonoBehaviour
{
    public static SelectedShipController Instance { get; private set; }

    [Header("Selected Ship Data")]
    public ShipController selectedShip;

    public delegate void OnShipSelected(ShipSO shipData);
    public event OnShipSelected ShipSelectedEvent;

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

    public void SelectShip(ShipController ship)
    {
        Debug.Log("Selecting Ship: " + ship.shipData.shipName);

        if (selectedShip != null)
        {
            selectedShip.DeselectShip();
        }

        selectedShip = ship;
        selectedShip.SelectShip();

            // Notificar a las vistas con el ShipSO
            ShipSelectedEvent?.Invoke(selectedShip != null ? selectedShip.shipData : null);
    }

    public void DeselectShip()
    {
        if (selectedShip != null)
        {
            selectedShip.DeselectShip();
            selectedShip = null;

            // Notificar que no hay barco seleccionado
            ShipSelectedEvent?.Invoke(null);
        }
    }
}