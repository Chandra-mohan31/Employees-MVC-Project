using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using mvc_asp_dotnet_project2.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System;

namespace mvc_asp_dotnet_project2.Controllers
{
    public class EmployeeController : Controller
    {
        //IConfiguration configuration;
        //public EmployeeController(IConfiguration configuration)
        //{
        //    this.configuration = configuration;
        //}

        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            SqlConnection conn = new SqlConnection("Data Source = F48DPF2; Initial Catalog = PracticeDatabase; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False");
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Employee";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                EmployeeModel emp = new EmployeeModel();
                float tmpSalary = Convert.ToSingle(reader["salary"]);
                emp.EmpId = (int)reader["empId"];
                emp.EmpName = (string)reader["empName"];
                emp.dateOfBirth = (DateTime)reader["dateOfBirth"];
                emp.Technology = (string)reader["technology"];
                emp.Email = "temp@gmail.com";
                emp.Salary = tmpSalary;
                emp.BaseLocation = (string)reader["baselocation"];
                
                employees.Add( emp );
            }
            reader.Close();
            conn.Close();
            return employees;



        }
        // GET: EmployeeController
        public ActionResult Index()
        {
            return View(GetEmployees());
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View(GetEmployeeData(id));
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create

        public void AddEmployee(EmployeeModel emp)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source = F48DPF2; Initial Catalog = PracticeDatabase; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False");
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                string dept = "bankiing1";
                Console.WriteLine(emp.dateOfBirth.ToString("yyyy-MM-dd"));
                cmd.CommandText = $"insert into Employee(empName,dateOfBirth,department,technology,baselocation,salary) values('{emp.EmpName}','{emp.dateOfBirth.ToString("yyyy-MM-dd")}','{dept}','{emp.Technology}','{emp.BaseLocation}',{emp.Salary})";
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine(rowsAffected);
            }
            catch(SqlException se)
            {
                Console.WriteLine(se.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeModel emp)
        {
            AddEmployee(emp);
            try
            {

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        EmployeeModel GetEmployeeData(int id)
        {
            EmployeeModel emp = new();

            try
            {
                SqlConnection conn = new SqlConnection("Data Source = F48DPF2; Initial Catalog = PracticeDatabase; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False");
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"select * from Employee where empId = {id}";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    emp.EmpId = (int)reader["empId"];
                    emp.EmpName = (string)reader["empName"];
                    emp.dateOfBirth = (DateTime)reader["dateOfBirth"];
                    emp.Technology = (string)reader["technology"];
                    emp.Email = "temp@gmail.com";
                    float tmpSalary = Convert.ToSingle(reader["salary"]);

                    emp.Salary = tmpSalary;
                    emp.BaseLocation = (string)reader["baselocation"];

                }
                

                reader.Close();
                conn.Close();
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }
            Console.WriteLine("Emp ID" + emp.EmpId);
            return emp;

        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            

            return View(GetEmployeeData(id));
        }

         void updateEmployee(int id,EmployeeModel emp)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source = F48DPF2; Initial Catalog = PracticeDatabase; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False");
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                string dept = "banking2";
                float tmpSalary = Convert.ToSingle(emp.Salary);

                cmd.CommandText = $"update Employee set empName = '{emp.EmpName}',department = '{dept}',technology = '{emp.Technology}',baselocation = '{emp.BaseLocation}',dateOfBirth='{emp.dateOfBirth.ToString("yyyy-MM-dd")}',salary={tmpSalary} where empId = {emp.EmpId}";
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine(rowsAffected);
            }catch(SqlException se)
            {
                Console.WriteLine(se.Message);
            }
        }

     
        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,EmployeeModel emp)
        {
            
            try
            {
                Console.WriteLine("inside edit post");
                Console.WriteLine(id);
                updateEmployee(id, emp);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        void DeleteEmp(int id)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source = F48DPF2; Initial Catalog = PracticeDatabase; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False");
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                string dept = "banking2";
                cmd.CommandText = $"delete from Employee where empId = {id}";
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine(rowsAffected);
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }
        }
        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {


            return View(GetEmployeeData(id));
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                DeleteEmp(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
