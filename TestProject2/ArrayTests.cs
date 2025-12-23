using FurionTest.Common.Algo;

namespace TestProject2;

public class ArrayTests
{
    [Fact]
    public void Test_BinarySearch()
    {
        int[] nums = [1, 3, 5, 7, 9];
        int result = nums.BinarySearch(5);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_BinarySearchLCRO()
    {
        int[] nums = [1, 3, 5, 7, 9];
        int result = nums.BinarySearchLCRO(5);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_BinarySearchInsertionSimple()
    {
        int[] nums = [1, 3, 5, 5, 7, 9];
        int result = nums.BinarySearchInsertionSimple(5);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_BinarySearchInsertion()
    {
        int[] nums = [1, 3, 5, 5, 5, 5, 5, 5, 7, 9];
        int result = nums.BinarySearchInsertion(5);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_BinarySearchLeftEdge()
    {
        int[] nums = [1, 3, 5, 5, 5, 5, 5, 5, 7, 9];
        int result = nums.BinarySearchLeftEdge(5);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_BinarySearchRightEdge()
    {
        int[] nums = [1, 3, 5, 5, 5, 5, 5, 5, 7, 9];
        int result = nums.BinarySearchRightEdge(5);
        Assert.Equal(7, result);
    }

    [Fact]
    public void Test_TwoSumBruteForce()
    {
        int[] nums = [2, 7, 11, 15];
        var result = nums.TwoSumBruteForce(13);
        Assert.Equal(new int[] { 0, 2 }, result);
    }

    [Fact]
    public void Test_TwoSumHashTable()
    {
        int[] nums = [1, 2, 7, 11, 15];
        /*
         * i=0, 13-1=12 [1,0]
         * i=1, 13-2=11 [2,1]
         * i=2, 13-7=6  [7,2]
         * i=3, 13-11=2 return [1,3]
         */
        var result = nums.TwoSumHashTable(13);
        Assert.Equal(new int[] { 1, 3 }, result);
    }
}