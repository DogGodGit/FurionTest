namespace FurionTest.Common.Algo;

/* 基于邻接矩阵实现的无向图类 */
public class GraphAdjMat
{
    List<int> vertices;     // 顶点列表，元素代表“顶点值”，索引代表“顶点索引”
    List<List<int>> adjMat; // 邻接矩阵，行列索引对应“顶点索引”

    /* 构造函数 */
    public GraphAdjMat(int[] vertices, int[][] edges)
    {
        this.vertices = [];
        this.adjMat = [];
        // 添加顶点
        foreach (int val in vertices)
        {
            AddVertex(val);
        }
        // 添加边
        // 请注意，edges 元素代表顶点索引，即对应 vertices 元素索引
        foreach (int[] e in edges)
        {
            AddEdge(e[0], e[1]);
        }
    }

    /* 获取顶点数量 */
    public int Size()
    {
        return vertices.Count;
    }

    /* 添加顶点 */
    public void AddVertex(int val)
    {
        int n = Size();
        // 向顶点列表中添加新顶点的值
        vertices.Add(val);
        // 在邻接矩阵中添加一行
        List<int> newRow = new(n);
        for (int j = 0; j < n; j++)
        {
            newRow.Add(0);
        }
        adjMat.Add(newRow);
        // 在邻接矩阵中添加一列
        foreach (List<int> row in adjMat)
        {
            row.Add(0);
        }
    }

    /* 删除顶点 */
    public void RemoveVertex(int index)
    {
        if (index >= Size())
            throw new IndexOutOfRangeException();
        // 在顶点列表中移除索引 index 的顶点
        vertices.RemoveAt(index);
        // 在邻接矩阵中删除索引 index 的行
        adjMat.RemoveAt(index);
        // 在邻接矩阵中删除索引 index 的列
        foreach (List<int> row in adjMat)
        {
            row.RemoveAt(index);
        }
    }

    /* 添加边 */
    // 参数 i, j 对应 vertices 元素索引
    public void AddEdge(int i, int j)
    {
        // 索引越界与相等处理
        if (i < 0 || j < 0 || i >= Size() || j >= Size() || i == j)
            throw new IndexOutOfRangeException();
        // 在无向图中，邻接矩阵关于主对角线对称，即满足 (i, j) == (j, i)
        adjMat[i][j] = 1;
        adjMat[j][i] = 1;
    }

    /* 删除边 */
    // 参数 i, j 对应 vertices 元素索引
    public void RemoveEdge(int i, int j)
    {
        // 索引越界与相等处理
        if (i < 0 || j < 0 || i >= Size() || j >= Size() || i == j)
            throw new IndexOutOfRangeException();
        adjMat[i][j] = 0;
        adjMat[j][i] = 0;
    }

    /* 打印邻接矩阵 */
    public void Print()
    {
        Console.Write("顶点列表 = ");
        PrintUtil.PrintList(vertices);
        Console.WriteLine("邻接矩阵 =");
        PrintUtil.PrintMatrix(adjMat);
    }
}
