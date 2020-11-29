using System;
using System.Collections.Generic;
using System.Linq;
using ProductInventory.Domain.Models;

namespace ProductInventory.Domain.Queries.CalculateTrolley
{
    public class CalculateTrolleyQueryHandler
    {
        public double Handle(CalculateTrolleyQuery query)
        {
            ValidateQueryParameters(query);

            var (appliedSpecials, remainingPurchasedQuantities) = UpdateQuantitiesWithSpecials(query.Specials, query.Quantities);
            return SumTotalPrice(query.Products, appliedSpecials, remainingPurchasedQuantities);
        }

        private static void ValidateQueryParameters(CalculateTrolleyQuery query)
        {
            if(query.Products == null || query.Quantities == null || query.Specials == null)
                throw new ArgumentNullException();
            
            if (query.Quantities.GroupBy(quantity => quantity.Name).Any(grouping => grouping.Count() > 1))
                throw new ArgumentException("Duplicate purchased product name not allowed.");
        }

        private static (List<Special> appliedSpecials, List<Quantity> quantitiesPurchased) UpdateQuantitiesWithSpecials(
            List<Special> specialsToApply, 
            List<Quantity> quantitiesPurchased
            )
        {
            var appliedSpecials = new List<Special>();
            var specialApplied = true;
            
            specialsToApply.ForEach(special =>
            {
                do
                {
                    special.Quantities.ForEach(portionQuantityFromSpecial =>
                    {
                        if (!DoesPortionOfSpecialApply(quantitiesPurchased, portionQuantityFromSpecial))
                        {
                            specialApplied = false;
                        }
                    });

                    if (specialApplied)
                    {
                        appliedSpecials.Add(special);
                        ReducePurchasedQuantitiesBySpecialQuantities(quantitiesPurchased, special);
                    }
                } while (specialApplied);
            });

            return (appliedSpecials, quantitiesPurchased);
        }

        private static double SumTotalPrice(
            IEnumerable<Product> productsCatalog,
            IEnumerable<Special> appliedSpecials,
            IEnumerable<Quantity> quantitiesPurchased
        )
        {
            var totalSpecial = appliedSpecials.Sum(special => special.Total);
            var totalRemainingTotal = quantitiesPurchased.Sum(quantityPurchased =>
            {
                return productsCatalog.Single(product => product.Name == quantityPurchased.Name).Price *
                       quantityPurchased.Value;
            });

            return totalSpecial + totalRemainingTotal;
        }

        private static void ReducePurchasedQuantitiesBySpecialQuantities(List<Quantity> quantitiesPurchased, Special special)
        {
            special.Quantities.ForEach(quantity =>
            {
                var foundPurchasedQuantity = quantitiesPurchased.Single(product => product.Name == quantity.Name);
                foundPurchasedQuantity.Value = foundPurchasedQuantity.Value - quantity.Value;
            });
        }

        private static bool DoesPortionOfSpecialApply(List<Quantity> quantitiesPurchased, Quantity quantity)
        {
            var foundPurchasedProduct = quantitiesPurchased.SingleOrDefault(product => product.Name == quantity.Name);
            return foundPurchasedProduct != null && !(foundPurchasedProduct.Value < quantity.Value);
        }

    }
}