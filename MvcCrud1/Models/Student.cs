using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcCrud1.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        //[Required]
        public PrefixOfName Prefix { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 4, ErrorMessage = "{0} should be between 4 to 25 Char")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 4, ErrorMessage = "{0} should be between 4 to 25 Char")]
        public string LastName { get; set; }

        //[Required]
        [MaxLength(10,ErrorMessage ="Max 10 char allow")]
        public string Gender { get; set; }

        public Skill SkillSet { get; set; }

        //[Required]
        [MaxLength(10,ErrorMessage ="10 digit must required")]
        public string PhoneNo { get; set; }

        [Required]
        public string Email { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public enum Skill
    {
        DotNet,
        Java,
        php
    }

    public enum PrefixOfName
    {
        Mr,
        Mrs,
        Miss
    }
}