﻿using System.ComponentModel.DataAnnotations;

namespace Butter.Web.Models
{
    public class UserModelUpdateForm
    {

        public int Id { get; set; }

        [Required]
        public string NickName { get; set; }


        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@#$%^&+=!])(?!.*\\s).{8,}$", ErrorMessage = "Votre mot de passe")]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }

        [EmailAddress]
        public string Email { get; set; }


        [DataType(DataType.Date)]
        //remarque ajouter contrainte date +de 18 ans
        public DateTime BirthDate { get; set; }


        [Required]
        public string Town { get; set; }


        [Required]
        public string Genre { get; set; }
    }
}
