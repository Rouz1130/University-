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
    
  }
}
