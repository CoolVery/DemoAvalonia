using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int ProductTypeId { get; set; }

    public string ProductName { get; set; } = null!;

    public string ProductArticul { get; set; } = null!;

    public double ProductMinPrice { get; set; }

    public virtual ICollection<PartnerProduct> PartnerProducts { get; set; } = new List<PartnerProduct>();

    public virtual ProductType ProductType { get; set; } = null!;
}
