// 08/10/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.UI;

public class PortView : MonoBehaviour
{
    public Text portNameText;

    public void UpdateUI(PortSO portData)
    {
        portNameText.text = portData.portName;
        
    }
}