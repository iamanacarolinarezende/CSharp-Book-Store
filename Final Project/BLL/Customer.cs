using Final_Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.BLL
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public decimal CreditLimit { get; set; }
        public DateTime DateTimeSince {  get; set; }
        public string Email { get; set; }

        //Save Customer
        public void SaveCustomer(Customer customer) => CustomerDB.SaveCustomerInfo(customer);

        //Update Customer
        public void UpdateCustomer(Customer customer) => CustomerDB.UpdateCustomerInfo(customer);

        //List all Customers
        public List<Customer> GetCustomerList()
        {
            return CustomerDB.GetAllCustomers();
        }

        //Delete Customer
        public void DeleteCustomer(string Phone) => CustomerDB.DeleteRecord(Phone);

        //Search by Customer Phone
        public Customer SearchCustomerPhone(string Phone) => CustomerDB.SearchRecord(Phone);

        //Search by Customer First or Last Name
        public List<Customer> SearchCustomerby(string Name) => CustomerDB.SearchRecordName(Name);

        //Search by Customer First and Last Name
        public List<Customer> SearchCustomerbyFullName(string FName, string LName) => CustomerDB.SearchRecordFLName(FName, LName);

        //check if Customer is unique at table of Customers.
        public bool IsUniqueCustomer(string Phone) => CustomerDB.IsUniquePhoneNumber(Phone);

        public bool ActiveCustomer(string Phone) => CustomerDB.IsCustomerActive(Phone);

    }
}
