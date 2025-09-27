using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MarketManager : MonoBehaviour {

    public List<MercancySO> market;

    public RectTransform marketPanel;
    public GameObject newLinePrefab;

    public int selectedMercancyID;
    public Text selectedMercancyIDText;
    public Mercancy selectedMercancy;
    public Text selectedMercancyName;
    public Text selectedMercancyVolume;

    float panelHeigth;
    public Text[] UItexts;

    private void Start()
    {
        //   market = new mercancy[5];

        if (market.Count > 0) {

            for (int i = 0; i < market.Count; i++)

            {
                GameObject newLine = Instantiate(newLinePrefab, marketPanel.position, marketPanel.rotation);

                newLine.transform.SetParent(marketPanel.transform);
                newLine.transform.Translate(0, -50 - (30 * i), 0);

                UItexts = newLine.GetComponentsInChildren<Text>();

                UItexts[0].text = market[i].MercancyName;
                UItexts[1].text = market[i].MercancyID.ToString() ;
                UItexts[2].text = market[i].MercancyCount.ToString();
                UItexts[3].text = market[i].MercancyVolume.ToString();

                // panelHeigth = marketPanel.position.y;
                // panelHeigth += 50;
            }



         //   UItexts[] = newLine.GetComponentsInChildren<Text>().ToString();


        }


    }

}
