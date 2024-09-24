using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BicyclesHub.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber {  get; set; }
        public string Email { get; set; }

        public Address Address { get; set; }

        public Customer(int id, string firstName, string lastName, string phoneNumber, string email, string street, string city,string state, string zip_code)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = new Address(street,city,state,zip_code);
        }
    }
}