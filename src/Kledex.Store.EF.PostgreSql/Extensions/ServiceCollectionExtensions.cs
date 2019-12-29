﻿using System;
using Kledex.Extensions;
using Kledex.Store.EF.Configuration;
using Kledex.Store.EF.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Kledex.Store.EF.PostgreSql.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IKledexServiceBuilder AddPostgreSqlStore(this IKledexServiceBuilder builder)
        {
            return AddPostgreSqlStore(builder, opt => { });
        }

        public static IKledexServiceBuilder AddPostgreSqlStore(this IKledexServiceBuilder builder, Action<DatabaseOptions> configureOptions)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));

            builder.Services.Configure(configureOptions);

            var sp = builder.Services.BuildServiceProvider();
            var dbOptions = sp.GetService<IOptions<DatabaseOptions>>().Value;

            builder.AddEFStore();

            builder.Services.AddDbContext<DomainDbContext>(options =>
                options.UseNpgsql(dbOptions.ConnectionString));

            builder.Services.AddTransient<IDatabaseProvider, PostgreSqlDatabaseProvider>();

            return builder;
        }
    }
}
