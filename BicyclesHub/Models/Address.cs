using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BicyclesHub.Models
{
    public class Address
    {
        public string Street {  get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public Address(string street, string city, string state, string zip_code) {
            this.Street = street;
            this.City = city;
            this.State = state;
            this.ZipCode = zip_code;
        }

        public string getAddress()
        {
            return this.Street + " " + this.City + " " + this.State + " " + this.ZipCode;
        }

        public string getCity() { return this.City; }
        public string getState() { return this.State; }
        public string getStreet() { return this.Street; }
        public string getZipCode() {  return this.ZipCode; }
    }
}