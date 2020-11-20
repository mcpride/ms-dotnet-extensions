using System;
using System.IO;
using System.Text;

// ReSharper disable InconsistentNaming
namespace XmlStreamToDictionaryParser.Specifications
{
    public abstract partial class Given_a_XmlStreamToDictionaryParser
    {
        public abstract partial class Given_a_valid_xml_stream : Given_a_XmlStreamToDictionaryParser, IDisposable
        {
            private const string _xml =
                @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                @"<Service enableDynamicOverrides=""false"">" +
                @"  <Endpoints>" +
                @"    <Endpoint id=""WebStatelessEndpoint"" enable=""true"">" +
                @"      <Routes>" +
                @"        <Route routeId=""RouteApi"" order=""1"">" +
                @"          <Match path=""/WebStateless/api/{**catch-all}"" />" +
                @"        </Route>" +
                @"        <Route routeId=""RouteUi"" order=""1"">" +
                @"          <Match path=""/WebStateless/ui/{**catch-all}"" />" +
                @"        </Route>" +
                @"      </Routes>" +
                @"    </Endpoint>" +
                @"  </Endpoints>" +
                @"</Service>";

            protected string _keyDelimiter = ".";

            protected Stream _stream;

            protected override void Arrange()
            {
                base.Arrange();
                _stream = new MemoryStream(Encoding.ASCII.GetBytes(_xml));
            }

            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    var stream = _stream;
                    _stream = null;
                    stream?.Dispose();
                }
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }
    }
}