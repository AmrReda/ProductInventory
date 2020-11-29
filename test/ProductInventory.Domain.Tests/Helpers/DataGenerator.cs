using System.Collections.Generic;
using ProductInventory.Domain.Models;

namespace ProductInventory.Domain.Tests.Helpers
{
    public static class DataGenerator
    {
        private static readonly Product productA = new Product("WooliesX Product A", 99.99, 0);
        private static readonly Product productB = new Product("WooliesX Product B", 101.99, 0);
        private static readonly Product productC = new Product("WooliesX Product C", 16.59, 0);
        private static readonly Product productD = new Product("WooliesX Product D", 5, 0);
        private static readonly Product productF = new Product("WooliesX Product F", 999999999999, 0);
        
        
        public static readonly List<Product> NotSortedProductsFormLowToHigh = new List<Product>
        {
            productB,
            productA,
            productC,
            productD,
            productF
        };



        public static readonly List<Product> SortedProductsFormLowToHigh = new List<Product>
        {
            productD,
            productC,
            productA,
            productB,
            productF
        };

        public static readonly List<Product> SortedProductsFormHighToLow = new List<Product>
        {
            productF,
            productB,
            productA,
            productC,
            productD
        };

        public static readonly List<Product> SortedProductsAscending = new List<Product>
        {
            productA,
            productB,
            productC,
            productD,
            productF
        };

        public static readonly List<Product> SortedProductsDescending = new List<Product>
        {
            productF,
            productD,
            productC,
            productB,
            productA
        };
        
        public static readonly List<Product> SortedProductsBasedOnRecommended = new List<Product>
        {
            productB,
            productA,
            productC,
            productD,
            productF
        };
    }
}