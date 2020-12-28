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

        [Scenario(DisplayName = "Parse a valid json stream to values and hierarchical keys with given delimiter")]
        [Example(".")]
        [Example(":")]
        public void ParseValidJsonStream(string keyDelimiter)
        {
            $"Given a valid json stream"
                .x(() => ArrangeStream(_json));

            $"And a JsonStreamToDictionaryParser instance"
                .x(() =>
                {
                    _sut = new MS.Extensions.IO.Json.JsonStreamToDictionaryParser(NewParserOptions(keyDelimiter));
                });

            $"When the parser parses the stream"
                .x(Act);

            $"Then the resulting dictionary should contain all key value pairs"
                .x(AssertResultShouldContainAllKVPairs);

            $"And the resulting dictionary should contain the configured indexes"
                .x(AssertResultingShouldContainConfiguredIndexes);

            $"And all keys of the resulting dictionary should start with the configured parents"
                .x(AssertKeysShouldStartWithConfiguredParents);
        }
    }
}
