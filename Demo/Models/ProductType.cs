using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class ProductType
{
    public int ProductTypeId { get; set; }

    public string ProductTypeName { get; set; } = null!;

    public double ProductTypeRatio { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
