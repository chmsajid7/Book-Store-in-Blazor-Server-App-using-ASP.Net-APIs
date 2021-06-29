using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoresWebAPI.Models
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        public int QuantityPerPage { get; set; } = 10;
        public string LastName { get; set; }
        public string City { get; set; }
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string EmailAddress { get; set; }
        public int Salary { get; set; }
        public string Phone { get; set; }

    }
}
