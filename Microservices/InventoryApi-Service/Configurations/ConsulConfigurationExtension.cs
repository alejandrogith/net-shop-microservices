using Consul;

namespace InventoryApi_Service.Configurations
{
    public static class ConsulConfigurationExtension
    {
        public static void AddConsulServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var consulClient = configuration.GetValue<Uri>("Consul:ServiceDiscoveryAddress");

            services.AddSingleton<ConsulServiceConfig>();
            services.AddSingleton<IHostedService, ServiceDiscoveryHostedService>();
            services.AddSingleton<IConsulClient, ConsulClient>(provider =>
              new ConsulClient(config => config.Address = consulClient));
        }
    }



    public class ConsulServiceConfig
    {


        private readonly IConfiguration _configuration;

        public ConsulServiceConfig(IConfiguration configuration)
        {
            _configuration = configuration;

            ServiceDiscoveryAddress = configuration.GetValue<Uri>("Consul:ServiceDiscoveryAddress");
            ServiceAddress = configuration.GetValue<Uri>("Consul:ServiceAddress");
            ServiceName = configuration.GetValue<string>("Consul:ServiceName");
            ServiceId = configuration.GetValue<string>("Consul:ServiceId");
        }

        public Uri ServiceDiscoveryAddress { get; }
        public Uri ServiceAddress { get; }
        public string ServiceName { get; }
        public string ServiceId { get; }
    }
}


