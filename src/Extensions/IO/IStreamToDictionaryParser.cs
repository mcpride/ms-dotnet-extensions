using System;
using System.Collections.Generic;
using System.IO;

namespace MS.Extensions.IO
{
    public interface IStreamToDictionaryParser : IStreamParser<IDictionary<string, string>>
    {
    }
}