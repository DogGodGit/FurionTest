using FurionTest.Common.Algo;
using Xunit;

namespace TestProject2
{
    public class ArrayHashMapTests
    {
        [Fact]
        public void Put_ShouldAddKeyValuePair()
        {
            // Arrange
            var hashMap = new ArrayHashMap();

            // Act
            hashMap.Put(1, "Value1");

            // Assert
            Assert.Equal("Value1", hashMap.Get(1));
        }

        [Fact]
        public void Get_ShouldReturnNull_WhenKeyDoesNotExist()
        {
            // Arrange
            var hashMap = new ArrayHashMap();

            // Act
            var result = hashMap.Get(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Remove_ShouldDeleteKeyValuePair()
        {
            // Arrange
            var hashMap = new ArrayHashMap();
            hashMap.Put(1, "Value1");

            // Act
            hashMap.Remove(1);

            // Assert
            Assert.Null(hashMap.Get(1));
        }

        [Fact]
        public void PairSet_ShouldReturnAllKeyValuePairs()
        {
            // Arrange
            var hashMap = new ArrayHashMap();
            hashMap.Put(1, "Value1");
            hashMap.Put(2, "Value2");

            // Act
            var pairs = hashMap.PairSet();

            // Assert
            Assert.Equal(2, pairs.Count);
            Assert.Contains(pairs, p => p.key == 1 && p.val == "Value1");
            Assert.Contains(pairs, p => p.key == 2 && p.val == "Value2");
        }

        [Fact]
        public void KeySet_ShouldReturnAllKeys()
        {
            // Arrange
            var hashMap = new ArrayHashMap();
            hashMap.Put(1, "Value1");
            hashMap.Put(2, "Value2");

            // Act
            var keys = hashMap.KeySet();

            // Assert
            Assert.Equal(2, keys.Count);
            Assert.Contains(1, keys);
            Assert.Contains(2, keys);
        }

        [Fact]
        public void ValueSet_ShouldReturnAllValues()
        {
            // Arrange
            var hashMap = new ArrayHashMap();
            hashMap.Put(1, "Value1");
            hashMap.Put(2, "Value2");

            // Act
            var values = hashMap.ValueSet();

            // Assert
            Assert.Equal(2, values.Count);
            Assert.Contains("Value1", values);
            Assert.Contains("Value2", values);
        }

        [Fact]
        public void HashMap_ShouldHandleCollisions()
        {
            // Arrange
            var hashMap = new ArrayHashMap();
            int key1 = 1;
            int key2 = 101; // Same hash index as key1
            hashMap.Put(key1, "Value1");
            hashMap.Put(key2, "Value2");

            // Act
            var value1 = hashMap.Get(key1);
            var value2 = hashMap.Get(key2);

            // Assert
            Assert.Equal("Value1", value1);
            Assert.Equal("Value2", value2);
        }
    }
}