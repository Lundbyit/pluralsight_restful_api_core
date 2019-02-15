using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pluralsight_restful_api_core.Models
{
    public abstract class LinkedResourceBaseDto
    {
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
