using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class PartnerProduct
{
    public int PartnerProductId { get; set; }

    public int ProductId { get; set; }

    public int PartnerId { get; set; }

    public int PartnerProductAmount { get; set; }

    public DateOnly PartnerProductDate { get; set; }

    public virtual Partner Partner { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
