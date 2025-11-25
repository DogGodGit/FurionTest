namespace TestProject2
{
    public class ArrayHashMapTests
    {
        /// <summary>
        /// 测试 Put 方法是否能够正确添加键值对。
        /// </summary>
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

        /// <summary>
        /// 测试 Get 方法在键不存在时是否返回 null。
        /// </summary>
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

        /// <summary>
        /// 测试 Remove 方法是否能够正确删除键值对。
        /// </summary>
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

        /// <summary>
        /// 测试 PairSet 方法是否能够返回所有键值对。
        /// </summary>
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

        /// <summary>
        /// 测试 KeySet 方法是否能够返回所有键。
        /// </summary>
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

        /// <summary>
        /// 测试 ValueSet 方法是否能够返回所有值。
        /// </summary>
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

        /// <summary>
        /// 测试 HashMap 是否能够正确处理哈希冲突。
        /// </summary>
        [Fact]
        public void HashMap_ShouldHandleCollisions()
        {
            // Arrange
            var hashMap = new ArrayHashMap();
            int key1 = 1;
            int key2 = 101; // 假设 key1 和 key2 具有相同的哈希索引
            hashMap.Put(key1, "Value1");
            hashMap.Put(key2, "Value2");

            // Act
            var value1 = hashMap.Get(key1);
            var value2 = hashMap.Get(key2);

            // Assert
            Assert.Equal("Value2", value1);
            Assert.Equal("Value2", value2);
        }
    }
}