// 27/09/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEngine;
using UnityEngine.AI;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    public ShipSO shipData; // Referencia al ScriptableObject
    public ShipView shipView;
    public NavMeshAgent myNavMeshAgent;

    public string message;

    private void Start()
    {
        IntializeShip();
    }

    private void IntializeShip()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        myNavMeshAgent.speed = shipData.shipSpeed;

        // Inicializar el estado desde el ScriptableObject
        shipData.myShipState = ShipSO.ShipState.Arrived;

        shipView.UpdateUI(shipData);
        shipView.UpdateVisuals(shipData.unSelectedColor);
    }

    public void SetDestinationPort(GameObject newDestination)
    {
       
        
        Debug.Log("Setting Destination Port to: " + (newDestination != null ? newDestination.name : "None"));   

        shipData.destinationPort = newDestination;
        myNavMeshAgent.destination = shipData.destinationPort.transform.position;
        myNavMeshAgent.isStopped = false;

        // Actualizar el estado en el ScriptableObject
        shipData.myShipState = ShipSO.ShipState.Travelling;    
        
        shipView.UpdateUI(shipData);

        SelectedShipController.Instance.SelectShip(this); // Notificar al SelectedShipController

      

        SelectedShipView.Instance.UpdateUI(shipData); // Actualizar la UI del barco seleccionado

        InvokeRepeating(nameof(ConsumeFuel), 0f, 0.5f);
    }

    public void PortArrival()
    {
        CancelInvoke(nameof(ConsumeFuel));
        shipData.myShipState = ShipSO.ShipState.Arrived; // Actualizar el estado
        myNavMeshAgent.isStopped = true;
       shipData.destinationPort = null;

        shipView.UpdateUI(shipData);
    }

    private void ConsumeFuel()
    {
        if (shipData.myShipState == ShipSO.ShipState.Travelling)
        {
            shipData.fuel -= shipData.fuelConsumption;
            shipView.UpdateUI(shipData);

            if (shipData.fuel <= 0)
            {
                CancelInvoke(nameof(ConsumeFuel));
                Debug.LogWarning($"{shipData.shipName} has run out of fuel!");
            }
        }
    }

    public void SelectShip()
    {
        shipView.UpdateVisuals(shipData.selectedColor);

    }

    public void DeselectShip()
    {
        shipView.UpdateVisuals(shipData.unSelectedColor);

    }
}