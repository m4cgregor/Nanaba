using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortController : MonoBehaviour
{

    public string portName;
    public Transform portLocation;

    public class product
    {

        public string productName;
        public int productPrice;

    }

    public product[] productList;

    // Use this for initialization
    void Start()

    {
        portLocation = GetComponent<Transform>();
        
    }

  public void OnTriggerEnter(Collider other)
    {
        Debug.Log("he");

        if (other.gameObject.CompareTag("Ship"))

        {
            Debug.Log("Ship Here");
            other.GetComponent<ShipController>().PortArrival();

        }

    } 
}
