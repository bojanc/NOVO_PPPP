
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Osiguranje.Models
{

using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Korisnik
{

        [Required(ErrorMessage = "Username je obavezan!")]
        [EmailAddress]
        [Display(Name = "Username")]
        [RegularExpression("^[a-z\\d]+\\.?[a-z\\d]+\\@[a-z]{2,6}\\.[a-z]{2,6}$", ErrorMessage = "Unesite validan username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezna!")]
        [StringLength(100, ErrorMessage = "Lozinka mora biti duza od {2} karaktera.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression("^[a-zA-Z]{6,10}[\\d]{3,5}[!@#$%^&*?]{1}$", ErrorMessage = "Unesite validnu lozinku")]
        public string Password { get; set; }

    public string ErrorMsg { get; set; }

}

}
