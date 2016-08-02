using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace University.Objects
{
  public class Student
  {
    private string _name;
    private string _doe;
    private int _id;

    public Student(string name, string doe, int id=0)
    {
      _name = name;
      _doe = doe;
      _id = id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetDoe()
    {
      return _doe;
    }

    public int GetId()
    {
      return _id;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }

    public override bool Equals(System.Object otherStudent)
    {
      if (!(otherStudent is Student))
      {
        return false;
      }
      else
      {
        Student newStudent = (Student) otherStudent;
        return this.GetName().Equals(newStudent.GetName());
      }
    }

    public override int GetHashCode()
    {
         return this.GetName().GetHashCode();
    }

    public void Save()
    {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO students(name,doe)OUTPUT INSERTED.id VALUES (@studentName,@studentDoe);", conn );
        SqlParameter nameParameter = new SqlParameter();
        nameParameter.ParameterName = "@studentName";
        nameParameter.Value = this.GetName();
        cmd.Parameters.Add(nameParameter);

        SqlParameter doeParameter = new SqlParameter();
        doeParameter.ParameterName = "@studentDoe";
        doeParameter.Value = this.GetDoe();
        cmd.Parameters.Add(doeParameter);
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

    public static List<Student> GetAll()
    {
      List<Student> allStudents = new List<Student>{};

      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM students;", conn);
      SqlDataReader rdr  = cmd.ExecuteReader();

      while(rdr.Read())
      {
      int studentId = rdr.GetInt32(0);
      string studentName = rdr.GetString(1);
      string studentDoe = rdr.GetString(2);
      Student newStudent = new Student(studentName, studentDoe, studentId);
      allStudents.Add(newStudent);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allStudents;
     }

     public static void DeleteAll()
     {
       SqlConnection conn = DB.Connection();
       conn.Open();
       SqlCommand cmd = new SqlCommand ("DELETE FROM students;", conn);
       cmd.ExecuteNonQuery();
       conn.Close();
     }


  }
}
