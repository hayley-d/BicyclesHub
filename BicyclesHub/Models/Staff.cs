using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BicyclesHub.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte Active { get; set; }
        public int StoreId { get; set; }
        public int ManagerId { get; set; }

        public Staff(int id, string firstName, string lastName, string email, string phoneNumber, byte active, int storeId, int managerId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Active = active;
            StoreId = storeId;
            ManagerId = managerId;
        }
    }
}