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

        //Save Customer
        public void SaveCustomer(Customer customer) => CustomerDB.SaveCustomerInfo(customer);

        //List all Customers
        public List<Customer> GetCustomerList()
        {
            return CustomerDB.GetAllCustomers();
        }

        //Delete Customer
        public void DeleteCustomer(string Phone) => CustomerDB.DeleteRecord(Phone);

        //Search user by Customer Phone
        public Customer SearchCustomerPhone(string Phone) => CustomerDB.SearchRecord(Phone);

        //check if Customer is unique at table of Customers.
        public bool IsUniqueCustomer(string Phone) => CustomerDB.IsUniquePhoneNumber(Phone);

    }
}
