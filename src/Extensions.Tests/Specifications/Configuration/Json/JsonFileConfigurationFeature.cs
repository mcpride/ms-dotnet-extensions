using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MS.Extensions.Configuration.Json;
using Xbehave;
using Xunit;

namespace Specifications.Configuration.Json
{
    public class JsonFileConfigurationFeature
    {
        [Scenario(DisplayName = "Read configuration from valid json file")]
        public void ReadConfigurationFromValidJsonFile(
            IConfigurationBuilder configBuilder = null, 
            JsonFileConfigurationSource configurationSource = null,
            IConfigurationRoot configuration = null,
            JsonFileConfigurationProvider configProvider = null,
            IConfigurationSection configSection = null)
        {
            "Given a new configuration builder"
                .x(() =>
                {
                    configBuilder = new ConfigurationBuilder();
                });
            $"And the host builder gets a {nameof(JsonFileConfigurationSource)} added"
                .x(() =>
                {
                    configBuilder.Add<JsonFileConfigurationSource>(source =>
                    {
                        configurationSource = source;
                    });
                });
            $"And the {nameof(JsonFileConfigurationSource)} targets to a valid json file"
                .x(() =>
                {
                    configurationSource.Path = @"Specifications\Configuration\Json\JsonFileConfigurationFeature.json";
                });
            $"And the identifier callback of the {nameof(JsonFileConfigurationSource)} is configured"
                .x(() =>
                {
                    configurationSource.IsIdentifier = (name, prefixes) =>
                    {
                        return prefixes.FirstOrDefault() switch
                        {
                            "Endpoints" => (string.Equals(name, "Id", StringComparison.OrdinalIgnoreCase)),
                            "Routes" => (string.Equals(name, "RouteId", StringComparison.OrdinalIgnoreCase)),
                            _ => false
                        };
                    };
                });
            $"And the parent sections of the {nameof(JsonFileConfigurationSource)} are configured"
                .x(() =>
                {
                    configurationSource.Parents = new List<string> {"MyApp", "MyService"};
                });
            "When the configuration builder builds the configuration root"
                .x(() =>
                {
                    configuration = configBuilder.Build();
                    var children = configuration.GetChildren();
                });

            "Then the configuration root contains 1 configuration provider"
                .x(() =>
                {
                    Assert.Equal(1, configuration.Providers.Count());
                    configProvider = (JsonFileConfigurationProvider)configuration.Providers.FirstOrDefault();
                    Assert.NotNull(configProvider);
                });
            "And the configuration provider data contains 9 entries"
                .x(() =>
                {
                    var childKeys = configProvider.GetChildKeys(new List<string>(), null);
                    Assert.Equal(9, childKeys.Count());
                });
            "And the configuration root contains 1 section with the name of the first configured parent"
                .x(() =>
                {
                    var children = configuration.GetChildren();
                    Assert.Equal(1, children.Count());
                    configSection = children.First();
                    Assert.Equal("MyApp", configSection.Key);
                });
        }
    }
}
