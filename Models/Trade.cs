using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreAPI_banking_app.Models;

[Table("trades")]
public partial class Trade
{
    public int Tradeid { get; set; }

    public int? Clientid { get; set; }

    public int? Instrumentid { get; set; }

    public string? Tradetype { get; set; }

    public int? Quantity { get; set; }

    public decimal? Priceperunit { get; set; }

    public DateOnly? Tradedate { get; set; }

    public DateOnly? Settlementdate { get; set; }

    public string? Brokername { get; set; }

    public string? Tradestatus { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Instrument? Instrument { get; set; }
}
