using Xunit.Abstractions;

namespace TestProject2;

public class UnitTest2
{
    private readonly ITestOutputHelper Output;

    public UnitTest2(
    ITestOutputHelper tempOutput)
    {
        Output = tempOutput;
    }

    [Fact]
    public void 测试生成数组()
    {
        int[] arr = [1, 2, 3, 4, 5];
        List<string> list = ["a", "b", "c"];
        Span<char> span = ['a', 'b', 'c'];
        int[][] two2D = [[1, 2], [3, 4], [5, 6]];

        int[] item0 = [88, 2, 3];
        int[] item1 = [88, 2, 3, 4, 5];
        int[] item2 = [88, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        int[] single = [.. item0, .. item1, .. item2];
        foreach (var item in single)
        {
            Output.WriteLine(item.ToString());
        }
    }

    [Fact]
    public void Test_StudentInfo()
    {
        var femaleStudents = students.Where(s => s.StudentName == "时光者");
        var studentNames = students.Select(s => s.StudentName);

        // 使用SelectMany展平所有学生的课程列表
        var allCourses = students.SelectMany(student => student.Courses).ToList();

        // 输出所有课程的名称
        foreach (var course in allCourses)
        {
            Output.WriteLine(course.CourseName);
        }

        var studentList = students.ToList();
        var studentArray = students.ToArray();
        var studentDictionary = students.ToDictionary(s => s.StudentID, s => s.StudentName);
        var studentLookup = students.ToLookup(s => s.ClassID, s => s.StudentName);

        foreach (var group in studentLookup)
        {
            Console.WriteLine("Age Group: {0}", string.Join(',', group.Key));  //每组都有一个键 

            foreach (var s in group)  //每个组都有一个内部集合  
                Console.WriteLine("Student Name: {0}", s);
        }

        var firstStudent = students.First();
        var firstAdult = students.FirstOrDefault(s => s.Birthday <= DateTime.Now.AddYears(-18));
        var onlyWangWu = students.Single(s => s.StudentName == "王五");
        var wangWuOrDefault = students.SingleOrDefault(s => s.StudentName == "王六");
        var lastStudent = students.Last();
        var lastAdult = students.LastOrDefault(s => s.Birthday <= DateTime.Now.AddYears(-18));
        var secondStudent = students.ElementAt(1);
        var tenthStudentOrDefault = students.ElementAtOrDefault(9);
        var nonEmptyStudents = students.DefaultIfEmpty(new StudentInfo { StudentID = 0, StudentName = "默认Student", Address = "默认" });
    }

    #region 学生信息测试

    public class StudentInfo
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public DateTime Birthday { get; set; }
        public int ClassID { get; set; }
        public string Address { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
    }

    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
    }

    private static List<StudentInfo> students = new List<StudentInfo>
    {
        new StudentInfo
        {
            StudentID=1,
            StudentName="大姚",
            Birthday=Convert.ToDateTime("1997-10-25"),
            ClassID=101,
            Courses = new List<Course>
            {
                new Course { CourseID = 101, CourseName = "语文" },
                new Course { CourseID = 102, CourseName = "数学" }
            }
        },
        new StudentInfo
        {
            StudentID=2,
            StudentName="李四",
            Birthday=Convert.ToDateTime("1998-10-25"),
            ClassID=101,
            Courses = new List<Course>
            {
                new Course { CourseID = 101, CourseName = "语文" },
                new Course { CourseID = 102, CourseName = "数学" }
            }
        },
        new StudentInfo
        {
            StudentID=3,
            StudentName="王五",
            Birthday=Convert.ToDateTime("1999-10-25"),
            ClassID=102,
            Address="广州",
            Courses = new List<Course>
            {
                new Course { CourseID = 101, CourseName = "语文" },
                new Course { CourseID = 102, CourseName = "数学" }
            }
        },
        new StudentInfo
        {
            StudentID=4,
            StudentName="时光者",
            Birthday=Convert.ToDateTime("1999-11-25"),
            ClassID=102,
            Address="深圳" ,
            Courses = new List<Course>
            {
                new Course { CourseID = 104, CourseName = "历史" },
                new Course { CourseID = 103, CourseName = "地理" }
            }
        }
    };

    #endregion
}
