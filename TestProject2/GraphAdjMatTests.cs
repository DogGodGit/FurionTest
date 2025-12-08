using FurionTest.Common.Algo;
using Xunit.Abstractions;

namespace TestProject2;

public class GraphAdjMatTests
{
    private readonly ITestOutputHelper Output;

    public GraphAdjMatTests(ITestOutputHelper tempOutput)
    {
        Output = tempOutput;
    }

    private void PrintGraph(GraphAdjMat graph)
    {
        using var writer = new StringWriter();
        Console.SetOut(writer);

        graph.Print();

        var output = writer.ToString().Trim();
        Output.WriteLine(output);
    }

    private GraphAdjMat CreateGraph()
    {
        var vertices = new int[] { 1, 3, 2, 5, 4 };
        var edges = new int[][] { [0, 3], [0, 1], [1, 2], [2, 4], [3, 4], [2, 3] };
        var graph = new GraphAdjMat(vertices, edges);
        return graph;
    }

    [Fact]
    public void Test_CreateGraph()
    {
        var graph = CreateGraph();

        Assert.Equal(5, graph.Size());

        PrintGraph(graph);
    }

    [Fact]
    public void Test_AddVertex()
    {
        var graph = CreateGraph();
        graph.AddVertex(6);

        Assert.Equal(6, graph.Size());
        PrintGraph(graph);
    }

    [Fact]
    public void Test_RemoveVertex()
    {
        var graph = CreateGraph();
        graph.RemoveVertex(1);

        Assert.Equal(4, graph.Size());
        PrintGraph(graph);
    }

    [Fact]
    public void TestAddEdge()
    {
        var graph = CreateGraph();
        graph.AddEdge(0, 2);

        PrintGraph(graph);
    }

    [Fact]
    public void TestRemoveEdge()
    {
        var graph = CreateGraph();
        graph.RemoveEdge(0, 1);

        PrintGraph(graph);
    }
}