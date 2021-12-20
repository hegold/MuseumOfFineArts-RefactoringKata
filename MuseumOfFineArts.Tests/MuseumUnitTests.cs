using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace MuseumOfFineArts.Tests
{
	[TestClass]
	public class MuseumUnitTests
	{
		[TestMethod]
		public void MuseumWithOneAnonymousPainting_GetCountOfAnonymousPaintings_ReturnsOne()
		{
			Museum m = new Museum();
			Painting p = new Painting
			{
				Name = "Painting with unknown artist",
				Artist = null,
			};
			m.Paintings.Add(p);
			Painting pp = new Painting
			{
				Name = "Painting with known artist",
				Artist = "Deadbeef",
			};
			m.Paintings.Add(pp);

			int count = m.GetCountOfAnonymousPaintings();

			Assert.AreEqual(1, count);
		}

		[TestMethod]
		public void WorthlessPainting_FinalPaintingValue_ReturnsZero()
		{
			Museum m = new Museum();
			Painting p = new Painting { Value = 0.0m };

			decimal actual = m.FinalPaintingValue(p);

			Assert.AreEqual(0.0m, actual);
		}

		[TestMethod]
		public void RarePaintingWithNonTrendingArtMarket_FinalPaintingValue_IsCorrect()
		{
			Museum m = new Museum();
			m.IsArtCurrentlyTrending = false;
			Painting p = new Painting { Value = 2.0m, Rarity = "rare" };

			decimal actual = m.FinalPaintingValue(p);

			Assert.AreEqual(4.8m, actual);
		}

		[TestMethod]
		public void SomePaintings_DisplayPaintingValues_EmitsFormattedValues()
		{
			Museum museum = new Museum();
            museum.Paintings.Add(new Painting
            {
                Name = "Bold and Brash",
                Artist = "Squidward",
                Value = 100.0m,
                TransactionLog =
            {
                { "08/15/1997", ("sold to Bob Binley", 1000) },
                { "08/18/1997", ("sold to Larry Schmelton", 2500) },
                { "02/05/2004", ("sold to museum", 100) }
            }
            });
            museum.Paintings.Add(new Painting
            {
                Name = "The Eating of The Cheeseburger",
                Value = 275000.0m,
                Rarity = "rare",
                TransactionLog =
            {
                { "06/02/2006", ("sold to museum", 100000) }
            }
            });
			var consoleOutput = new StringWriter();
			System.Console.SetOut(consoleOutput);

			museum.DisplayPaintingValues();

			Assert.AreEqual("Bold and Brash (common) is worth 80.000 zorknids and has transfered hands 3 times\r\nThe Eating of The Cheeseburger (rare) is worth 660000.000 zorknids and has transfered hands 1 times\r\n", consoleOutput.ToString());
		}

		[TestMethod]
		public void RarePaintingWithTrendingArtMarket_FinalPaintingValue_IsCorrect()
		{
			Museum m = new Museum();
			m.IsArtCurrentlyTrending = true;
			Painting p = new Painting { Value = 2.0m, Rarity = "rare" };

			decimal actual = m.FinalPaintingValue(p);

			Assert.AreEqual(1.5m, actual);
		}

		[TestMethod]
		public void VeryCommonPaintingWithNonTrendingArtMarket_FinalPaintingValue_IsAValue()
		{
			Museum m = new Museum();
			m.IsArtCurrentlyTrending = false;
			Painting p = new Painting { Value = 2.0m, Rarity = "very_common" };

			decimal actual = m.FinalPaintingValue(p);

			Assert.AreEqual(0.8m, actual);
		}

		[TestMethod]
		public void UniquePaintingWithNonTrendingArtMarketAndPopularRarity_FinalPaintingValue_IsCorrect()
		{
			Museum m = new Museum();
			m.IsArtCurrentlyTrending = false;
			m.CurrentlyPopularRarity = "unique";
			Painting p = new Painting { Value = 4.0m, Rarity = "unique" };

			decimal actual = m.FinalPaintingValue(p);

			Assert.AreEqual(9.6m, actual);
		}
	}
}
