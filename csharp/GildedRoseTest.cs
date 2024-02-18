using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        [Test]
        public void DoubleDegradationPastSellByDateTest()
        {
            IList<IBaseItem> Items = new List<IBaseItem> { new Item { Name = "Item test 1", SellIn = 0, Quality = 8 } };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.AreEqual(6, Items[0].Quality);

            Items = new List<IBaseItem> { new ItemConjurado { Name = "Item test 1", SellIn = 0, Quality = 8 } };

            app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.AreEqual(4, Items[0].Quality);
        }

        [Test]
        public void CheckNonNegativeQualityTest()
        {
            IList<IBaseItem> Items = new List<IBaseItem> { new Item { Name = "Item test 2", SellIn = 0, Quality = 0 } };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.True(Items[0].Quality == 0);

            Items = new List<IBaseItem> { new ItemConjurado { Name = "Item test 2", SellIn = 0, Quality = 0 } };

            app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.True(Items[0].Quality == 0);
        }

        [Test]
        public void CheckQualityValidBrieCheeseDateTest()
        {
            IList<IBaseItem> Items = new List<IBaseItem> { new Item { Name = "Aged Brie", SellIn = 1, Quality = 5 } };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.True(Items[0].Quality == 6);
        }

        [Test]
        public void CheckQualityExpiredBrieCheeseTest()
        {
            IList<IBaseItem> Items = new List<IBaseItem> { new Item { Name = "Aged Brie", SellIn = 0, Quality = 5 } };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.True(Items[0].Quality == 7);
        }

        [Test]
        public void ItemQualityBelow50Test()
        {
            IList<IBaseItem> Items = new List<IBaseItem> { new Item { Name = "Aged Brie", SellIn = 0, Quality = 49 } };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.AreEqual(50, Items[0].Quality);
        }

        [Test]
        public void SulfurasQualityImmutableTest()
        {
            IList<IBaseItem> Items = new List<IBaseItem> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 20 } };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.AreEqual(10, Items[0].SellIn);
            Assert.AreEqual(20, Items[0].Quality);
        }

        [Test]
        public void BackStage()
        {
            IList<IBaseItem> Items = new List<IBaseItem> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 20 } };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.AreEqual(9, Items[0].SellIn);
            Assert.AreEqual(22, Items[0].Quality);

            Items.Add(new Item()
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 7,
                Quality = 20
            });

            app.UpdateQuality();

            Assert.AreEqual(6, Items[1].SellIn);
            Assert.AreEqual(22, Items[1].Quality);

            Items.Add(new Item()
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 5,
                Quality = 20
            });

            app.UpdateQuality();

            Assert.AreEqual(4, Items[2].SellIn);
            Assert.AreEqual(23, Items[2].Quality);

            Items.Add(new Item()
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 3,
                Quality = 20
            });

            app.UpdateQuality();

            Assert.AreEqual(2, Items[3].SellIn);
            Assert.AreEqual(23, Items[3].Quality);

            Items.Add(new Item()
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 0,
                Quality = 20
            });

            app.UpdateQuality();

            Assert.AreEqual(-1, Items[4].SellIn);
            Assert.AreEqual(0, Items[4].Quality);
        }
    }
}
