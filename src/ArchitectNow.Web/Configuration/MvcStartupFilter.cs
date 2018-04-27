﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace ArchitectNow.Web.Configuration
{
    public class MvcStartupFilter : IStartupFilter{
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                builder.UseMvc();
                next(builder);
            };
        }
    }
}