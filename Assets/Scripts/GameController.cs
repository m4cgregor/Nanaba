// 27/09/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController gameController { get; private set; }

    [Header("Player Data")]
    public string playerName;
    public Text playerNameText;
    public int money;
    public Text moneyText;

    [Header("Ports Array")]
    public PortController[] ports;

    [Header("Selected Ship Data")]
    public Text selectedShipName;
    public Text selectedShipDestination;
    public Text selectedShipState;
    public Text selectedShipFuel;
    public Text messageText;

    private void Awake()
    {
        if (gameController == null)
        {
            gameController = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerNameText.text = playerName;
        UpdateUI();
    }

    public void UpdateUI()
    {
       

        UpdateMoney();
    }

    private void UpdateMoney()
    {
        moneyText.text = money.ToString();
    }

    public void MessageUI(string message)
    {
        messageText.text = message + messageText.text;
        UpdateUI();
    }
}