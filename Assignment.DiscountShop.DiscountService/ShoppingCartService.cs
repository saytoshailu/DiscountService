﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Assignment.DiscountShop.Contracts;
using Assignment.DiscountShop.Models;

namespace Assignment.DiscountShop.DiscountShopService
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IDiscountService _discountService;
        private readonly List<ShoppingCart> _shoppingCarts = new List<ShoppingCart>();

        public ShoppingCartService(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        public void AddItem(ShoppingCart shoppingCart, Product product, int count)
        {
            var item = new KeyValuePair<Product, int>();
            if (shoppingCart.CartItems != null)
                item = shoppingCart.CartItems.Find(ci => ci.Key.Id == product.Id);
            else
                shoppingCart.CartItems = new List<KeyValuePair<Product, int>>();

            if (item.Equals(new KeyValuePair<Product, int>()))
            {
                shoppingCart.CartItems.Add(new KeyValuePair<Product, int>(product, count));
            }
            else
            {
                shoppingCart.CartItems.Remove(item);
                shoppingCart.CartItems.Add(new KeyValuePair<Product, int>(product, count));
            }
        }

        public void RemoveItem(ShoppingCart shoppingCart, Product product, int count)
        {
            var item = new KeyValuePair<Product, int>();

            if (shoppingCart.CartItems != null)
                item = shoppingCart.CartItems.First(ci => ci.Key.Id == product.Id);

            if (!item.Equals(new KeyValuePair<Product, int>()))
                shoppingCart.CartItems.Remove(item);
        }

        public ShoppingCart ComputeBill(ShoppingCart shoppingCart)
        {
            var applicableDiscounts = GetAllPossibleDiscountsApplied(shoppingCart);
            var shoppingCartClone = shoppingCart.Clone();

            foreach (var applicableDiscount in applicableDiscounts)
                ApplyDiscount(shoppingCartClone, applicableDiscount);

            shoppingCart.TotalBillAmount = shoppingCart.CartItems.Select(ci => ci.Key.CostPerUnit * ci.Value).Sum();
            shoppingCart.TotalDiscountAmount = shoppingCartClone.TotalDiscountAmount;
            shoppingCart.DiscountsApplied = shoppingCartClone.DiscountsApplied;

            return shoppingCart;
        }

        public ShoppingCart Checkout(ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }

        public ShoppingCart CreateShoppingCart(int id, int customerId)
        {
            var customerShoppingCart = _shoppingCarts.Where(sc => sc.CustomerId == customerId).Select(sc => sc);

            if (customerShoppingCart.Count() == 1)
                return customerShoppingCart.First();
            if (customerShoppingCart.Count() > 1)
                throw new InvalidDataException("more than one shopping carts exist for one customer");

            var maxId = _shoppingCarts.Select(sc => sc.Id)
                .DefaultIfEmpty(0).Max();

            _shoppingCarts.Add(new ShoppingCart(++maxId, customerId));
            return _shoppingCarts[--maxId];
        }

        private void ApplyDiscount(ShoppingCart shoppingCartClone,
            Discount discount)
        {
            var discountCombinationItems = discount.DiscountCombinationItems;
            decimal discountAmount = 0;

            var itemsMarkedForDiscount = new List<KeyValuePair<Product, int>>();

            var eligibleForDiscount = false;
            foreach (var itemsCountsCombination in discountCombinationItems.ItemsCountsCombinations)
            {
                var shoppedItem = shoppingCartClone.CartItems
                    .Where(ci => ci.Key == itemsCountsCombination.Key)
                    .Select(ci => ci)
                    .Single();

                if (shoppedItem.Equals(new KeyValuePair<Product, int>()))
                {
                    eligibleForDiscount = false;
                    break;
                }

                if (shoppedItem.Value >= itemsCountsCombination.Value)
                {
                    eligibleForDiscount = true;
                    itemsMarkedForDiscount.Add(shoppedItem);
                    var prod = shoppedItem.Key;
                    shoppingCartClone.CartItems.Remove(shoppedItem);
                    var remainingCount = shoppedItem.Value - itemsCountsCombination.Value;
                    shoppingCartClone.CartItems.Add(new KeyValuePair<Product, int>(prod, remainingCount));
                }
            }

            if (eligibleForDiscount)
            {
                discountAmount = discountCombinationItems.DiscountAmount;
                var ds = shoppingCartClone.DiscountsApplied.Find(s => s.Key == discount);

                if (ds.Equals(new KeyValuePair<Discount, decimal>()))
                {
                    shoppingCartClone.DiscountsApplied.Add(
                        new KeyValuePair<Discount, decimal>(discount, discountAmount));
                }
                else
                {
                    discountAmount = ds.Value;
                    shoppingCartClone.DiscountsApplied.Remove(ds);
                    shoppingCartClone.DiscountsApplied.Add(
                        new KeyValuePair<Discount, decimal>(discount, discountAmount));
                }
            }
            else //reverse and restore
            {
                itemsMarkedForDiscount.ForEach(it =>
                {
                    shoppingCartClone.CartItems.Remove(shoppingCartClone.CartItems.Find(p => p.Key == it.Key));
                    shoppingCartClone.CartItems.Add(it);
                });
            }

            if (discountAmount == 0) return;

            shoppingCartClone.TotalDiscountAmount += discountAmount;
            ApplyDiscount(shoppingCartClone, discount);
        }

        private IEnumerable<Discount> GetAllPossibleDiscountsApplied(ShoppingCart sc)
        {
            var discounts = _discountService.GetAll().Where(d => d.Status == DiscountStatus.Active)
                .ToList();
            var applicaDiscounts = new List<Discount>();

            var products = sc.CartItems.Select(ci => ci.Key);

            foreach (var product in products)
            {
                var discountsHavingProduct = discounts
                    .Where(d => d.DiscountCombinationItems
                        .ItemsCountsCombinations
                        .Any(ic => ic.Key == product))
                    .Select(disc => disc);

                applicaDiscounts.AddRange(discountsHavingProduct);
            }

            return applicaDiscounts.Distinct();
        }
    }
}