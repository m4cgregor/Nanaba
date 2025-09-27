using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ShipController : MonoBehaviour {

    [Header("Ship Details")]
    [Tooltip("Detalles del barco")]
    public string shipName;
    [Range(1f, 5f)]
    public float shipSpeed;
    public NavMeshAgent myNavMeshAgent;
    public enum shipState {

        travelling,
        arrived,
        loading,
        unLoading,

    };
    public Color selectedColor;
    public Color unSelectedColor;
    public Material shipMaterial;
    public shipState myShipState;
    [Header("Ship UI")]
    public Text shipNameText;
    public Text shipDestinationText;

    [Header("Port Details")]    
    public Transform destinationPort;

    public string message;

    // Use this for initialization
    void Start () {

        myShipState = shipState.travelling;
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        shipNameText = GetComponentInChildren<Text>();
        shipMaterial = GetComponentInChildren<Renderer>().material;

        myNavMeshAgent.speed = shipSpeed;
        shipUI();
        

		
	}

   public void setDestinationPort(Transform newDestination) {

        myNavMeshAgent.destination = newDestination.position;
        myShipState = shipState.travelling;
        myNavMeshAgent.isStopped = false;
        shipUI();
    }

    public void shipUI() {

        shipNameText.text = shipName;

        if (destinationPort)

        {
            shipDestinationText.text = destinationPort.gameObject.GetComponent<PortController>().name;
        }

    }

    public void PortArrival()

    {
        message = (shipName + " arrived to " + shipDestinationText.text+ ".\n");
        myShipState = shipState.arrived;
        myNavMeshAgent.isStopped = true;
        GameController.gameController.MessageUI(message);

    }

    public void SelectedOn() {

        shipMaterial.color = selectedColor;
    }

    public void SelectedOff()

    {
        shipMaterial.color = unSelectedColor;
    }
}
