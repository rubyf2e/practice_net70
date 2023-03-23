using System;
using Microsoft.AspNetCore.Identity;

namespace practiceNet70.Models
{
	public class User : IdentityUser
    {
        [PersonalData]
        public string? Name { get; set; }
    }
}

