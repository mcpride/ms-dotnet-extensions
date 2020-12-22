using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using MS.Extensions.Text;

namespace MS.Extensions.IO.Json
{
    public class JsonStreamToDictionaryParser : IStreamToDictionaryParser
    {
        private readonly ParseToDictionaryOptions _options;

        /// <summary>
        /// Creates a new instance of <c>JsonStreamToDictionaryParser</c>.
        /// </summary>
        /// <param name="options">The parser options.</param>
        public JsonStreamToDictionaryParser(ParseToDictionaryOptions options = null)
        {
            _options = options;
        }

        /// <summary>
        /// Loads the JSON data from a stream.
        /// </summary>
        /// <param name="input">The JSON stream to read.</param>
        public IDictionary<string, string> Parse(Stream input)
        {
            return ParseStream(input, _options);
        }

        /// <summary>
        /// Loads the JSON data from a stream.
        /// </summary>
        /// <param name="stream">The JSON stream to read.</param>
        /// <param name="options">The parser options.</param>
        public static IDictionary<string, string> ParseStream(Stream stream, ParseToDictionaryOptions options = null)
        {
            var parserOptions = options ?? new ParseToDictionaryOptions();

            var data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var prefixStack = new Stack<string>();

            var jsonDocumentOptions = new JsonDocumentOptions
            {
                CommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
            };

            if (parserOptions.Parents != null)
            {
                foreach (var parent in parserOptions.Parents)
                {
                    prefixStack.Push(parent);
                }
            }

            using (var reader = new StreamReader(stream))
            {
                using (var doc = JsonDocument.Parse(reader.ReadToEnd(), jsonDocumentOptions))
                {
                    if (doc.RootElement.ValueKind != JsonValueKind.Object)
                    {
                        throw new FormatException($"Top-level JSON element must be an object. Instead, '{doc.RootElement.ValueKind}' was found.");
                    }
                    VisitElement(doc.RootElement, prefixStack, data, parserOptions);
                }
            }

            return data;
        }

        private static void VisitElement(JsonElement element, Stack<string> prefixStack, IDictionary<string, string> data, ParseToDictionaryOptions parserOptions)
        {
            var isEmpty = true;

            foreach (var property in element.EnumerateObject())
            {
                isEmpty = false;
                prefixStack.Push(property.Name.Capitalize());
                try
                {
                    VisitValue(property.Value, prefixStack, data, parserOptions);
                }
                finally
                {
                    prefixStack.Pop();
                }
            }

            if (isEmpty && prefixStack.Count > 0)
            {
                var key = string.Join(parserOptions.KeyDelimiter, prefixStack.Reverse());
                data[key] = null;
            }
        }

        private static bool HasIdentifier(JsonElement element, Stack<string> prefixStack, ParseToDictionaryOptions parserOptions, out string identifier)
        {
            identifier = null;
            foreach (var property in element.EnumerateObject())
            {
                switch (property.Value.ValueKind)
                {
                    case JsonValueKind.Number:
                    case JsonValueKind.String:
                        identifier = property.Value.GetString();
                        if (!string.IsNullOrEmpty(identifier) && parserOptions.IsIdentifier(property.Name, prefixStack))
                        {
                            return true;
                        }
                        break;
                    default:
                        continue;
                }

            }
            return false;
        }


        private static void VisitValue(JsonElement value, Stack<string> prefixStack, IDictionary<string, string> data, ParseToDictionaryOptions parserOptions)
        {
            Debug.Assert(prefixStack.Count > 0);

            switch (value.ValueKind)
            {
                case JsonValueKind.Object:
                    VisitElement(value, prefixStack, data, parserOptions);
                    break;

                case JsonValueKind.Array:
                    var index = 0;
                    foreach (var arrayElement in value.EnumerateArray())
                    {
                        if (HasIdentifier(arrayElement, prefixStack, parserOptions, out var identifier))
                        {
                            prefixStack.Push(identifier.Capitalize());
                        }
                        else
                        {
                            prefixStack.Push(index.ToString());
                            index++;
                        }
                        try
                        {
                            VisitValue(arrayElement, prefixStack, data, parserOptions);
                        }
                        finally
                        {
                            prefixStack.Pop();
                        }
                    }
                    break;

                case JsonValueKind.Number:
                case JsonValueKind.String:
                case JsonValueKind.True:
                case JsonValueKind.False:
                case JsonValueKind.Null:
                    var key = string.Join(parserOptions.KeyDelimiter, prefixStack.Reverse());
                    if (data.ContainsKey(key))
                    {
                        throw new FormatException($"A duplicate key '{key}' was found.");
                    }
                    data[key] = value.ToString();
                    break;

                default:
                    throw new FormatException($"Unsupported JSON token '{value.ValueKind}' was found.");
            }
        }
    }
}
