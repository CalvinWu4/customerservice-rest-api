using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CustomerServiceRESTAPI.Entities;
using CustomerServiceRESTAPI.Services;

namespace CustomerServiceRESTAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string URL_CONNECTION_STRING = Configuration["DB"];

            services.AddMvc();
            services.AddDbContext<Context>(o => o.UseMySql(URL_CONNECTION_STRING));

            services.AddScoped<ClientRepository>();
            services.AddScoped<TicketRepository>();
            services.AddScoped<ReviewRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Client, Models.ClientDto>();
                cfg.CreateMap<Entities.Client, Models.ClientWithTicketsDto>();
                cfg.CreateMap<Models.ClientForCreationDto, Entities.Client>();

                cfg.CreateMap<Entities.Ticket, Models.TicketDto>();
                cfg.CreateMap<Entities.Ticket, Models.TicketWithClientDto>();
                cfg.CreateMap<Models.TicketForCreationDto, Entities.Ticket>();

                cfg.CreateMap<Entities.Review, Models.ReviewDto>();
                cfg.CreateMap<Models.ReviewDtoForCreation, Entities.Review>();
            });

            app.UseMvc();
        }
    }
}
