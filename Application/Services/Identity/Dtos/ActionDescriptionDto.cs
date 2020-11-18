﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Identity.Dtos
{
   public class ActionDescriptionDto
    {
        public string Key => $"{AreaName}:{ControllerName}:{ActionName}";

        public string AreaName { get; set; }

        public string ControllerName { get; set; }
        public string ControllerDisplayName { get; set; }

        public string ActionName { get; set; }

        public string ActionDisplayName { get; set; }
    }
}
