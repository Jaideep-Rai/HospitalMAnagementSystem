using BAL.Core;
using BAL.Interfaces;
using BAL.Interfaces.AlbumMaster;
using BAL.Interfaces.AspectRatioMaster;
using BAL.Interfaces.CategoryMaster;
using BAL.Interfaces.Media;
using BAL.Interfaces.MenuMaster;
using BAL.Interfaces.RequiredFieldsMaster;
using BAL.Operations.AlbumMaster;
using BAL.Operations.AspectRatioMaster;
using BAL.Operations.CategoryMaster;
using BAL.Operations.Media;
using BAL.Operations.MenuMaster;
using BAL.Operations.Post;
using BAL.Operations.RequiredFieldsMaster;
using BAL.Services;
using BAL.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BAL.DependencyResolver
{
    public static class DIResolver
    {
        public static IServiceCollection DIBALResolver(this IServiceCollection services)
        {
            services.AddHostedService<JwtRefreshTokenCache>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<bIPost, bPost>();
            services.AddScoped<bICategoryMaster, bCategoryMasterRepo>();
            services.AddScoped<bIMenuMaster, bMenuMasterRepo>();
            services.AddScoped<bIAlbumMaster, bAlbumMaster>();
            services.AddScoped<bIAspectRatioMaster, bAspectRatioMaster>();
            services.AddScoped<bIRequiredFieldsMaster, bRequiredFieldsMaster>();
            services.AddScoped<bIMedia, bMedia>();
            return services;
        }
    }
}
