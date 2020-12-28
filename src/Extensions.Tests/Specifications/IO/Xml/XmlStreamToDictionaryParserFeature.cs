﻿using System;
using Xbehave;
using Xunit.Abstractions;

namespace Specifications.IO.Xml
{
    public class XmlStreamToDictionaryParserFeature : StreamToDictionaryParserFeatureBase
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

        public XmlStreamToDictionaryParserFeature(ITestOutputHelper output) : base(output)
        {
        }

        [Scenario(DisplayName = "Parse a valid xml stream to values and hierarchical keys with given delimiter")]
        [Example(".")]
        [Example(":")]
        public void ParseValidXmlStream(string keyDelimiter)
        {
            $"Given a valid xml stream"
                .x(() => ArrangeStream(_xml));

            $"And a XmlStreamToDictionaryParser instance"
                .x(() =>
                {
                    _sut = new MS.Extensions.IO.Xml.XmlStreamToDictionaryParser(NewParserOptions(keyDelimiter));
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
