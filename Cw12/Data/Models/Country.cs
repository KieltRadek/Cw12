﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cw12.Data.Models
{
    public partial class Country
    {
        public Country() { CountryTrips = new HashSet<CountryTrip>(); }

        public int IdCountry { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CountryTrip> CountryTrips { get; set; }
    }
}