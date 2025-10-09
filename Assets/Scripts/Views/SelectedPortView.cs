using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectedPortView : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private TextMeshProUGUI portNameText;

    // Actualiza la vista del puerto seleccionado con el nombre del puerto seleccionado

    public void UpdateSelectedPortView(PortSO selectedPortData)
    {
        if (selectedPortData != null)
        {
            portNameText.text = selectedPortData.portName;
        }
        else
        {
            portNameText.text = "No Port Selected";
        }
    }
}
