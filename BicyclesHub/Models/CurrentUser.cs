using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BicyclesHub.Models
{
    public class CurrentUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Boolean IsCustomer { get; set; }
        public Boolean IsOwner { get; set; }

        public CurrentUser(int id, string email, bool isCustomer, bool isOwner)
        {
            Id = id;
            Email = email;
            IsCustomer = isCustomer;
            IsOwner = isOwner;
        }
    }
}