using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solar_Panel.Models;

public partial class Product
{
    public int PId { get; set; }

    [Required(ErrorMessage = "Must provide name")]
    [Display(Name = "Product Name")]
    public string Pname { get; set; } = null!;

    [Required(ErrorMessage = "Must provide price")]
    [Display(Name = "Price")]
    public string Pprice { get; set; } = null!;

    [Required(ErrorMessage = "Must provide details")]
    [Display(Name = "Details")]
    public string Pdetails { get; set; } = null!;

    //[Required(ErrorMessage = "Must provide image")]
    [Display(Name = "Image")]
    public string? Pimage { get; set; }

    public int? Pcat { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Category? PcatNavigation { get; set; }
}
