using MS.Extensions.IO;

// ReSharper disable InconsistentNaming

namespace JsonStreamToDictionaryParser.Specifications
{
    public abstract partial class Given_a_JsonStreamToDictionaryParser
    {
        protected IStreamToDictionaryParser _sut;
        protected virtual void Arrange(ParseToDictionaryOptions options)
        {
            _sut = new MS.Extensions.IO.Json.JsonStreamToDictionaryParser(options);
        }
    }
}