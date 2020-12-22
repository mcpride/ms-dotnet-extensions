using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.Json;
using MS.Extensions.IO.Json;

namespace MS.Extensions.Configuration.Json
{
    /// <summary>
    /// A JSON file based <see cref="FileConfigurationProvider"/>.
    /// </summary>
    public class JsonFileConfigurationProvider : FileConfigurationProvider
    {
        private readonly JsonFileConfigurationSource _source;

        /// <summary>
        /// Initializes a new instance with the specified source.
        /// </summary>
        /// <param name="source">The source settings.</param>
        public JsonFileConfigurationProvider(JsonFileConfigurationSource source) : base(source)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
        }

        /// <summary>
        /// Loads the JSON data from a stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        public override void Load(Stream stream)
        {
            try
            {
                Data = JsonStreamToDictionaryParser.ParseStream(
                        stream, 
                        new IO.ParseToDictionaryOptions
                        {
                            KeyDelimiter = ConfigurationPath.KeyDelimiter,
                            Parents = _source.Parents,
                            IsIdentifier = _source.IsIdentifier
                        }
                    );
            }
            catch (JsonException e)
            {
                throw new FormatException("Could not parse the JSON file.", e);
            }
        }
    }
}
