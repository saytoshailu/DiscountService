using System.Collections.Generic;
using Assignment.DiscountShop.Contracts;
using Assignment.DiscountShop.DiscountShopService;
using Assignment.DiscountShop.Models;
using NUnit.Framework;

namespace Assignment.DiscountShop.Tests
{
    public class ShoppingCartServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ComputeBillScenarioATest()
        {
            IDiscountService discountService = new DiscountService();
            IShoppingCartService scs = new ShoppingCartService(discountService);
            var cs = new CustomerService(scs);
            var custId = cs.CreateCustomer("Paniraj N");
            var shoppingCart = cs.CreateShoppingCart(custId);


            var ps = new ProductService();
            var pA = ps.CreateProduct("A", "product A", 50);
            var pB = ps.CreateProduct("B", "product B", 30);
            var pC = ps.CreateProduct("C", "product C", 20);

            var discountA = discountService.CreateDiscount("Discount on A", "Discount if Purchase 3 As");
            var pAs = new KeyValuePair<Product, int>(pA, 3);
            var discountCombinationAs = new List<KeyValuePair<Product, int>>();
            discountCombinationAs.Add(pAs);
            var dci = new DiscountCombinationItems(discountCombinationAs,
                20, 0);

            discountService.CreateDiscountCombinationItems(discountA.Id, dci);

            scs.AddItem(shoppingCart, pA, 1);
            scs.AddItem(shoppingCart, pB, 1);
            scs.AddItem(shoppingCart, pC, 1);

            shoppingCart = scs.ComputeBill(shoppingCart);

            Assert.AreEqual(100, shoppingCart.TotalBillAmount - shoppingCart.TotalDiscountAmount);
        }

        [Test]
        public void ComputeBillScenarioBTest()
        {
            IDiscountService discountService = new DiscountService();
            IShoppingCartService scs = new ShoppingCartService(discountService);
            var cs = new CustomerService(scs);
            var custId = cs.CreateCustomer("Paniraj N");
            var shoppingCart = cs.CreateShoppingCart(custId);


            var ps = new ProductService();
            var pA = ps.CreateProduct("A", "product A", 50);
            var pB = ps.CreateProduct("B", "product B", 30);
            var pC = ps.CreateProduct("C", "product C", 20);
            var pD = ps.CreateProduct("D", "product D", 15);

            var discountA = discountService.CreateDiscount("Discount on A", "Discount if Purchase 3 As");
            var pAs = new KeyValuePair<Product, int>(pA, 3);
            var discountCombinationAs = new List<KeyValuePair<Product, int>>();
            discountCombinationAs.Add(pAs);
            var dci = new DiscountCombinationItems(discountCombinationAs,
                20, 0);

            discountService.CreateDiscountCombinationItems(discountA.Id, dci);

            //Discount B
            var discountB = discountService.CreateDiscount("Discount on B", "Discount if Purchase 2 Bs");
            var pBs = new KeyValuePair<Product, int>(pB, 2);
            var discountCombinationBs = new List<KeyValuePair<Product, int>>();
            discountCombinationBs.Add(pBs);
            var dc2 = new DiscountCombinationItems(discountCombinationBs,
                15, 0);

            discountService.CreateDiscountCombinationItems(discountB.Id, dc2);

            scs.AddItem(shoppingCart, pA, 5);
            scs.AddItem(shoppingCart, pB, 5);
            scs.AddItem(shoppingCart, pC, 1);

            shoppingCart = scs.ComputeBill(shoppingCart);

            Assert.AreEqual(370, shoppingCart.TotalBillAmount - shoppingCart.TotalDiscountAmount);
        }

        [Test]
        public void ComputeBillScenarioCTest()
        {
            IDiscountService discountService = new DiscountService();
            IShoppingCartService scs = new ShoppingCartService(discountService);
            var cs = new CustomerService(scs);
            var custId = cs.CreateCustomer("Paniraj N");
            var shoppingCart = cs.CreateShoppingCart(custId);


            var ps = new ProductService();
            var pA = ps.CreateProduct("A", "product A", 50);
            var pB = ps.CreateProduct("B", "product B", 30);
            var pC = ps.CreateProduct("C", "product C", 20);
            var pD = ps.CreateProduct("D", "product D", 15);

            //Discount A
            var discountA = discountService.CreateDiscount("Discount on A", "Discount if Purchase 3 As");
            var pAs = new KeyValuePair<Product, int>(pA, 3);
            var discountCombinationAs = new List<KeyValuePair<Product, int>>();
            discountCombinationAs.Add(pAs);
            var dci = new DiscountCombinationItems(discountCombinationAs,
                20, 0);

            discountService.CreateDiscountCombinationItems(discountA.Id, dci);

            //Discount B
            var discountB = discountService.CreateDiscount("Discount on B", "Discount if Purchase 2 Bs");
            var pBs = new KeyValuePair<Product, int>(pB, 2);
            var discountCombinationBs = new List<KeyValuePair<Product, int>>();
            discountCombinationBs.Add(pBs);
            var dc2 = new DiscountCombinationItems(discountCombinationBs,
                15, 0);

            discountService.CreateDiscountCombinationItems(discountB.Id, dc2);

            //Discount C + D
            var discountCD = discountService.CreateDiscount("Discount on C & D", "Discount if Purchase C & Ds");
            var pCs = new KeyValuePair<Product, int>(pC, 1);
            var pDs = new KeyValuePair<Product, int>(pD, 1);
            var discountCombinationCDs = new List<KeyValuePair<Product, int>>();
            discountCombinationCDs.Add(pCs);
            discountCombinationCDs.Add(pDs);
            var dc3 = new DiscountCombinationItems(discountCombinationCDs,
                5, 0);

            discountService.CreateDiscountCombinationItems(discountCD.Id, dc3);

            scs.AddItem(shoppingCart, pA, 3);
            scs.AddItem(shoppingCart, pB, 5);
            scs.AddItem(shoppingCart, pC, 1);
            scs.AddItem(shoppingCart, pD, 1);

            shoppingCart = scs.ComputeBill(shoppingCart);

            Assert.AreEqual(280, shoppingCart.TotalBillAmount - shoppingCart.TotalDiscountAmount);
        }
    }
}