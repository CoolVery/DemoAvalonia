using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class Partner
{
    public int PartnerId { get; set; }

    public int PartnerTypeId { get; set; }

    public string PartnerName { get; set; } = null!;

    public string PartnerDirectorCredentials { get; set; } = null!;

    public string? PartnerEmail { get; set; }

    public string? PartnerPhone { get; set; }

    public string? PartnerAdress { get; set; }

    public string PartnerInn { get; set; } = null!;

    public int? PartnerRating { get; set; }

    public virtual ICollection<PartnerProduct> PartnerProducts { get; set; } = new List<PartnerProduct>();

    public virtual PartnerType PartnerType { get; set; } = null!;
}
