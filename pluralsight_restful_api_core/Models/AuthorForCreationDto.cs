using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pluralsight_restful_api_core.Models
{
    public class AuthorForCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Genre { get; set; }

        public ICollection<BookForCreationDto> Books { get; set; } = new List<BookForCreationDto>();
    }
}
