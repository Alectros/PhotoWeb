using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Autofac.Extensions.DependencyInjection;

namespace PhotoWEB
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .ConfigureServices(services => services.AddAutofac())
                .UseIISIntegration()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();
            host.Run();
        }
        
        

    }
}
