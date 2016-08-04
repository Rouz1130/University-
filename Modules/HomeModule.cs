using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace University.Objects
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };

      Get["/students"] = _ => {
        List<Student> allStudents = Student.GetAll();
        return View["students.cshtml", allStudents];
      };

      Get["/students/new"] = _ => {

        return View["add_student.cshtml"];

      };

      Post["/students/new"] = _ => {
        Student newStudent = new Student(Request.Form["student-name"],Request.Form["student-doe"]);
        newStudent.Save();
        List<Student> allStudents= Student.GetAll();
        return View["students.cshtml", allStudents];
      };

      Get["/students/delete/{id}"] = parameters => {
        Student SelectedStudent = Student.Find(parameters.id);
        return View["confirm_student_delete.cshtml", SelectedStudent];
      };

      Get["students/edit/{id}"] = parameters => {
        Student SelectedStudent = Student.Find(parameters.id);
        return View["student_edit.cshtml", SelectedStudent];
      };

      Patch["students/edit/{id}"] = parameters => {
        Student SelectedStudent = Student.Find(parameters.id);
        SelectedStudent.Update(Request.Form["student-name"]);
        return View["index.cshtml"];
      };



      Delete["/students/delete/{id}"] = parameters => {
        Student SelectedStudent = Student.Find(parameters.id);
        SelectedStudent.Delete();
        return View["index.cshtml"];
      };


      Post["/students/delete"] = _ => {
        Student.DeleteAll();
        return View["index.cshtml"];
      };


      Get["/courses"] = _ => {
        List<Course> allCourses = Course.GetAll();
        return View["courses.cshtml", allCourses];
      };


      Get["/courses/new"] = _ => {

        return View["add_course.cshtml"];

      };

      Post["/courses/new"] = _ => {
        Course newCourse = new Course(Request.Form["course-name"],Request.Form["course-courseNumber"]);
        newCourse.Save();
        List<Course> allCourses= Course.GetAll();
        return View["courses.cshtml", allCourses];
      };


      Get["courses/edit/{id}"] = parameters => {
        Course SelectedCourse = Course.Find(parameters.id);
        return View["course_edit.cshtml", SelectedCourse];
      };

      Patch["courses/edit/{id}"] = parameters => {
        Course SelectedCourse = Course.Find(parameters.id);
        SelectedCourse.Update(Request.Form["course-name"]);
        return View["index.cshtml"];
      };



      Delete["/courses/delete/{id}"] = parameters => {
        Course SelectedCourse = Course.Find(parameters.id);
        SelectedCourse.Delete();
        return View["index.cshtml"];
      };

      Get["/courses/delete/{id}"] = parameters => {
        Course SelectedCourse = Course.Find(parameters.id);
        return View["confirm_course_delete.cshtml", SelectedCourse];
      };


      Post["/courses/delete"] = _ => {
        Course.DeleteAll();
        return View["index.cshtml"];
      };


  }



}

}
