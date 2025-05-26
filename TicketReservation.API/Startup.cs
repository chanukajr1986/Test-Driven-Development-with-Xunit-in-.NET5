using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TicketReservation.Core.DataService;
using TicketReservation.Core.Requests;
using TicketReservation.Db;
using TicketReservation.Db.Repositories;

namespace TicketReservation.API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TicketReservation.API", Version = "v1" });
            });


            var connString = "Filename=:memory:";
            var conn = new SqliteConnection(connString);
            conn.Open();

            services.AddDbContext<TicketReservationDbContext>(opt => opt.UseSqlite(conn));

            EnsureDatabaseCreated(conn);

            services.AddScoped<ITicketReservationService, TicketReservationService>();
            services.AddScoped<ITicketReservationRequeestProcessor, TicketReservationRequeestProcessor>();
        }

        private static void EnsureDatabaseCreated(SqliteConnection conn)
        {
            var builder = new DbContextOptionsBuilder<TicketReservationDbContext>();
            builder.UseSqlite(conn);

            using var context = new TicketReservationDbContext(builder.Options);
            context.Database.EnsureCreated();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketReservation.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
