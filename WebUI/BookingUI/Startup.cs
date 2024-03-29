﻿using BookingUI.ClientServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;
using Westwind.AspNetCore.LiveReload;

namespace BookingUI
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddLiveReload();

            services.AddRazorPages();

            string apiWriteBaseAddress = Configuration.GetSection("ApiConfiguration:WriteBaseAddress").Value;
            string apiReadBaseAddress = Configuration.GetSection("ApiConfiguration:ReadBaseAddress").Value;

            services.AddHttpClient<HotelWriteService>(httpConfiguration =>
            {
                httpConfiguration.BaseAddress = new Uri(apiWriteBaseAddress);
                httpConfiguration.DefaultRequestHeaders.Add("Accept", "application/json");
                httpConfiguration.DefaultRequestHeaders.Add("User-Agent", "EmergingBooking");
            })
            .AddPolicyHandler(GetWriteRetryPolicy());

            services.AddHttpClient<HotelReadService>(httpConfiguration =>
            {
                httpConfiguration.BaseAddress = new Uri(apiReadBaseAddress);
                httpConfiguration.DefaultRequestHeaders.Add("Accept", "application/json");
                httpConfiguration.DefaultRequestHeaders.Add("User-Agent", "EmergingBooking");
            });//.AddPolicyHandler(GetReadRetryPolicy());

            services.AddHttpClient<BookingWriteService>(httpConfiguration =>
            {
                httpConfiguration.BaseAddress = new Uri(apiWriteBaseAddress);
                httpConfiguration.DefaultRequestHeaders.Add("Accept", "application/json");
                httpConfiguration.DefaultRequestHeaders.Add("User-Agent", "EmergingBooking");
            })
            .AddPolicyHandler(GetWriteRetryPolicy()); ;

            services.AddHttpClient<BookingReadService>(httpConfiguration =>
            {
                httpConfiguration.BaseAddress = new Uri(apiReadBaseAddress);
                httpConfiguration.DefaultRequestHeaders.Add("Accept", "application/json");
                httpConfiguration.DefaultRequestHeaders.Add("User-Agent", "EmergingBooking");
            }).AddPolicyHandler(GetReadRetryPolicy());
        }

        private IAsyncPolicy<HttpResponseMessage> GetWriteRetryPolicy()
        {
            return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        private IAsyncPolicy<HttpResponseMessage> GetReadRetryPolicy()
        {
            return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseLiveReload();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
