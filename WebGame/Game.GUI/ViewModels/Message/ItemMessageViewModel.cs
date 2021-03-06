﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Message
{
    public class ItemMessageViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Theme { get; set; }
        [Required]
        public string Content { get; set; }
        public int Customer_ID { get; set; }
        public string Customer_Login { get; set; }
        public int Sender_ID { get; set; }
        public string Sender_Login { get; set; }
        public bool IfRead { get; set; }
        public string Read { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime SentDate { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        public bool allDiv { get; set; }
        public bool messageDiv { get; set; }
    }
}