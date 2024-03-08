using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistance;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Infrastructure.Email;
using CleanArchitecture.Infrastructure.Persistance;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) {
            
            services.AddDbContext<StreamerDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString"))
            );

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IVideoRepository), typeof(VideoRepository));
            services.AddScoped(typeof(IStreamerRepository), typeof(StreamerRepository));

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            return services;

        }
    }
}
