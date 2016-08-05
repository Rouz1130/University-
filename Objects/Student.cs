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

    public static Student Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students WHERE id = @studentId;", conn);
      SqlParameter studentIdParameter = new SqlParameter();
      studentIdParameter.ParameterName =  "@studentId";
      studentIdParameter.Value = id.ToString();
      cmd.Parameters.Add(studentIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int findStudentId = 0;
      string findStudentName = null;
      string findStudentDoe = null;
      while(rdr.Read())
      {
        findStudentId = rdr.GetInt32(0);
        findStudentName = rdr.GetString(1);
        findStudentDoe = rdr.GetString(2);
      }
      Student findStudent = new Student(findStudentName,findStudentDoe,findStudentId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return findStudent;

    }

    public void Update(string Name)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE students SET name =@studentName output inserted.name WHERE id =@studentId;", conn);
      SqlParameter StudentNameParameter = new SqlParameter();
      StudentNameParameter.ParameterName = "@studentName";
      StudentNameParameter.Value = Name;

      SqlParameter StudentIdParameter = new SqlParameter();
      StudentIdParameter.ParameterName = "@StudentId";
      StudentIdParameter.Value = this.GetId();

      cmd.Parameters.Add(StudentNameParameter);
      cmd.Parameters.Add(StudentIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
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
        SqlCommand cmd = new SqlCommand ("DELETE FROM students WHERE id =@studentId;", conn);

        SqlParameter studentIdParameter = new SqlParameter();
        studentIdParameter.ParameterName = "@studentId";
        studentIdParameter.Value=this.GetId();
        // Console.WriteLine(this.GetId());
        cmd.Parameters.Add(studentIdParameter);
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
      SqlCommand cmd = new SqlCommand ("DELETE FROM students;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public void AddCourse(Course newCourse)
    {

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO courses_students (course_id, student_id) VALUES (@CourseId, @StudentId);", conn);

      SqlParameter courseIdParameter = new SqlParameter();
      courseIdParameter.ParameterName = "@CourseId";
      courseIdParameter.Value = newCourse.GetId();
      cmd.Parameters.Add(courseIdParameter);

      SqlParameter studentIdParameter = new SqlParameter();
      studentIdParameter.ParameterName = "@StudentId";
      studentIdParameter.Value = this.GetId();
      cmd.Parameters.Add(studentIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }

    }

  public List<Course> GetCourses()
{
  SqlConnection conn = DB.Connection();
  conn.Open();

  SqlCommand cmd = new SqlCommand("SELECT courses.* FROM students JOIN courses_students ON (students.id = courses_students.student_id) JOIN courses ON (courses_students.course_id = courses.id) WHERE students.id = @StudentId", conn);
  SqlParameter CourseIdParam = new SqlParameter();
  CourseIdParam.ParameterName = "@StudentId";
  CourseIdParam.Value = this.GetId().ToString();

  cmd.Parameters.Add(CourseIdParam);

  SqlDataReader rdr = cmd.ExecuteReader();

  List<Course> courses = new List<Course>{};

  while(rdr.Read())
  {
    int courseId = rdr.GetInt32(2);
   string thisCourseName = rdr.GetString(0);
   string thisCourseNumber = rdr.GetString(1);
   Course newCourse = new Course(thisCourseName,thisCourseNumber,courseId);
   courses.Add(newCourse);
  }

  if (rdr != null)
  {
    rdr.Close();
  }
  if (conn != null)
  {
    conn.Close();
  }
  return courses;
}
}
}
