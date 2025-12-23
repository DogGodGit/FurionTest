using FurionTest.Common.Algo;
using Xunit.Abstractions;

namespace TestProject2;

public class GraphAdjListTests
{
    private readonly ITestOutputHelper Output;

    public GraphAdjListTests(ITestOutputHelper tempOutput)
    {
        Output = tempOutput;
    }

    private GraphAdjList CreateGraph()
    {
        // 定义顶点
        Vertex v1 = new Vertex(1);
        Vertex v2 = new Vertex(2);
        Vertex v3 = new Vertex(3);
        Vertex v4 = new Vertex(4);
        Vertex v5 = new Vertex(5);

        // 定义边
        Vertex[][] edges =
        [
            [v1, v3],
            [v1, v5],
            [v2, v3],
            [v4, v2],
            [v5, v2],
            [v4, v5]
        ];
        GraphAdjList graph = new GraphAdjList(edges);
        return graph;
    }


    private void PrintGraph(GraphAdjList graph)
    {
        using var writer = new StringWriter();
        Console.SetOut(writer);

        graph.Print();

        var output = writer.ToString().Trim();
        Output.WriteLine(output);
    }

    [Fact]
    public void Tset_Constructor()
    {
        var graph = CreateGraph();
        Assert.Equal(5, graph.Size());

        PrintGraph(graph);
    }

    [Fact]
    public void Test_AddVertex()
    {
        var graph = CreateGraph();
        graph.AddVertex(new Vertex(6));

        Assert.Equal(6, graph.Size());
        PrintGraph(graph);
    }

    [Fact]
    public void Test_RemoveVertex()
    {
        var graph = CreateGraph();
        graph.AddVertex(new Vertex(1));

        Assert.Equal(6, graph.Size());
        PrintGraph(graph);
    }

    [Fact]
    public void Test_Dictionary()
    {
        Dictionary<Vertex, List<Vertex>> adjList = new()
        {
            { new Vertex(1), [new Vertex(2), new Vertex(3)] }
        };

        bool containsKey = adjList.ContainsKey(new Vertex(1));
        Assert.True(containsKey);
    }

    [Fact]
    public void TestAddEdge()
    {
        var graph = CreateGraph();
        graph.AddEdge(new Vertex(1), new Vertex(2));

        PrintGraph(graph);
    }

    [Fact]
    public void TestRemoveEdge()
    {
        var graph = CreateGraph();
        graph.AddEdge(new Vertex(1), new Vertex(3));

        PrintGraph(graph);
    }

    [Fact]
    public void TestBFS()
    {
        var graph = CreateGraph();
        Assert.Equal(5, graph.Size());
        PrintGraph(graph);
        var list = graph.GraphBFS(new Vertex(5));

        Assert.Equal([5, 1, 2, 4, 3], list.Select(v => v.val));
    }


    [Fact]
    public void TestDFS()
    {
        var graph = CreateGraph();
        Assert.Equal(5, graph.Size());

        var list = graph.GraphDFS(new Vertex(5));

        Assert.Equal([5, 1, 3, 2, 4], list.Select(v => v.val));
    }
}
