// 08/10/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class PortController : MonoBehaviour
{
    public PortSO portData; // Referencia al ScriptableObject
    public PortView portView; // Referencia a la vista

    public void InitializePort()
    {
        portView.UpdateUI(portData);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ship"))
        {
            Debug.Log("Ship arrived at port: " + portData.portName);
            other.GetComponent<ShipController>().PortArrival();
        }
    }
}