using MvcCrud1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;   

namespace MvcCrud1.Controllers
{
    public class RegistrationController : Controller
    {
        SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["StudentRegistration"].ToString());
        // GET: Main
        public ActionResult Index()
        {
            SqlCommand cmd = new SqlCommand("Proc_Student_GetAll", sqlConn);
            cmd.CommandType = CommandType.StoredProcedure;
            sqlConn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            sqlConn.Close();
            List<Student> listStudent = new List<Student>();
            foreach (DataRow rw in dt.Rows)
            {
                listStudent.Add(
                    new Student
                    {
                        StudentId = Convert.ToInt32(rw["StudentId"]),
                        Prefix = (PrefixOfName)Enum.Parse(typeof(PrefixOfName), rw["Prefix"].ToString()),
                        FirstName = Convert.ToString(rw["FirstName"]),
                        LastName = Convert.ToString(rw["LastName"]),
                        Gender = Convert.ToString(rw["Gender"]),
                        Email = Convert.ToString(rw["Email"]),
                        PhoneNo = Convert.ToString(rw["PhoneNo"]),
                        Password = Convert.ToString(rw["Password"])
                        
                    }
                );
            }
            return View(listStudent);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student oStudent)
            {
            if(ModelState.IsValid)
            {
                SqlCommand cmd = new SqlCommand("Proc_Student_Insert", sqlConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Prefix",oStudent.Prefix);
                cmd.Parameters.AddWithValue("@FirstName", oStudent.FirstName);
                cmd.Parameters.AddWithValue("@LastName", oStudent.LastName);
                cmd.Parameters.AddWithValue("@Gender", oStudent.Gender);
                cmd.Parameters.AddWithValue("@PhoneNo", oStudent.PhoneNo);
                cmd.Parameters.AddWithValue("@Email", oStudent.Email);
                cmd.Parameters.AddWithValue("@Password", oStudent.Password);
                sqlConn.Open();
                int res = (int)cmd.ExecuteScalar();
                if(res > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    View(oStudent);
                }
            }
            else
            {
                return View(oStudent);
            }
            return View();
        }

        
        public ActionResult DeleteStudent(int id)
        {
            SqlCommand cmd = new SqlCommand("Proc_Student_Delete", sqlConn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", id);
            sqlConn.Open();
            try
            {
                cmd.ExecuteScalar();
                sqlConn.Close();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                throw;
            }
        }

        public ActionResult Details(int id)
        {
            SqlCommand cmd = new SqlCommand("Proc_Student_GetDetilsById", sqlConn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", id);
            sqlConn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            sqlConn.Close();

            Student student = new Student();
            student.StudentId = Convert.ToInt32(dt.Rows[0]["StudentId"]);
            student.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
            student.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
            student.PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]);
            student.Password = Convert.ToString(dt.Rows[0]["Password"]);
            student.Email = Convert.ToString(dt.Rows[0]["Email"]);
            student.Gender = Convert.ToString(dt.Rows[0]["Gender"]);
            //student.Prefix = PrefixOfName(dt.Rows[0]["Prefix"]);
            return View(student);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SqlCommand cmd = new SqlCommand("Proc_Student_GetDetilsById", sqlConn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", id);
            sqlConn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            sqlConn.Close();
            Student student = new Student();
            student.StudentId = Convert.ToInt32(dt.Rows[0]["StudentId"]);
            student.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
            student.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
            student.PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]);
            student.Password = Convert.ToString(dt.Rows[0]["Password"]);
            student.Email = Convert.ToString(dt.Rows[0]["Email"]);
            student.Gender = Convert.ToString(dt.Rows[0]["Gender"]);
            //student.Prefix = PrefixOfName(dt.Rows[0]["Prefix"]);
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(Student model)
        {
            SqlCommand cmd = new SqlCommand("Proc_Student_Update", sqlConn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", model.StudentId);
            cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
            cmd.Parameters.AddWithValue("@LastName", model.LastName);
            cmd.Parameters.AddWithValue("@Email", model.Email);
            cmd.Parameters.AddWithValue("@Gender", model.Gender);
            cmd.Parameters.AddWithValue("@PhoneNo", model.PhoneNo);
            cmd.Parameters.AddWithValue("@Password", model.Password);
            cmd.Parameters.AddWithValue("@Prefix", "Mr");
            sqlConn.Open();
            cmd.ExecuteScalar();
            sqlConn.Close();
            return RedirectToAction("Details",new { id = model.StudentId });
        }
    }
}