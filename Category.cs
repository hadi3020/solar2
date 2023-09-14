using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solar_Panel.Models;

public partial class Category
{
    public int Cid { get; set; }

    [Display(Name = "Category Name")]
    public string Cname { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
