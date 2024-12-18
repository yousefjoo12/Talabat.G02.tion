﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Erorrs;
using Talabat.APIS.Helpers;
using Talabat.Core;
using Talabat.Core.Reposeitories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository;
using Talabat.Service;

namespace Talabat.APIS.Extensions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IOrderService), typeof(OrderService));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfiles));
            
            services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                          .SelectMany(p => p.Value.Errors)
                                                          .Select(E => E.ErrorMessage)
                                                          .ToList();
                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });

            services.AddScoped(typeof(IBasketReposeitory), typeof(BasketReposeitory));

            services.AddScoped(typeof(IAuthService), typeof(AuthService));


            return services;
        }
    }

    //public static WebApplications UseSwaggerMiddleWare(this WebApplications app)
    //{

    //}

}
