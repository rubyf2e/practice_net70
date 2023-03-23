using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace practiceNet70.Models
{
	public class User : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        [PersonalData]
        public string? Name { get; set; } = string.Empty;
    }
}

