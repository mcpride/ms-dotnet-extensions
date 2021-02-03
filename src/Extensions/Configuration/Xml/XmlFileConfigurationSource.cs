using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace MS.Extensions.Configuration.Xml
{
    /// <summary>
    /// Represents a XML file as an <see cref="IConfigurationSource"/>.
    /// </summary>
    public class XmlFileConfigurationSource : FileConfigurationSource
    {
        public IEnumerable<string> Parents { get; set; }

        public Func<string, IEnumerable<string>, bool> IsIdentifier { get; set; } = (name, _) =>
            string.Equals("name", name, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Builds the <see cref="XmlFileConfigurationProvider"/> for this source.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>A <see cref="XmlFileConfigurationProvider"/></returns>
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new XmlFileConfigurationProvider(this);
        }
    }
}
