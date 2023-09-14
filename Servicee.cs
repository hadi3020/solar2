using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solar_Panel.Models;

public partial class Servicee
{
    public int SId { get; set; }

    [Required(ErrorMessage = "Must provide name")]
    [Display(Name = "Package Name")]
    public string SName { get; set; } = null!;

    //[Required(ErrorMessage = "Must provide image")]
    [Display(Name = "Image")]
    public string? SImage { get; set; }

    [Required(ErrorMessage = "Must provide details")]
    [Display(Name = "Details")]
    public string? SDetails { get; set; }

    [Required(ErrorMessage = "Must provide price")]
    [Display(Name = "Price")]
    public string? SPrice { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
