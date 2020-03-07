using System;
using System.Diagnostics.CodeAnalysis;
using Amaury.V2.Persistence;
using Amazon.DynamoDBv2;
using Microsoft.Extensions.DependencyInjection;
using ICelebrityEventStore = Amaury.Abstractions.Persistence.ICelebrityEventStore;

namespace Amaury.Store.DynamoDb.V2.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class EventStoreServiceExtensions
    {
        public static void AddEventStore(this IServiceCollection services, Action<DynamoDb.Configurations.EventStoreOptions> configure = null)
        {
            var options = new DynamoDb.Configurations.EventStoreOptions();
            configure?.Invoke(options);

            services.AddSingleton(options);
            services.AddAWSService<IAmazonDynamoDB>(options);
            services.AddSingleton<ICelebrityEventStoreConfiguration, DynamoDbEventStoreConfiguration>();
            services.AddAsyncInitializer<DynamoDbEventStoreInitializer>();
            services.AddSingleton<ICelebrityEventStore, DynamoDbEventStore>();
        }

        public static void AddEventStore(this IServiceCollection services, IAmazonDynamoDB client, Action<DynamoDb.Configurations.EventStoreOptions> configure = null)
        {
            var options = new DynamoDb.Configurations.EventStoreOptions();
            configure?.Invoke(options);

            services.AddSingleton(options);
            services.AddSingleton<IAmazonDynamoDB>(client);
            services.AddSingleton<ICelebrityEventStoreConfiguration, DynamoDbEventStoreConfiguration>();
            services.AddAsyncInitializer<DynamoDbEventStoreInitializer>();
            services.AddSingleton<ICelebrityEventStore, DynamoDbEventStore>();
        }
    }
}