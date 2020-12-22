using MS.Extensions.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable InconsistentNaming
namespace JsonStreamToDictionaryParser.Specifications
{
    public abstract partial class Given_a_JsonStreamToDictionaryParser
    {
        public abstract partial class Given_a_valid_json_stream
        {
            public class When_parser_parses_the_valid_json_stream : Given_a_valid_json_stream
            {
                private readonly ITestOutputHelper _output;
                private IDictionary<string, string> _actual;

                public When_parser_parses_the_valid_json_stream(ITestOutputHelper output)
                {
                    _output = output;
                }

                [Theory]
                [InlineData(".")]
                [InlineData(":")]
                public void Then_all_keys_of_the_resulting_dictionary_should_start_with_the_configured_parents(string keyDelimiter)
                {
                    _keyDelimiter = keyDelimiter;
                    var options = NewParserOptions();
                    options.KeyDelimiter = _keyDelimiter;

                    Arrange(options);
                    Act();
                    Assert.All(_actual, pair => Assert.StartsWith($"MyApp{_keyDelimiter}MyService{_keyDelimiter}", pair.Key));
                }

                [Fact]
                public void Then_the_resulting_dictionary_should_contain_all_key_value_pairs()
                {
                    Arrange(NewParserOptions());
                    Act();
                    Assert.Equal(9, _actual.Count);
                }

                [Theory]
                [InlineData(".")]
                [InlineData(":")]
                public void Then_the_resulting_dictionary_should_contain_the_configured_indexes(string keyDelimiter)
                {
                    _keyDelimiter = keyDelimiter;
                    var options = NewParserOptions();
                    options.KeyDelimiter = _keyDelimiter;

                    Arrange(options);
                    Act();
                    Assert.Collection(_actual, 
                        pair => {},
                        pair =>
                        {
                            Assert.Contains($"{_keyDelimiter}Endpoints{_keyDelimiter}WebStatelessEndpoint{_keyDelimiter}", pair.Key);
                            Assert.Equal("true", pair.Value);
                        },
                        pair =>
                        {
                            Assert.Contains($"{_keyDelimiter}Endpoints{_keyDelimiter}WebStatelessEndpoint{_keyDelimiter}", pair.Key);
                            Assert.Equal("WebStatelessEndpoint", pair.Value);
                        },
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
                    _actual = _sut.Parse(_stream);
                    foreach (var pair in _actual)
                    {
                        _output.WriteLine($"{pair.Key} = {pair.Value}");
                    }
                }
                private static ParseToDictionaryOptions NewParserOptions()
                {
                    return new ParseToDictionaryOptions
                    {
                        Parents = new List<string> { "MyApp", "MyService" },
                        IsIdentifier = (attribute, stack) =>
                        {
                            return stack.FirstOrDefault() switch
                            {
                                "Endpoints" => (string.Equals(attribute, "Id", StringComparison.OrdinalIgnoreCase)),
                                "Routes" => (string.Equals(attribute, "RouteId", StringComparison.OrdinalIgnoreCase)),
                                _ => false
                            };
                        }
                    };
                }
            }
        }
    }
}
