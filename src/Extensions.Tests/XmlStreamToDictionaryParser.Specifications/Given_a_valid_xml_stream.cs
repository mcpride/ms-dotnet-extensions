using MS.Extensions.IO;
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
            private readonly string _xml =
                @"<?xml version=""1.0"" encoding=""utf-8""?>" + Environment.NewLine +
                @"<Service enableDynamicOverrides=""false"">" + Environment.NewLine +
                @"  <Endpoints>" + Environment.NewLine +
                @"    <Endpoint id=""WebStatelessEndpoint"" enable=""true"">" + Environment.NewLine +
                @"      <Routes>" + Environment.NewLine +
                @"        <Route routeId=""RouteApi"" order=""1"">" + Environment.NewLine +
                @"          <Match path=""/WebStateless/api/{**catch-all}"" />" + Environment.NewLine +
                @"        </Route>" + Environment.NewLine +
                @"        <Route routeId=""RouteUi"" order=""1"">" + Environment.NewLine +
                @"          <Match path=""/WebStateless/ui/{**catch-all}"" />" + Environment.NewLine +
                @"        </Route>" + Environment.NewLine +
                @"      </Routes>" + Environment.NewLine +
                @"    </Endpoint>" + Environment.NewLine +
                @"  </Endpoints>" + Environment.NewLine +
                @"</Service>";

            protected string _keyDelimiter = ".";

            protected Stream _stream;

            protected override void Arrange(ParseToDictionaryOptions options)
            {
                base.Arrange(options);
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