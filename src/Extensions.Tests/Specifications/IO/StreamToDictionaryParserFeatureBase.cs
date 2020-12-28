using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MS.Extensions.IO;
using Xunit;
using Xunit.Abstractions;

namespace Specifications.IO
{
    public abstract class StreamToDictionaryParserFeatureBase : IDisposable
    {
        private readonly ITestOutputHelper _output;

        protected string _keyDelimiter;

        protected Stream _stream;

        protected IDictionary<string, string> _actual;

        protected IStreamToDictionaryParser _sut;


        protected StreamToDictionaryParserFeatureBase(ITestOutputHelper output)
        {
            _output = output;
        }

        protected virtual void Arrange(string text)
        {
            _stream = new MemoryStream(Encoding.ASCII.GetBytes(text));
        }

        protected virtual void Act()
        {
            _actual = _sut.Parse(_stream);
            foreach (var pair in _actual)
            {
                _output.WriteLine($"{pair.Key} = {pair.Value}");
            }
        }

        protected void Assert_resulting_dictionary_should_contain_all_key_value_pairs()
        {
            Assert.Equal(9, _actual.Count);
        }

        protected void Assert_resulting_dictionary_should_contain_the_configured_indexes()
        {
            Assert.Collection(_actual,
                pair => { },
                pair =>
                {
                    Assert.Contains($"{_keyDelimiter}Endpoints{_keyDelimiter}WebStatelessEndpoint{_keyDelimiter}",
                        pair.Key);
                    Assert.Equal("true", pair.Value);
                },
                pair =>
                {
                    Assert.Contains($"{_keyDelimiter}Endpoints{_keyDelimiter}WebStatelessEndpoint{_keyDelimiter}",
                        pair.Key);
                    Assert.Equal("WebStatelessEndpoint", pair.Value);
                },
                pair => Assert.Contains(
                    $"{_keyDelimiter}Endpoints{_keyDelimiter}WebStatelessEndpoint{_keyDelimiter}Routes{_keyDelimiter}RouteApi{_keyDelimiter}",
                    pair.Key),
                pair => Assert.Contains(
                    $"{_keyDelimiter}Endpoints{_keyDelimiter}WebStatelessEndpoint{_keyDelimiter}Routes{_keyDelimiter}RouteApi{_keyDelimiter}",
                    pair.Key),
                pair => Assert.Contains(
                    $"{_keyDelimiter}Endpoints{_keyDelimiter}WebStatelessEndpoint{_keyDelimiter}Routes{_keyDelimiter}RouteApi{_keyDelimiter}",
                    pair.Key),
                pair => Assert.Contains(
                    $"{_keyDelimiter}Endpoints{_keyDelimiter}WebStatelessEndpoint{_keyDelimiter}Routes{_keyDelimiter}RouteUi{_keyDelimiter}",
                    pair.Key),
                pair => Assert.Contains(
                    $"{_keyDelimiter}Endpoints{_keyDelimiter}WebStatelessEndpoint{_keyDelimiter}Routes{_keyDelimiter}RouteUi{_keyDelimiter}",
                    pair.Key),
                pair => Assert.Contains(
                    $"{_keyDelimiter}Endpoints{_keyDelimiter}WebStatelessEndpoint{_keyDelimiter}Routes{_keyDelimiter}RouteUi{_keyDelimiter}",
                    pair.Key)
            );
        }

        protected void Assert_all_keys_of_the_resulting_dictionary_should_start_with_the_configured_parents()
        {
            Assert.All(_actual, pair => Assert.StartsWith($"MyApp{_keyDelimiter}MyService{_keyDelimiter}", pair.Key));
        }


        protected ParseToDictionaryOptions NewParserOptions(string keyDelimiter = ".")
        {
            _keyDelimiter = keyDelimiter;
            return new ParseToDictionaryOptions
            {
                KeyDelimiter = _keyDelimiter,
                Parents = new List<string> {"MyApp", "MyService"},
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
