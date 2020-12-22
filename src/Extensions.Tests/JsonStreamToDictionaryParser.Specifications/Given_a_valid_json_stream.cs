using MS.Extensions.IO;
using System;
using System.IO;
using System.Text;

// ReSharper disable InconsistentNaming
namespace JsonStreamToDictionaryParser.Specifications
{
    public abstract partial class Given_a_JsonStreamToDictionaryParser
    {
        public abstract partial class Given_a_valid_json_stream : Given_a_JsonStreamToDictionaryParser, IDisposable
        {
            private readonly string _json =
                @"{ ""enableDynamicOverrides"":""false""," + Environment.NewLine +
                @"  ""Endpoints"":" + Environment.NewLine +
                @"    [" + Environment.NewLine +
                @"      { " + Environment.NewLine +
                @"        ""id"":""WebStatelessEndpoint""," + Environment.NewLine +
                @"        ""enable"":""true""," + Environment.NewLine +
                @"        ""Routes"":" + Environment.NewLine +
                @"          [" + Environment.NewLine +
                @"            { " + Environment.NewLine +
                @"              ""routeId"":""RouteApi""," + Environment.NewLine +
                @"              ""order"":""1""," + Environment.NewLine +
                @"              ""Match"":" + Environment.NewLine +
                @"                {" + Environment.NewLine +
                @"                  ""path"":""/WebStateless/api/{**catch-all}""" + Environment.NewLine +
                @"                }" + Environment.NewLine +
                @"            }," + Environment.NewLine +
                @"            { " + Environment.NewLine +
                @"              ""routeId"":""RouteUi""," + Environment.NewLine +
                @"              ""order"":""2""," + Environment.NewLine +
                @"              ""Match"":" + Environment.NewLine +
                @"                {" + Environment.NewLine +
                @"                 ""path"":""/WebStateless/ui/{**catch-all}""" + Environment.NewLine +
                @"                }" + Environment.NewLine +
                @"            }" + Environment.NewLine +
                @"          ]" + Environment.NewLine +
                @"      }" + Environment.NewLine +
                @"    ]" + Environment.NewLine +
                @"}";

            protected string _keyDelimiter = ".";

            protected Stream _stream;

            protected override void Arrange(ParseToDictionaryOptions options)
            {
                base.Arrange(options);
                _stream = new MemoryStream(Encoding.ASCII.GetBytes(_json));
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