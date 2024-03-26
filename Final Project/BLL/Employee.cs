﻿using Final_Project.DAL;
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
        public List<Employee> GetEmployeeList()
        {
            return EmployeeDB.GetAllRecords();
        }

    }
}
