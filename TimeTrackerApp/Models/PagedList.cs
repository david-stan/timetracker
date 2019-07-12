using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTrackerApp.Models
{
    public class PagedList<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Page { get; set; } //trenutna stranica
        public int PageSize { get; set; } //velicina stranice
        public int TotalCount { get; set; } //ukupan broj entiteta
        public int TotalPages => 
            (int) Math.Ceiling(TotalCount / (decimal)PageSize);
    }
}
