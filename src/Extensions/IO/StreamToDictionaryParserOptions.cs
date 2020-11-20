using System;
using System.Collections.Generic;

namespace MS.Extensions.IO
{
    public class StreamToDictionaryParserOptions
    {
        public IEnumerable<string> Parents { get; set; }
        public string KeyDelimiter { get; set; } = ".";

        public Func<string, IEnumerable<string>, bool> IsIdentifier { get; set; } = (name, _) =>
            string.Equals("name", name, StringComparison.OrdinalIgnoreCase);
    }
}