using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class MaterialType
{
    public int MaterialTypeId { get; set; }

    public string MaterialTypeName { get; set; } = null!;

    public double MaterialTypeDefectsPercent { get; set; }
}
