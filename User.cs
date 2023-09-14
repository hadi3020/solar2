using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solar_Panel.Models;

public partial class User
{
    public int UId { get; set; }

    [Required(ErrorMessage = "Must provide Username")]
    [Display(Name = "First Name")]
    [DataType(DataType.Text)]
    public string Fname { get; set; } = null!;

    [Required(ErrorMessage = "Must provide Username")]
    [Display(Name = "Last Name")]
    [DataType(DataType.Text)]
    public string Lname { get; set; } = null!;

    [Required(ErrorMessage = "Must provide Username")]
    [Display(Name = "User Name")]
    [DataType(DataType.Text)]
    public string Uname { get; set; } = null!;

    [Required]
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    [StringLength(15, MinimumLength = 5, ErrorMessage = "Password must be between 5 to 15 characters")]
    public string Upass { get; set; } = null!;

    [Required]
    [Display(Name = "Contact")]
    [DataType(DataType.Text)]
    [RegularExpression("^03[0-9]{2}-[0-9]{7}$", ErrorMessage = "Contact must be in the format 03xx-xxxxxxx")]
    public string Contact { get; set; } = null!;

    [Required]
    [Display(Name = "Address")]
    [DataType(DataType.MultilineText)]
    public string? Uadd { get; set; }


    //public int UId { get; set; }

    //public string Fname { get; set; } = null!;

    //public string Lname { get; set; } = null!;

    //public string Uname { get; set; } = null!;

    //public string Email { get; set; } = null!;

    //public string Upass { get; set; } = null!;

    //public string Contact { get; set; } = null!;

    //public string? Uadd { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
