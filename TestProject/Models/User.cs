using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Models
{
    // Represents the user model mapped from the JSON API
    public class User
    {
       
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public Address Address { get; set; }
        
    }
}
