﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.Infra.CrossCutting.Identity.Models
{
    public class SignInResponse
    {
        public string StatusMessage { get; set; }
        public AuthResponse? AuthResponse { get; set; }
    }
}
