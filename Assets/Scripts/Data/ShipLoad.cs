using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShipLoad
{
    [SerializeField]
    private int capacity;

    [SerializeField]
    private List<CargoStack> cargo = new List<CargoStack>();

    public ShipLoad(int capacity)
    {
        this.capacity = Mathf.Max(0, capacity);
    }

    public ShipLoad(int capacity, IEnumerable<CargoStack> initialCargo) : this(capacity)
    {
        if (initialCargo == null)
        {
            return;
        }

        foreach (CargoStack stack in initialCargo)
        {
            if (stack?.Mercancy == null || stack.Quantity <= 0)
            {
                continue;
            }

            TryAddCargo(stack.Mercancy, stack.Quantity);
        }
    }

    public int Capacity => capacity;

    public IReadOnlyList<CargoStack> CargoStacks => cargo;

    public int UsedCapacity
    {
        get
        {
            int total = 0;
            for (int i = 0; i < cargo.Count; i++)
            {
                total += cargo[i].TotalVolume;
            }

            return total;
        }
    }

    public int RemainingCapacity => Mathf.Max(0, capacity - UsedCapacity);

    public bool CanFit(MercancySO mercancy, int quantity)
    {
        if (mercancy == null || quantity <= 0)
        {
            return false;
        }

        int volumePerUnit = Mathf.Max(0, mercancy.MercancyVolume);
        int requiredVolume = volumePerUnit * quantity;

        return UsedCapacity + requiredVolume <= capacity;
    }

    public bool TryAddCargo(MercancySO mercancy, int quantity)
    {
        if (!CanFit(mercancy, quantity))
        {
            return false;
        }

        CargoStack stack = GetOrCreateStack(mercancy);
        stack.Increase(quantity);
        return true;
    }

    public bool TryRemoveCargo(MercancySO mercancy, int quantity)
    {
        if (mercancy == null || quantity <= 0)
        {
            return false;
        }

        CargoStack stack = FindStack(mercancy);
        if (stack == null)
        {
            return false;
        }

        if (!stack.TryDecrease(quantity))
        {
            return false;
        }

        if (stack.Quantity == 0)
        {
            cargo.Remove(stack);
        }

        return true;
    }

    public int GetQuantity(MercancySO mercancy)
    {
        CargoStack stack = FindStack(mercancy);
        return stack?.Quantity ?? 0;
    }

    public void Clear()
    {
        cargo.Clear();
    }

    private CargoStack FindStack(MercancySO mercancy)
    {
        for (int i = 0; i < cargo.Count; i++)
        {
            if (cargo[i].Mercancy == mercancy)
            {
                return cargo[i];
            }
        }

        return null;
    }

    private CargoStack GetOrCreateStack(MercancySO mercancy)
    {
        CargoStack existing = FindStack(mercancy);
        if (existing != null)
        {
            return existing;
        }

        CargoStack newStack = new CargoStack(mercancy, 0);
        cargo.Add(newStack);
        return newStack;
    }
}

[Serializable]
public class CargoStack
{
    [SerializeField]
    private MercancySO mercancy;

    [SerializeField]
    private int quantity;

    public CargoStack(MercancySO mercancy, int quantity)
    {
        this.mercancy = mercancy;
        this.quantity = Mathf.Max(0, quantity);
    }

    public MercancySO Mercancy => mercancy;

    public int Quantity => quantity;

    public int TotalVolume
    {
        get
        {
            if (mercancy == null)
            {
                return 0;
            }

            return quantity * Mathf.Max(0, mercancy.MercancyVolume);
        }
    }

    internal void Increase(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        quantity += amount;
    }

    internal bool TryDecrease(int amount)
    {
        if (amount <= 0 || amount > quantity)
        {
            return false;
        }

        quantity -= amount;
        return true;
    }
}
