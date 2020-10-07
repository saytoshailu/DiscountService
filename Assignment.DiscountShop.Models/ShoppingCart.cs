﻿using System;
using System.Collections.Generic;

namespace Assignment.DiscountShop.Models
{
    public class ShoppingCart
    {
        public ShoppingCart(int id, int customerId)
        {
            Id = id;
            CustomerId = customerId;
            CartItems=new List<KeyValuePair<Product, int>>();
            DiscountsApplied=new List<KeyValuePair<Discount, decimal>>();
            TotalDiscountAmount = 0;
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<KeyValuePair<Product, int>> CartItems { get; set; }
        public List<KeyValuePair<Discount, decimal>> DiscountsApplied { get; set; }

        public decimal TotalBillAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }

        public ShoppingCart Clone()
        {
            var scNew = new ShoppingCart(this.Id, this.CustomerId);

            this.CartItems.ForEach(ci => { scNew.CartItems.Add(new KeyValuePair<Product, int>(ci.Key, ci.Value)); });
            return scNew;
        }
    }
}