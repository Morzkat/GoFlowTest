using GoFlow.InventoryPickingLocations;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace GoFlow.Test
{
    [TestFixture]
    public class PickingLocationsTest
    {
        List<Location> _locations;

        [SetUp]
        public void Setup()
        {
            _locations = new List<Location>();
        }

        [Test]
        public void First_Pickup_Test_Case()
        {
            //Arrange
            var quantityToPick = 12;
            var expectedResult = new List<InventoryToPick>() { new InventoryToPick(1, 12) };

            _locations.Add(new Location { Id = 1, QuantityAvailable = 24 });
            _locations.Add(new Location { Id = 2, QuantityAvailable = 48 });

            var pickingLocations = new GoFlow.InventoryPickingLocations.PickingLocations(_locations);

            //Act
            var picks = pickingLocations.Calculate(quantityToPick);

            //Assert
            Assert.NotNull(picks);
            Assert.IsTrue(new InventoryToPickComparer().Equals(expectedResult, picks));
        }

        [Test]
        public void Second_Pickup_Test_Case()
        {
            //Arrange
            var quantityToPick = 24;
            var expectedResult = new List<InventoryToPick>()
            {
                new InventoryToPick(2, 24),
            };

            _locations.Add(new Location { Id = 1, QuantityAvailable = 12 });
            _locations.Add(new Location { Id = 2, QuantityAvailable = 48 });
            _locations.Add(new Location { Id = 3, QuantityAvailable = 12 });

            var pickingLocations = new GoFlow.InventoryPickingLocations.PickingLocations(_locations);

            //Act
            var picks = pickingLocations.Calculate(quantityToPick);

            //Assert
            Assert.NotNull(picks);
            Assert.IsTrue(new InventoryToPickComparer().Equals(expectedResult, picks));
        }

        [Test]
        public void Third_Pickup_Test_Case()
        {
            //Arrange
            var quantityToPick = 24;
            var expectedResult = new List<InventoryToPick>()
            {
                new InventoryToPick(2, 12),
                new InventoryToPick(5, 12),
            };

            _locations.Add(new Location { Id = 1, QuantityAvailable = 10 });
            _locations.Add(new Location { Id = 2, QuantityAvailable = 12 });
            _locations.Add(new Location { Id = 3, QuantityAvailable = 10 });
            _locations.Add(new Location { Id = 4, QuantityAvailable = 10 });
            _locations.Add(new Location { Id = 5, QuantityAvailable = 12 });

            var pickingLocations = new GoFlow.InventoryPickingLocations.PickingLocations(_locations);

            //Act
            var picks = pickingLocations.Calculate(quantityToPick);

            //Assert
            Assert.NotNull(picks);
            Assert.IsTrue(new InventoryToPickComparer().Equals(expectedResult, picks));
        }

        [Test]
        public void Fourth_Pickup_Test_Case()
        {
            //Arrange
            var quantityToPick = 60;
            var expectedResult = new List<InventoryToPick>()
            {
                new InventoryToPick(4, 50),
                new InventoryToPick(5, 10),
            };

            _locations.Add(new Location { Id = 1, QuantityAvailable = 20 });
            _locations.Add(new Location { Id = 2, QuantityAvailable = 20 });
            _locations.Add(new Location { Id = 3, QuantityAvailable = 20 });
            _locations.Add(new Location { Id = 4, QuantityAvailable = 50 });
            _locations.Add(new Location { Id = 5, QuantityAvailable = 10 });

            var pickingLocations = new GoFlow.InventoryPickingLocations.PickingLocations(_locations);

            //Act
            var picks = pickingLocations.Calculate(quantityToPick);

            //Assert
            Assert.NotNull(picks);
            Assert.IsTrue(new InventoryToPickComparer().Equals(expectedResult, picks));
        }
    }

    sealed class InventoryToPickComparer : IEqualityComparer<IEnumerable<InventoryToPick>>
    {
        public bool Equals([DisallowNull] IEnumerable<InventoryToPick> x, [DisallowNull] IEnumerable<InventoryToPick> y)
        {
            var result = true;
            for (int i = 0; i < x.Count(); i++)
            {
                var xInventoryPickup = x.ElementAt(i);
                var yInventoryPickup = y.ElementAt(i);
                var locationsAreNotEqual = xInventoryPickup.LocationId != yInventoryPickup.LocationId;
                var quantitiesAreNotEqual = xInventoryPickup.Quantity != yInventoryPickup.Quantity;

                if (locationsAreNotEqual || quantitiesAreNotEqual)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public int GetHashCode([DisallowNull] IEnumerable<InventoryToPick> obj)
        {
            throw new System.NotImplementedException();
        }
    }
}