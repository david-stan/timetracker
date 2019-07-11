using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TimeTrackerApp.Domain
{
    public class Client
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
