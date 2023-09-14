using System;
using System.Collections.Generic;

namespace Solar_Panel.Models;

public partial class Contact
{
    public int Cid { get; set; }

    public string Cname { get; set; } = null!;

    public string? Cemail { get; set; }

    public string? Csubject { get; set; }

    public string? Cmessage { get; set; }
}
