using System;
using System.Collections.Generic;

namespace ESFEG06.Entities;

public partial class QuotationDetail
{
    public long QuotationDetailId { get; set; }

    public long? QuotationId { get; set; }

    public long? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Discount { get; set; }

    public decimal? Subtotal { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Quotation? Quotation { get; set; }
}
