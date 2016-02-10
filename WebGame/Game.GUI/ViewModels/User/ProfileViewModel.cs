using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.User
{
    public class ProfileViewModel
    {
        public int User_ID { get; set; }
        public string Login { get; set; }
        public List<string> Friend_Login { get; set; }
        public List<string> Ignored_Login { get; set; }
        public int Value { get; set; }
        public bool Ignor { get; set; }
        public bool IfIgnored { get; set; }
        public int RegDays { get; set; }
        public int LogDays { get; set; }
        public int Number { get; set; }
        public string Theme { get; set; }
        public string Content { get; set; }

        [Required]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password")]
        public string ComparePassword { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public bool allDiv { get; set; }
        public bool setDiv { get; set; }

    }
}