using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreAPI_banking_app.Models;

[Table("clients")]
public partial class Client
{
    public int Clientid { get; set; }

    public string Fullname { get; set; } = null!;

    public string? Email { get; set; }

    public string? Contactnumber { get; set; }

    public string? Accounttype { get; set; }

    public DateTime? Createdon { get; set; }

    public virtual ICollection<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

    public virtual ICollection<Trade> Trades { get; set; } = new List<Trade>();
}
