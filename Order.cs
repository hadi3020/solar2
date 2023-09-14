using System;
using System.Collections.Generic;

namespace Solar_Panel.Models;

public partial class Order
{
    public int OId { get; set; }

    public string Dates { get; set; } = null!;

    public int? UId { get; set; }

    public int? TPrice { get; set; }

    public string? Status { get; set; }

    public string? Address { get; set; }

    public string? Message { get; set; }

    public string? Contact { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User? UIdNavigation { get; set; }
}
