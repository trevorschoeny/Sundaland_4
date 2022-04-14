using System;
using System.Linq;

namespace Sundaland.Models
{
    public interface IPurchaseRepository
    {
        IQueryable<Purchase> Purchases { get; }
        void SavePurchase(Purchase purchase);
    }
}
