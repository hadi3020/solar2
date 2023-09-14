using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solar_Panel.Models;

public partial class Booking
{
    public int BId { get; set; }

    [Required(ErrorMessage = "Must provide name")]
    [Display(Name = "User Name")]
    public string? BName { get; set; }

    [Required(ErrorMessage = "Must provide email")]
    [Display(Name = "User Email")]
    public string? BEmail { get; set; }

    [Required(ErrorMessage = "Must provide contact")]
    [Display(Name = "User Contact")]
    public string? BContact { get; set; }

    [Required(ErrorMessage = "Must provide ddress")]
    [Display(Name = "User Adress")]
    public string? BAddress { get; set; }

    public int? SId { get; set; }

    public virtual Servicee? SIdNavigation { get; set; }
}
