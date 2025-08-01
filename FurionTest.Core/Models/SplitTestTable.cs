using SqlSugar;
using System;
namespace FurionTest.Core.Models;

[SplitTable(SplitType.Year)] // Table by year (the table supports year, quarter, month, week and day)
[SugarTable("SplitTestTable_{year}{month}{day}")]
public class SplitTestTable
{
    [SugarColumn(IsPrimaryKey = true)]
    public long Id { get; set; }

    public string Name { get; set; }

    //When the sub-table field is inserted, which table will be inserted according to this field.
    //When it is updated and deleted, it can also be convenient to use this field to
    //find out the related table
    [SplitField]
    public DateTime CreateTime { get; set; }
}