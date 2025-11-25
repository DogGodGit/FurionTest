using SqlSugar;
using System;

namespace FurionTest.Core.Models;

public class Order
{
    public int CustomId { get; set; }

    [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnDescription = "主键")]
    public int Id { get; set; }

    public DateTime CreateTime { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
}