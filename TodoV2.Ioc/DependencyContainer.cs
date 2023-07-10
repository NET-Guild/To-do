using System;
using System.IO;
using System.Reflection;
using Application.IServices;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TodoV2.Infrastructure.IRepository;
using TodoV2.Infrastructure.Repository;

namespace TodoV2.Ioc
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region service

            services.AddScoped<ITodoService, TodoService>();

            #endregion

            #region Repository

            services.AddScoped<ITodoRepository, TodoRepository>();

            #endregion

            #region settings

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #endregion
        }
    }
}