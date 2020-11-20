using System;
using System.Collections.Generic;
using System.IO;

namespace MS.Extensions.IO
{
    public interface IStreamToDictionaryParser
    {
        /// <summary>
        /// Loads the XML data from a stream.
        /// </summary>
        /// <param name="stream">The xml stream to read.</param>
        /// <param name="options">The parser options.</param>
        IDictionary<string, string> Parse(Stream stream, Action<StreamToDictionaryParserOptions> options = null);
    }
}