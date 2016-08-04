using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace University.Objects
{
public class Course
{
  private string _courseName;
  private string _courseNumber;
  private int _id;

  public Course(string courseName, string courseNumber, int id=0)
  {
    _courseName = courseName;
    _courseNumber = courseNumber;
    _id = id;
  }

  public string GetCourseName()
  {
    return _courseName;
  }

  public string GetCourseNumber()
  {
    return _courseNumber;
  }

  public int GetId()
  {
    return _id;
  }

  public void SetName(string newCourseName)
  {
    _courseName = newCourseName;
  }


  public override bool Equals(System.Object otherCourse)
  {
    if (!(otherCourse is Course))
    {
      return false;
    }
    else
    {
      Course newCourse = (Course) otherCourse;
      return this.GetCourseName().Equals(newCourse.GetCourseName());
    }
  }

        public override int GetHashCode()
        {
          return this.GetCourseName().GetHashCode();
        }



        public static List<Course> GetAll()
        {
          List<Course> allCourses = new List<Course>{};

          SqlConnection conn = DB.Connection();
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT * FROM courses;", conn);
          SqlDataReader rdr  = cmd.ExecuteReader();

          while(rdr.Read())
          {
            int courseId = rdr.GetInt32(0);
            string courseName = rdr.GetString(1);
            string courseNumber = rdr.GetString(2);
            Course newCourse = new Course(courseName, courseNumber, courseId);
            allCourses.Add(newCourse);
          }
          if (rdr != null)
          {
            rdr.Close();
          }
          if (conn != null)
          {
            conn.Close();
          }
          return allCourses;
        }

















          // public static void DeleteAll()
          // {
          //   SqlConnection conn = DB.Connection();
          //   conn.Open();
          //   SqlCommand cmd = new SqlCommand ("DELETE FROM course;", conn);
          //   cmd.ExecuteNonQuery();
          //   conn.Close();
          // }







        }
      }
