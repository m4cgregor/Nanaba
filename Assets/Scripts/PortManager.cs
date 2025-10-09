// 08/10/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class PortManager : MonoBehaviour
{
    [SerializeField] private PortController[] ports; // Lista de puertos
    [SerializeField] private GameObject buttonPrefab; // Prefab del botón
    [SerializeField] private Transform buttonParent; // Contenedor de los botones
    [SerializeField] private SelectedPortView selectedPortView; // Referencia al SelectedPortView

    private void Awake()
    {
        // Busca todos los puertos en la escena
        ports = Object.FindObjectsByType<PortController>(FindObjectsSortMode.InstanceID);

        // Crea un botón para cada puerto
        foreach (var port in ports)
        {
            CreatePortButton(port);
        }
    }

    private void CreatePortButton(PortController port)
    {
        // Instanciar el botón
        GameObject newButton = Instantiate(buttonPrefab, buttonParent);

        // Configurar el texto del botón
        var buttonText = newButton.GetComponentInChildren<UnityEngine.UI.Text>();
        buttonText.text = port.portData.portName;

        // Asignar la funcionalidad al botón
        var button = newButton.GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(() => OnPortSelected(port));
    }

    private void OnPortSelected(PortController port)
    {
        Debug.Log($"Port selected: {port.portData.portName}");
        selectedPortView.UpdateSelectedPortView(port.portData); // Enviar la data al SelectedPortView
    }
}