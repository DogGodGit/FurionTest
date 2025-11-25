namespace TestProject2;

public class MyListTests
{
    [Fact]
    public void Test_Constructor()
    {
        var list = new MyList();
        Assert.Equal(0, list.Size());
        Assert.Equal(10, list.Capacity());
    }

    [Fact]
    public void Test_Add()
    {
        var list = new MyList();
        list.Add(10);
        list.Add(20);
        Assert.Equal(2, list.Size());
        Assert.Equal(10, list.Get(0));
        Assert.Equal(20, list.Get(1));
    }

    [Fact]
    public void Test_Insert()
    {
        var list = new MyList();
        list.Add(10);
        list.Add(30);
        list.Insert(1, 20);
        Assert.Equal(3, list.Size());
        Assert.Equal(20, list.Get(1));
    }

    [Fact]
    public void Test_Remove()
    {
        var list = new MyList();
        list.Add(10);
        list.Add(20);
        var removed = list.Remove(0);
        Assert.Equal(10, removed);
        Assert.Equal(1, list.Size());
        Assert.Equal(20, list.Get(0));
    }

    [Fact]
    public void Test_Set()
    {
        var list = new MyList();
        list.Add(10);
        list.Set(0, 20);
        Assert.Equal(20, list.Get(0));
    }

    [Fact]
    public void Test_ExtendCapacity()
    {
        var list = new MyList();
        for (int i = 0; i < 11; i++)
        {
            list.Add(i);
        }
        Assert.Equal(11, list.Size());
        Assert.True(list.Capacity() >= 11);
    }

    [Fact]
    public void Test_ToArray()
    {
        var list = new MyList();
        list.Add(10);
        list.Add(20);
        var array = list.ToArray();
        Assert.Equal(new[] { 10, 20 }, array);
    }

    [Fact]
    public void Test_Get_OutOfRange()
    {
        var list = new MyList();
        Assert.Throws<IndexOutOfRangeException>(() => list.Get(0));
    }

    [Fact]
    public void Test_Insert_OutOfRange()
    {
        var list = new MyList();
        Assert.Throws<IndexOutOfRangeException>(() => list.Insert(1, 10));
    }

    [Fact]
    public void Test_Remove_OutOfRange()
    {
        var list = new MyList();
        Assert.Throws<IndexOutOfRangeException>(() => list.Remove(0));
    }

    [Fact]
    public void Test_Set_OutOfRange()
    {
        var list = new MyList();
        Assert.Throws<IndexOutOfRangeException>(() => list.Set(0, 10));
    }
}