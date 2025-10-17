using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour
{
    [SerializeField]
    private List<MercancySO> market = new List<MercancySO>();

    public Market ActiveMarket { get; private set; }

    public RectTransform marketPanel;
    public GameObject newLinePrefab;

    public int selectedMercancyID;
    public Text selectedMercancyIDText;
    public Mercancy selectedMercancy;
    public Text selectedMercancyName;
    public Text selectedMercancyVolume;

    private readonly List<GameObject> instantiatedRows = new List<GameObject>();

    private void Awake()
    {
        ActiveMarket = new Market(market);
    }

    private void Start()
    {
        PopulateUI();
    }

    public void RefreshUI()
    {
        ClearUIRows();
        PopulateUI();
    }

    private void PopulateUI()
    {
        if (marketPanel == null || newLinePrefab == null || ActiveMarket == null)
        {
            return;
        }

        IReadOnlyList<MarketStock> stock = ActiveMarket.Stock;
        for (int i = 0; i < stock.Count; i++)
        {
            MarketStock entry = stock[i];
            MercancySO mercancy = entry.Mercancy;
            if (mercancy == null)
            {
                continue;
            }

            GameObject newLine = Instantiate(newLinePrefab, marketPanel.position, marketPanel.rotation);
            newLine.transform.SetParent(marketPanel.transform);
            newLine.transform.Translate(0, -50 - (30 * i), 0);

            Text[] uiTexts = newLine.GetComponentsInChildren<Text>();
            if (uiTexts.Length >= 4)
            {
                uiTexts[0].text = mercancy.MercancyName;
                uiTexts[1].text = mercancy.MercancyID.ToString();
                uiTexts[2].text = entry.Quantity.ToString();
                uiTexts[3].text = mercancy.MercancyVolume.ToString();
            }

            instantiatedRows.Add(newLine);
        }
    }

    private void ClearUIRows()
    {
        for (int i = 0; i < instantiatedRows.Count; i++)
        {
            if (instantiatedRows[i] != null)
            {
                Destroy(instantiatedRows[i]);
            }
        }

        instantiatedRows.Clear();
    }
}
