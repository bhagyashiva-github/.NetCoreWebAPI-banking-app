using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreAPI_banking_app.Models;

[Table("portfolios")]
public partial class Portfolio
{
    public int Portfolioid { get; set; }

    public int? Clientid { get; set; }

    public int? Instrumentid { get; set; }

    public int? Quantityheld { get; set; }

    public decimal? Averagecost { get; set; }

    public DateTime? Lastupdated { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Instrument? Instrument { get; set; }
}
