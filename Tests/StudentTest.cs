using System.Collections.Generic;
using Xunit;
using System;


namespace University.Objects
{
  public class StudentTest
  {
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

    Student newStudent = new Student("Russ", "2016-1-1");
    newStudent.Save();

    List<Student> allStudents = Student.GetAll();

    Assert.Equal(newStudent, allStudents[0]);


    }

  }
}
