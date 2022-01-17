using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenTecnico.Request
{
    public class ContractRequest
    {
        [Required]
        [MaxLength(150)]
        public string Description { get; set; }

        public bool ActiveState { get; set; }
    }
}
