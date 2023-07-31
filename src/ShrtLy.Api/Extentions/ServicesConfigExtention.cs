using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ShrtLy.Api.ResponceMapper;
using ShrtLy.Application.Services;
using ShrtLy.Contracts;
using ShrtLy.Infrastructure;

namespace ShrtLy.Api.Extentions
{
    public static class ServicesConfigExtention
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IShorteningService, ShorteningService>();
            builder.Services.AddSingleton<IResponseMapper, HttpResponceMapper>();
            builder.Services.AddTransient<ILinksRepository, LinksRepository>();
            builder.Services.AddTransient<ShrtLyContext>();

            builder.Services.AddSwaggerGen();
        }
    }
}
