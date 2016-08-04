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
            int courseId = rdr.GetInt32(2);
            string courseName = rdr.GetString(0);
            string courseNumber = rdr.GetString(1);
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


        public void Save()
        {
          SqlConnection conn = DB.Connection();
          conn.Open();

          SqlCommand cmd = new SqlCommand("INSERT INTO courses(name,course_number)OUTPUT INSERTED.id VALUES (@courseName,@courseNumber);", conn );
          SqlParameter nameParameter = new SqlParameter();
          nameParameter.ParameterName = "@courseName";
          nameParameter.Value = this.GetCourseName();
          cmd.Parameters.Add(nameParameter);

          SqlParameter courseNumberParameter = new SqlParameter();
          courseNumberParameter.ParameterName = "@courseNumber";
          courseNumberParameter.Value = this.GetCourseNumber();
          cmd.Parameters.Add(courseNumberParameter);
          SqlDataReader rdr = cmd.ExecuteReader();

          while(rdr.Read())
          {
            this._id = rdr.GetInt32(0);
          }
          if (rdr !=null)
          {
            rdr.Close();
          }
          if (conn !=null)
          {
            conn.Close();
          }
        }



        public static Course Find(int id)
        {
          SqlConnection conn = DB.Connection();
          conn.Open();

          SqlCommand cmd = new SqlCommand("SELECT * FROM courses WHERE id = @courseId;", conn);
          SqlParameter courseIdParameter = new SqlParameter();
          courseIdParameter.ParameterName =  "@courseId";
          courseIdParameter.Value = id.ToString();
          cmd.Parameters.Add(courseIdParameter);
          SqlDataReader rdr = cmd.ExecuteReader();

          int findCourseId = 0;
          string findCourseName = null;
          string findCourseNumber = null;
          while(rdr.Read())
          {
            findCourseId = rdr.GetInt32(2);
            findCourseName = rdr.GetString(0);
            findCourseNumber = rdr.GetString(1);
          }
          Course findCourse = new Course(findCourseName,findCourseNumber,findCourseId);

          if (rdr != null)
          {
            rdr.Close();
          }
          if (conn != null)
          {
            conn.Close();
          }
          return findCourse;

        }


        public void Update(string Name)
        {
          SqlConnection conn = DB.Connection();
          conn.Open();

          SqlCommand cmd = new SqlCommand("UPDATE courses SET name =@courseName output inserted.name WHERE id =@courseId;", conn);
          SqlParameter CourseNameParameter = new SqlParameter();
          CourseNameParameter.ParameterName = "@courseName";
          CourseNameParameter.Value = Name;

          SqlParameter CourseIdParameter = new SqlParameter();
          CourseIdParameter.ParameterName = "@courseId";
          CourseIdParameter.Value = this.GetId();

          cmd.Parameters.Add(CourseNameParameter);
          cmd.Parameters.Add(CourseIdParameter);

          SqlDataReader rdr = cmd.ExecuteReader();

          while(rdr.Read())
          {
            this._courseName = rdr.GetString(0);
          }

          if (rdr != null)
          {
            rdr.Close();
          }

          if (rdr != null)
          {
            conn.Close();
          }
        }

        public void Delete()
        {
          SqlConnection conn = DB.Connection();
          conn.Open();
          SqlCommand cmd = new SqlCommand ("DELETE FROM courses WHERE id =@courseId;", conn);

          SqlParameter courseIdParameter = new SqlParameter();
         courseIdParameter.ParameterName = "@courseId";
         courseIdParameter.Value=this.GetId();
          // Console.WriteLine(this.GetId());
          cmd.Parameters.Add(courseIdParameter);
          cmd.ExecuteNonQuery();
          if (conn !=null)
          {
            conn.Close();
          }
        }

        public static void DeleteAll()
        {
          SqlConnection conn = DB.Connection();
          conn.Open();
          SqlCommand cmd = new SqlCommand ("DELETE FROM courses;", conn);
          cmd.ExecuteNonQuery();
          conn.Close();
        }


        }
      }
