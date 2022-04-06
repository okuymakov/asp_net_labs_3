using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace asp_net_labs_3.Models
{
    public class Customer
    {
        public Customer()
        {
        }

        public Customer(Customer customer)
        {
           
            Id = customer.Id;
            Firstname = customer.Firstname;
            Surname = customer.Surname;
            Patronymic = customer.Patronymic;
            Gender = customer.Gender;
            Dob = customer.Dob;
            Address = customer.Address;
            Phone = customer.Phone;
            Email = customer.Email;
            Orders = customer.Orders;
            PasswordHash = customer.PasswordHash;
        }

        public int Id { get; set; }
        public string Firstname  { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Gender { get; set; } 
        public DateTime? Dob { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();

        public string PasswordHash { get; set; }
    }
}

