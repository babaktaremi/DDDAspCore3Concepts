using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model.AccountModels
{
    public class GetTokenViewModel
    {
        [Required]
        public string UserKey { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
