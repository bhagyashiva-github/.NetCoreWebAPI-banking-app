using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreAPI_banking_app.Models;



[Table("instruments")]
public partial class Instrument
{
    public int Instrumentid { get; set; }

    public string Name { get; set; } = null!;

    public string? Type { get; set; }

    public string? Market { get; set; }

    public string? Tickersymbol { get; set; }

    public string? Currency { get; set; }

    public virtual ICollection<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

    public virtual ICollection<Trade> Trades { get; set; } = new List<Trade>();
}
