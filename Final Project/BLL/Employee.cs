using Final_Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.BLL
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string JobTitle { get; set; }

        //Save Employee
        public void SaveEmployee(Employee emp) => EmployeeDB.SaveEmployee(emp);

        //Update Employee
        public void UpdateEmployee(Employee emp) => EmployeeDB.UpdateRecord(emp);

        //Delete Employee
        public void DeleteEmployee(int empId) => EmployeeDB.DeleteRecord(empId);

        //Unique Employee ID
        public bool IsUniqueEmployeeId(int empId) => EmployeeDB.IsUniqueId(empId);

        //List all employees
        public List<Employee> GetEmployeeList() => EmployeeDB.GetAllRecords();

        //Search by Employee ID
        public Employee SearchEmployeeID (int empId) => EmployeeDB.SearchRecord(empId);

        //Search by Employee Name or Last Name
        public List<Employee> SearchEmployeeName(string inputN) => EmployeeDB.SearchRecord(inputN);

        //Search by Employee First name
        public List<Employee> SearchEmployeeFLName(string inputF, string inputL) => EmployeeDB.SearchRecord(inputF, inputL);
    }
}
