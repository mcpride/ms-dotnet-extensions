using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using MS.Extensions.IO.Xml;

namespace MS.Extensions.Configuration.Xml
{
    /// <summary>
    /// A XML file based <see cref="FileConfigurationProvider"/>.
    /// </summary>
    public class XmlFileConfigurationProvider : FileConfigurationProvider
    {
        private readonly XmlFileConfigurationSource _source;

        /// <summary>
        /// Initializes a new instance with the specified source.
        /// </summary>
        /// <param name="source">The source settings.</param>
        public XmlFileConfigurationProvider(XmlFileConfigurationSource source) : base(source)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
        }

        /// <summary>
        /// Loads the XML data from a stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        public override void Load(Stream stream)
        {
            try
            {
                Data = XmlStreamToDictionaryParser.ParseStream(
                        stream, 
                        new IO.ParseToDictionaryOptions
                        {
                            KeyDelimiter = ConfigurationPath.KeyDelimiter,
                            Parents = _source.Parents,
                            IsIdentifier = _source.IsIdentifier
                        }
                    );
            }
            catch (Exception e)
            {
                throw new FormatException("Could not parse the XML file.", e);
            }
        }
    }
}
