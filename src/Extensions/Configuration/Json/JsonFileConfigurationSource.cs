using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace MS.Extensions.Configuration.Json
{
    /// <summary>
    /// Represents a JSON file as an <see cref="IConfigurationSource"/>.
    /// </summary>
    public class JsonFileConfigurationSource : FileConfigurationSource
    {
        public IEnumerable<string> Parents { get; set; }

        public Func<string, IEnumerable<string>, bool> IsIdentifier { get; set; } = (name, _) =>
            string.Equals("name", name, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Builds the <see cref="JsonFileConfigurationProvider"/> for this source.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>A <see cref="JsonFileConfigurationProvider"/></returns>
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new JsonFileConfigurationProvider(this);
        }
    }
}
