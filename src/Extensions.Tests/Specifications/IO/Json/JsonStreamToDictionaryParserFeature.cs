using System;
using Xbehave;
using Xunit.Abstractions;

namespace Specifications.IO.Json
{
    public class JsonStreamToDictionaryParserFeature : StreamToDictionaryParserFeatureBase
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

        public JsonStreamToDictionaryParserFeature(ITestOutputHelper output) : base(output)
        {
        }

        [Scenario(DisplayName = "Parse a valid xml stream")]
        [Example(".")]
        [Example(":")]
        public void ParseValidXmlStream(string keyDelimiter)
        {
            $"Given a valid json stream"
                .x(() => Arrange(_json));

            $"And a JsonStreamToDictionaryParser instance"
                .x(() =>
                {
                    _sut = new MS.Extensions.IO.Json.JsonStreamToDictionaryParser(NewParserOptions(keyDelimiter));
                });

            $"When the parser parses the stream"
                .x(Act);

            $"Then the resulting dictionary should contain all key value pairs"
                .x(Assert_resulting_dictionary_should_contain_all_key_value_pairs);

            $"And the resulting dictionary should contain the configured indexes"
                .x(Assert_resulting_dictionary_should_contain_the_configured_indexes);

            $"And all keys of the resulting dictionary should start with the configured parents"
                .x(Assert_all_keys_of_the_resulting_dictionary_should_start_with_the_configured_parents);
        }
    }
}
