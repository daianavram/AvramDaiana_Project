﻿using System;
using AvramDaiana_FacultyProject.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(AvramDaiana_FacultyProject.Areas.Identity.IdentityHostingStartup))]
namespace AvramDaiana_FacultyProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(
                    context.Configuration.GetConnectionString("IdentityContextConnection")));


                services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddDefaultUI().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            });
        }
    }
}