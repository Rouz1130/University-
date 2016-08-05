using System.Collections.Generic;
using Xunit;
using System;


namespace University.Objects
{
  public class StudentTest : IDisposable
  {
    public void Dispose()
    {
      Student.DeleteAll();
    }

    public StudentTest()
   {
     DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=University_;Integrated Security=SSPI;";
   }



    [Fact]
    public void Test1_StudentGetName()
    {
      // arrange
      Student newStudent = new Student("Russ", "2016-1-1");
      // act
      string result = newStudent.GetName();

      Assert.Equal("Russ", result);
    }
    [Fact]
    public void Test2_GetDoe()
    {
      // arrange
      Student newStudent = new Student("Russ", "2016-1-1");
      // act
      string result = newStudent.GetDoe();

      Assert.Equal("2016-1-1", result);

    }

    [Fact]
    public void Test3_SetName()
    {
      // arrange
      Student newStudent = new Student("Russ", "2016-1-1");
      newStudent.SetName("Sam");
      // act
      string result = newStudent.GetName();

      Assert.Equal("Sam", result);
    }

    [Fact]
    public void Test4_SaveStudent()
    {
      //Arrange
    Student newStudent = new Student("Russ", "2016-1-1");
    newStudent.Save();
//ACt
    List<Student> allStudents = Student.GetAll();
    Console.WriteLine(allStudents.Count);
//assert
    Assert.Equal(newStudent, allStudents[0]);
    }

    [Fact]
    public void Test5_FindId()
    {
      Student newStudent = new Student ("Rick","2016-1-1");
      newStudent.Save();

      Student findStudent = Student.Find(newStudent.GetId());

      Assert.Equal(findStudent, newStudent);
    }

    [Fact]
    public void Test6_UpdateStudent_Database()
    {
      Student newStudent = new Student("Rouz","2016-1-1");
      newStudent.Save();
      newStudent.Update("mike");
      string result = newStudent.GetName();

      Assert.Equal("mike", result);
    }

    [Fact]
    public void Test7_DeleteOneStudent()
    {
      Student firstStudent = new Student("Russ","2016-1-1");
      firstStudent.Save();

      Student secondStudent = new Student("Bob", "2016-1-1");
      secondStudent.Save();

      firstStudent.Delete();
      List<Student> allStudents = Student.GetAll();
      List<Student> afterDeleteFristStudent = new List<Student> {secondStudent};

      Assert.Equal(afterDeleteFristStudent, allStudents);
    }

    [Fact]
    public void Test8_AddCourseToStudent()
    {
      //Arrange
      Student firstStudent = new Student("Russ","2016-1-1");
      firstStudent.Save();

      Course testCourse = new Course("Algebra","MATH101");
      testCourse.Save();
      //act
      firstStudent.AddCourse(testCourse);
      List<Course> studentsCourses = firstStudent.GetCourses();
      //Assert
      Assert.Equal(testCourse, studentsCourses[0]);
    }

    [Fact]
   public void Test9_GetCourse_RetrievesAllCourseWithStudents()
   {
     Student testStudent = new Student("Russ","2016-1-1");
     testStudent.Save();

     Course firstCourse = new Course("English","101");
     firstCourse.Save();
     Course secondCourse = new Course("MATH","101");
     secondCourse.Save();
     testStudent.AddCourse(firstCourse);
     testStudent.AddCourse(secondCourse);

     List<Course> testCourseList = new List<Course> {firstCourse, secondCourse};
     List<Course> resultCourseList = testStudent.GetCourses();

     Assert.Equal(testCourseList, resultCourseList);
   }




    }

  }
