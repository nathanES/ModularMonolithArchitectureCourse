﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace RiverBooks.Users;

public static class UserModuleExtensions
{
    public static IServiceCollection AddUsersModuleServices(
        this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger)
    {
        string? connectionString = config.GetConnectionString("UsersConnectionString");
        services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(connectionString));

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<UsersDbContext>();
        
        logger.Information("{Module} module services registered", "Users");

        return services;
    }
}