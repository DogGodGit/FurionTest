using SqlSugar;
using System;
namespace FurionTest.Core.Models;

[SugarTable("dbo.Student")]
public class Student
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "StudentId", ColumnDescription = "主键")]
    public int Id { get; set; }

    public string Name { get; set; }
    public int? SchoolId { get; set; }
    public DateTime? CreateTime { get; set; }
    public int? TestId { get; set; }
}