using System.Collections.Generic;
using Xunit;
using System;


namespace University.Objects
{
  public class CourseTest 
  {
    // public void Dispose()
    // {
    //   Course.DeleteAll();
    // }

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
}
}
