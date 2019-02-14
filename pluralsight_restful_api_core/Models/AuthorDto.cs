using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pluralsight_restful_api_core.Models
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Genre { get; set; }
    }
}
