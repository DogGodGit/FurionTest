/* 键值对 int->string */

public class Vertex
{
    public int val { get; set; }

    public Vertex()
    { }

    public Vertex(int x) => val = x;

    public override bool Equals(object obj)
    {
        if (obj is Vertex other)
        {
            return other.val == val;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return val.GetHashCode();
    }
}