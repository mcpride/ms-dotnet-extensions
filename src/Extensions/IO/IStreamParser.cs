using System.IO;

namespace MS.Extensions.IO
{
    public interface IStreamParser<TOut> : IParser<Stream, TOut>
    {
    }
}