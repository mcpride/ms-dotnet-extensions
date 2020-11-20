using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable InconsistentNaming
namespace XmlStreamToDictionaryParser.Specifications
{
    public abstract partial class Given_a_XmlStreamToDictionaryParser
    {
        public abstract partial class Given_a_valid_xml_stream
        {
            public class When_parser_parses_the_valid_xml_stream : Given_a_valid_xml_stream
            {
                private readonly ITestOutputHelper _output;
                private IDictionary<string, string> _actual;

                public When_parser_parses_the_valid_xml_stream(ITestOutputHelper output)
                {
                    _output = output;
                }

                [Theory]
                [InlineData(".")]
                [InlineData(":")]
                public void Then_all_keys_of_the_resulting_dictionary_should_start_with_the_configured_parents(string keyDelimiter)
                {
                    _keyDelimiter = keyDelimiter;
                    Arrange();
                    Act();
                    Assert.All(_actual, pair => Assert.StartsWith($"MyApp{_keyDelimiter}MyService{_keyDelimiter}", pair.Key));
                }

                [Fact]
                public void Then_the_resulting_dictionary_should_contain_all_key_value_pairs()
                {
                    Arrange();
                    Act();
                    Assert.Equal(9, _actual.Count);
                }

                [Theory]
                [InlineData(".")]
                [InlineData(":")]
                public void Then_the_resulting_dictionary_should_contain_the_configured_indexes(string keyDelimiter)
                {
                    _keyDelimiter = keyDelimiter;
                    Arrange();
                    Act();
                    Assert.Collection(_actual, 
                        pair => {},
                        pair =>
                        {
                            Assert.Contains($"{_keyDelimiter}Endpoints{_keyDelimiter}WebStatelessEndpoint{_keyDelimiter}", pair.Key);
                            Assert.Equal("WebStatelessEndpoint", pair.Value);
                        },
                        pair => Assert.Contains($"{_keyDelimiter}Endpoints{_keyDelimiter}WebStatelessEndpoint{_keyDelimiter}", pair.Key),
                        pair => Assert.Contains($"{_keyDelimiter}Endpoints{_keyDelimiter}WebStatelessEndpoint{_keyDelimiter}Routes{_keyDelimiter}RouteApi{_keyDelimiter}", pair.Key),
                        pair => Assert.Contains($"{_keyDelimiter}Endpoints{_keyDelimiter}WebStatelessEndpoint{_keyDelimiter}Routes{_keyDelimiter}RouteApi{_keyDelimiter}", pair.Key),
                        pair => Assert.Contains($"{_keyDelimiter}Endpoints{_keyDelimiter}WebStatelessEndpoint{_keyDelimiter}Routes{_keyDelimiter}RouteApi{_keyDelimiter}", pair.Key),
                        pair => Assert.Contains($"{_keyDelimiter}Endpoints{_keyDelimiter}WebStatelessEndpoint{_keyDelimiter}Routes{_keyDelimiter}RouteUi{_keyDelimiter}", pair.Key),
                        pair => Assert.Contains($"{_keyDelimiter}Endpoints{_keyDelimiter}WebStatelessEndpoint{_keyDelimiter}Routes{_keyDelimiter}RouteUi{_keyDelimiter}", pair.Key),
                        pair => Assert.Contains($"{_keyDelimiter}Endpoints{_keyDelimiter}WebStatelessEndpoint{_keyDelimiter}Routes{_keyDelimiter}RouteUi{_keyDelimiter}", pair.Key)
                    );
                }

                private void Act()
                {
                    _actual = _sut.Parse(_stream, options =>
                    {
                        options.KeyDelimiter = _keyDelimiter;
                        options.Parents = new List<string> {"MyApp", "MyService"};
                        options.IsIdentifier = (attribute, stack) =>
                        {
                            return stack.FirstOrDefault() switch
                            {
                                "Endpoint" => (string.Equals(attribute, "Id", StringComparison.OrdinalIgnoreCase)),
                                "Route" => (string.Equals(attribute, "RouteId", StringComparison.OrdinalIgnoreCase)),
                                _ => false
                            };
                        };
                    });
                    foreach (var pair in _actual)
                    {
                        _output.WriteLine($"{pair.Key} = {pair.Value}");
                    }
                }
            }
        }
    }
}
