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

    public void Save()
    {
        SqlConnection conn = DB.Connection();
        conn.Open();
    }

    public static List<Student> GetAll()
    {

      return new List<Student>();
    }

  }
}
