using System.Collections.Generic;
using UnityEngine;

public class ShipLoadController : MonoBehaviour
{
    [SerializeField]
    private ShipController shipController;

    [SerializeField]
    private List<MercancySO> shipLoad = new List<MercancySO>();

    public ShipLoad Load { get; private set; }

    public int RemainingCapacity => Load?.RemainingCapacity ?? 0;

    private void Awake()
    {
        if (shipController == null)
        {
            shipController = GetComponent<ShipController>();
        }

        int capacity = shipController != null && shipController.shipData != null
            ? shipController.shipData.shipCapacity
            : 0;

        Load = new ShipLoad(capacity);
    }

    private void Start()
    {
        RefreshCapacityFromShip();
        InitializeFromSerializedList();
    }

    public bool TryLoadFromMarket(Market market, MercancySO mercancy, int quantity)
    {
        if (market == null || mercancy == null || quantity <= 0)
        {
            return false;
        }

        if (!Load.CanFit(mercancy, quantity))
        {
            return false;
        }

        if (!market.TryTake(mercancy, quantity))
        {
            return false;
        }

        return Load.TryAddCargo(mercancy, quantity);
    }

    public bool TryUnloadToMarket(Market market, MercancySO mercancy, int quantity)
    {
        if (market == null || mercancy == null || quantity <= 0)
        {
            return false;
        }

        if (!Load.TryRemoveCargo(mercancy, quantity))
        {
            return false;
        }

        market.Restock(mercancy, quantity);
        return true;
    }

    public int GetQuantity(MercancySO mercancy)
    {
        if (Load == null || mercancy == null)
        {
            return 0;
        }

        return Load.GetQuantity(mercancy);
    }

    public void RefreshCapacityFromShip()
    {
        int newCapacity = shipController != null && shipController.shipData != null
            ? shipController.shipData.shipCapacity
            : Load?.Capacity ?? 0;

        if (Load == null)
        {
            Load = new ShipLoad(newCapacity);
            return;
        }

        if (Load.Capacity == newCapacity)
        {
            return;
        }

        ShipLoad previous = Load;
        Load = new ShipLoad(newCapacity);

        foreach (CargoStack stack in previous.CargoStacks)
        {
            if (!Load.TryAddCargo(stack.Mercancy, stack.Quantity))
            {
                Debug.LogWarning($"Cargo {stack.Mercancy?.MercancyName} exceeds new capacity and was dropped.", this);
            }
        }
    }

    private void InitializeFromSerializedList()
    {
        if (shipLoad == null || shipLoad.Count == 0 || Load == null)
        {
            return;
        }

        foreach (MercancySO mercancy in shipLoad)
        {
            if (mercancy == null)
            {
                continue;
            }

            int quantity = mercancy.MercancyCount > 0 ? mercancy.MercancyCount : 1;
            if (!Load.TryAddCargo(mercancy, quantity))
            {
                Debug.LogWarning($"Unable to load initial cargo {mercancy.MercancyName} due to capacity limits.", this);
            }
        }

        shipLoad.Clear();
    }
}
