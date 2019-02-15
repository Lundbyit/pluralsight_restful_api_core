using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pluralsight_restful_api_core.Models
{
    public class BookForUpdateDto : BookForManipulationDto
    {
        [Required(ErrorMessage = "A description is needed")]
        public override string Description { get => base.Description; set => base.Description = value; }
    }
}
