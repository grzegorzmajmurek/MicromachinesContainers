﻿using System.ComponentModel.DataAnnotations;

namespace UserService.Model
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Funds { get; set; }
    }
}
