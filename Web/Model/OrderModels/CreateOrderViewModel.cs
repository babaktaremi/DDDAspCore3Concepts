using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.OrderApplication.Commands.Create;
using Web.Model.Common.MapperConfiguration;

namespace Web.Model.OrderModels
{
    public class CreateOrderViewModel:ICreateMapper<OrderCreateCommand>
    {
        [Required]
        [Range(1,10000 ,ErrorMessage = "Given Value Not Valid")]
        public int NumberOfItems { get; set; }
        [Required]
        public int TotalPrice { get; set; }
    }
}
