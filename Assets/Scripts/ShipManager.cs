// 27/09/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShipManager : MonoBehaviour
{
    public static ShipManager Instance { get; private set; }

    [Header("Ship Prefabs and UI")]
    public GameObject shipPrefab;
    public GameObject buttonPrefab;
    public GameObject shipListVerticalLayout;
    public ShipSO shipSOBase; // <-- Añade esta referencia en el inspector

    [Header("Ship Data")]
    public List<GameObject> shipList = new List<GameObject>();
    public int shipCost;

    [Header("Selected Ship Data")]
    public int selectedShipID;
    public ShipController selectedShip;

    private GameObject selectedButton;
    private Button oldButton;

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

    public void CreateShip()
    {
        // Instanciar nuevo barco
        GameObject newShip = Instantiate(shipPrefab, transform.position, transform.rotation);
        shipList.Add(newShip);
        int newShipID = shipList.Count - 1;

        // Crear una nueva instancia de ShipSO y asignarla
        ShipSO newShipData = ScriptableObject.Instantiate(shipSOBase);
        newShipData.shipID = newShipID;
        newShipData.shipName = "Ship " + newShipID.ToString();

        // Inicializar el ShipController con el nuevo ShipSO
        ShipController newShipController = newShip.GetComponent<ShipController>();
        newShipController.shipData = newShipData;
        newShip.name = newShipData.shipName;

        // Notificar al GameController sobre el costo
        GameController.gameController.money -= shipCost;
        GameController.gameController.MessageUI($"{newShip.name} Created.\nShip Cost: ${shipCost}\nCurrent Money: ${GameController.gameController.money}\n");

        // Crear el botón de UI para el nuevo barco
        CreateNewShipButton(newShip, newShipID);
    }

    private void CreateNewShipButton(GameObject newShip, int newShipID)
    {
        GameObject newButton = Instantiate(buttonPrefab, transform.position, transform.rotation);
        newButton.transform.SetParent(shipListVerticalLayout.transform, false);
        newButton.name = newShip.name + " button";
        newButton.GetComponentInChildren<Text>().text = newShip.name;

        int capturedID = newShipID;
        newButton.GetComponent<Button>().onClick.AddListener(() => ShipManager.Instance.SelectShip(capturedID));
    }

    public void SelectShip(int selectedID)
    {
        selectedShip = shipList[selectedID].GetComponent<ShipController>();
        SelectedShipController.Instance.SelectShip(selectedShip);
    }

    public void SetSelectedShipDestination(GameObject destination)
    {
        if (selectedShip == null) return;
        selectedShip.SetDestinationPort(destination);
    }


}