using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController gameController { get; private set; }
    [Header("Ports Array")]
    public PortController[] ports;
    
    [Header("Ships Array")]
    public GameObject[] ships;
    
    [Header("Selected Ship Data")]
    public int selectedShipID;
    public ShipController selectedShip;
    public Text selectedShipName;
    public Text selectedShipDestination;
    public Text selectedShipState;
    public Text messageText;

    // Use this for initialization
    private void Awake () {

        if (gameController == null)
        {

            gameController = this;
            DontDestroyOnLoad(gameObject);
        }

        else {

            Destroy(gameObject);
        }

        UpdateUI();

    }

    void UpdateUI() {

        selectedShipName.text = selectedShip.shipName;
        selectedShipDestination.text = selectedShip.shipDestinationText.text;
        selectedShipState.text = selectedShip.myShipState.ToString();

    }

    public void MessageUI(string message)
    {

        messageText.text = message + messageText.text;
        UpdateUI();
    }

    public void SetShipDestinationPort(GameObject newPort) {

        selectedShip.destinationPort = newPort.transform;
        selectedShip.myNavMeshAgent.isStopped = false;
        selectedShip.setDestinationPort(newPort.transform);
        MessageUI(selectedShipName.text + " send to " + selectedShip.shipDestinationText.text+".\n");
        UpdateUI();
    }

    public void SelectShip(int selectedID) {

        ships[selectedShipID].GetComponent<ShipController>().SelectedOff();
        selectedShipID = selectedID; // Object ID
        selectedShip = ships[selectedID].GetComponent<ShipController>(); // Data from array
        selectedShip.SelectedOn();
        selectedShipName.text = selectedShip.shipName;
        selectedShipDestination.text = selectedShip.shipDestinationText.text;
        MessageUI(selectedShipName.text + " selected.\n");
        UpdateUI();
    }

    
}
