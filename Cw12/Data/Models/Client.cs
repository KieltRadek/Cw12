﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cw12.Data.Models
{
    public partial class Client
    {
        public Client() { ClientTrips = new HashSet<ClientTrip>(); }

        public int IdClient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Pesel { get; set; }

        public virtual ICollection<ClientTrip> ClientTrips { get; set; }
    }
}