using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Gateway
{
    public class StudentGateway:CommonGateway
    {
        public string GetLastAddedStudentRegistration(string searchKey)
        {
            Query = "SELECT * FROM Students WHERE RegistrationNo LIKE '%" + searchKey +
                    "%' and Id=(select Max(Id) FROM Students WHERE RegistrationNo LIKE '%" + searchKey + "%' )";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Student student = null;
            string RegNo = null;
            SqlDataReader reader = Command.ExecuteReader();
            if (reader.Read())
            {
                student = new Student
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    RegistrationNo = reader["RegistrationNo"].ToString(),
                    Name = reader["Name"].ToString(),
                    Email = reader["Email"].ToString(),
                    ContactNo = reader["ContactNo"].ToString(),

                };
                RegNo = student.RegistrationNo;
            }

            Connection.Close();
            Command.Dispose();
            reader.Close();
            return RegNo;


        }
        public int Save(Student student)
        {
            Query = "INSERT INTO Students(RegistrationNo,Name,Email,ContactNo,RegistrationDate,Address,DepartmentId)" +
                    " VALUES(@RegistrationNo,@Name,@Email,@ContactNo,@RegistrationDate,@Address,@DepartmentId)";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();

            Command.Parameters.Add("RegistrationNo", SqlDbType.VarChar);
            Command.Parameters["RegistrationNo"].Value = student.RegistrationNo;

            Command.Parameters.Add("Name", SqlDbType.VarChar);
            Command.Parameters["Name"].Value = student.Name;

            Command.Parameters.Add("Email", SqlDbType.VarChar);
            Command.Parameters["Email"].Value = student.Email;

            Command.Parameters.Add("ContactNo", SqlDbType.VarChar);
            Command.Parameters["ContactNo"].Value = student.ContactNo;

            Command.Parameters.Add("RegistrationDate", SqlDbType.Date);
            Command.Parameters["RegistrationDate"].Value = student.RegistrationDate;

            Command.Parameters.Add("Address", SqlDbType.VarChar);
            Command.Parameters["Address"].Value = student.Address;

            Command.Parameters.Add("DepartmentId", SqlDbType.Int);
            Command.Parameters["DepartmentId"].Value = student.DepartmentId;

            Connection.Open();
            int rowsAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowsAffected;
        }

        public bool IsEmailExist(string email)
        {
            Query = "SELECT *FROM Students WHERE Email='" + email + "'";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            bool isEmailExist = reader.HasRows;
            reader.Close();
            Connection.Close();
            return isEmailExist;
        }


        public List<Student> GetAll()
        {
            Query = "SELECT * FROM Students";
            SqlCommand Command = new SqlCommand(Query, Connection);
            List<Student> studenList = new List<Student>();
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();

            while (reader.Read())
            {
                Student student = new Student();

                student.Id = Convert.ToInt32(reader["Id"].ToString());
                student.RegistrationNo = reader["RegistrationNo"].ToString();
                student.Name = reader["Name"].ToString();
                student.Email = reader["Email"].ToString();
                student.ContactNo = reader["ContactNo"].ToString();
                student.RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"].ToString());
                student.Address = reader["Address"].ToString();
                student.DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString());
                studenList.Add(student);
            }

            reader.Close();
            return studenList;
        }


        public List<StudentViewModel> GetStudentInformationById(int studentId)
        {
            Query = "SELECT * FROM viewStudentsDepartments";
            SqlCommand Command = new SqlCommand(Query, Connection);
            List<StudentViewModel> studentViewModelList = new List<StudentViewModel>();
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();

            while (reader.Read())
            {
                StudentViewModel studentViewModel = new StudentViewModel();

                studentViewModel.Id = Convert.ToInt32(reader["Id"].ToString());
                studentViewModel.RegNo = reader["RegistrationNo"].ToString();
                studentViewModel.Name = reader["StudentName"].ToString();
                studentViewModel.Email = reader["Email"].ToString();
                studentViewModel.ContactNo = reader["ContactNo"].ToString();
                studentViewModel.RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"].ToString());
                studentViewModel.Address = reader["Address"].ToString();
                studentViewModel.Department = reader["DepartmentName"].ToString();
                studentViewModelList.Add(studentViewModel);
            }

            reader.Close();
            return studentViewModelList;
        }

        public List<Student> GetAllLastStudent()
        {
            Query = "SELECT * FROM Students WHERE Id = (SELECT MAX(Id) FROM Students)";
            SqlCommand Command = new SqlCommand(Query, Connection);
            List<Student> studenList = new List<Student>();
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();

            while (reader.Read())
            {
                Student student = new Student();

                student.Id = Convert.ToInt32(reader["Id"].ToString());
                student.RegistrationNo = reader["RegistrationNo"].ToString();
                student.Name = reader["Name"].ToString();
                student.Email = reader["Email"].ToString();
                student.ContactNo = reader["ContactNo"].ToString();
                student.RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"].ToString());
                student.Address = reader["Address"].ToString();
                student.DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString());
                studenList.Add(student);
            }

            reader.Close();
            return studenList;
        }
    }
}