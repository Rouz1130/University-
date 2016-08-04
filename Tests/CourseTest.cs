using System.Collections.Generic;
using Xunit;
using System;


namespace University.Objects
{
  public class CourseTest : IDisposable
  {
    public void Dispose()
    {
      Course.DeleteAll();
    }

    public CourseTest()
   {
     DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=University_;Integrated Security=SSPI;";
   }

   [Fact]
   public void Test1_GetCourseName()
   {
     // arrange
     Course newCourse = new Course("English", "101");
     // act
     string result = newCourse.GetCourseName();

     Assert.Equal("English", result);
   }

   [Fact]
   public void Test2_GetCourseNumber()
   {
     // arrange
    Course newCourse = new Course("English", "101");
     // act
     string result = newCourse.GetCourseNumber();

     Assert.Equal("101", result);

   }

   [Fact]
   public void Test3_SetCourseName()
   {
     // arrange
     Course newCourse = new Course("English", "101");
     newCourse.SetName("English");
     // act
     string result = newCourse.GetCourseName();

     Assert.Equal("English", result);
   }

   [Fact]
   public void Test4_SaveCourseName()
   {
     //Arrange
   Course newCourse = new Course("English", "101");
   newCourse.Save();
//ACt
   List<Course> allCourses = Course.GetAll();
   Console.WriteLine(allCourses.Count);
//assert
   Assert.Equal(newCourse, allCourses[0]);
   }



     [Fact]
     public void Test5_FindId()
     {
       Course newCourse = new Course ("English","101");
       newCourse.Save();

       Course findCourse = Course.Find(newCourse.GetId());

       Assert.Equal(findCourse, newCourse);
     }


     [Fact]
     public void Test6_UpdateCourse_Database()
     {
       Course newCourse = new Course("English","101");
       newCourse.Save();
       newCourse.Update("Math");
       string result = newCourse.GetCourseName();

       Assert.Equal("Math", result);
     }


     [Fact]
     public void Test7_DeleteOneCourse()
     {
       Course firstCourse = new Course("English","101");
       firstCourse.Save();

       Course secondCourse = new Course("English", "101");
       secondCourse.Save();

       firstCourse.Delete();
       List<Course> allCourses = Course.GetAll();
       List<Course> afterDeleteFristCourse = new List<Course> {secondCourse};

       Assert.Equal(afterDeleteFristCourse, allCourses);
     }


 }
}
