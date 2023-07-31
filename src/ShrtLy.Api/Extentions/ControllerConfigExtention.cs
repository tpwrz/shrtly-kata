using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using System;
using System.Text.Json.Serialization;

namespace ShrtLy.Api.Extencions
{
    public static class ControllerConfigExtention
    {
        [Obsolete("Obsolete")]
        public static void AddControllersConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        }
    }
}
