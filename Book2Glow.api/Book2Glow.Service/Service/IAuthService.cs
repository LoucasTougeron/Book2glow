﻿using Book2Glow.Infrastructure.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Service.Service
{
    public interface IAuthService
    {
        string GenerateJwtToken(ApplicationUser user);
    }
}
