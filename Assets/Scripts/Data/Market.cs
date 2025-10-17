using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Market
{
    [SerializeField]
    private List<MarketStock> stock = new List<MarketStock>();

    private readonly Dictionary<MercancySO, MarketStock> lookup = new Dictionary<MercancySO, MarketStock>();

    public Market(IEnumerable<MercancySO> initialStock)
    {
        if (initialStock == null)
        {
            return;
        }

        foreach (MercancySO mercancy in initialStock)
        {
            if (mercancy == null)
            {
                continue;
            }

            AddStock(mercancy, Mathf.Max(0, mercancy.MercancyCount));
        }
    }

    public IReadOnlyList<MarketStock> Stock => stock;

    public bool HasStock(MercancySO mercancy, int quantity)
    {
        if (mercancy == null || quantity <= 0)
        {
            return false;
        }

        MarketStock marketStock = FindStock(mercancy);
        return marketStock != null && marketStock.Quantity >= quantity;
    }

    public bool TryTake(MercancySO mercancy, int quantity)
    {
        if (!HasStock(mercancy, quantity))
        {
            return false;
        }

        MarketStock marketStock = lookup[mercancy];
        marketStock.TryDecrease(quantity);

        if (marketStock.Quantity == 0)
        {
            stock.Remove(marketStock);
            lookup.Remove(mercancy);
        }

        return true;
    }

    public void Restock(MercancySO mercancy, int quantity)
    {
        if (mercancy == null || quantity <= 0)
        {
            return;
        }

        AddStock(mercancy, quantity);
    }

    public int GetQuantity(MercancySO mercancy)
    {
        MarketStock marketStock = FindStock(mercancy);
        return marketStock?.Quantity ?? 0;
    }

    public void Clear()
    {
        stock.Clear();
        lookup.Clear();
    }

    private void AddStock(MercancySO mercancy, int quantity)
    {
        MarketStock marketStock = GetOrCreateStock(mercancy);
        marketStock.Increase(quantity);
    }

    private MarketStock GetOrCreateStock(MercancySO mercancy)
    {
        if (lookup.TryGetValue(mercancy, out MarketStock existing))
        {
            return existing;
        }

        MarketStock newStock = new MarketStock(mercancy, 0);
        lookup.Add(mercancy, newStock);
        stock.Add(newStock);
        return newStock;
    }

    private MarketStock FindStock(MercancySO mercancy)
    {
        if (mercancy == null)
        {
            return null;
        }

        lookup.TryGetValue(mercancy, out MarketStock marketStock);
        return marketStock;
    }
}

[Serializable]
public class MarketStock
{
    [SerializeField]
    private MercancySO mercancy;

    [SerializeField]
    private int quantity;

    public MarketStock(MercancySO mercancy, int quantity)
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
