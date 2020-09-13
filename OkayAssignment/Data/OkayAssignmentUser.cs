using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OkayAssignment.Data
{
    public class OkayAssignmentUser:IdentityUser
    {
        public bool IsAdmin { get; set; }
    }
}
