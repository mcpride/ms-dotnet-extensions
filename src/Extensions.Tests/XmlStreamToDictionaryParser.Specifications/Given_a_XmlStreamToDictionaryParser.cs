using MS.Extensions.IO;

// ReSharper disable InconsistentNaming

namespace XmlStreamToDictionaryParser.Specifications
{
    public abstract partial class Given_a_XmlStreamToDictionaryParser
    {
        protected IStreamToDictionaryParser _sut;
        protected virtual void Arrange()
        {
            _sut = new MS.Extensions.IO.Xml.XmlStreamToDictionaryParser();
        }
    }
}