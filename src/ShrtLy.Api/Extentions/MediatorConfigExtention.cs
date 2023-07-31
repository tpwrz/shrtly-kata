using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ShrtLy.Application;

namespace ShrtLy.Api.Extencions
{
    public static class MediatorConfigExtention
    {
        public static void AddMediator(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<MediatorEntryPoint>());
        }
    }
}
