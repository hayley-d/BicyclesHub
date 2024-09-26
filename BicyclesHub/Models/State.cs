using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BicyclesHub.Models
{
    public class State
    {
        /// <summary>
        /// The full name of the state.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The abbreviation of the state.
        /// </summary>
        public string Abr { get; set; }

        public State(string name, string abr)
        {
            Name = name;
            Abr = abr;
        }
    }

}