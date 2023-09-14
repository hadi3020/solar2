using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solar_Panel.Models;

public partial class OrderItem
{
    public int OIId { get; set; }

    public int Qty { get; set; }

    public int? OId { get; set; }

    public int? PId { get; set; }

    [Display(Name = "User Name")]
    public virtual Order? OIdNavigation { get; set; }

    public virtual Product? PIdNavigation { get; set; }
}
