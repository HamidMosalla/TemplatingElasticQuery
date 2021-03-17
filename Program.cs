using Elasticsearch.Net;
using Nest;

namespace TemplatingPrototype
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    class Program
    {
        public static void TemplatingPrototypeSimple(IEnumerable<string> terms, string fielddName, string sortField)
        {
            var query1 = File.ReadAllText(@"C:\Users\Hamid\source\repos\TemplatingPrototype\Query1.txt");

            var termsString = string.Join(',', terms);

            query1 = query1.Replace("#Terms", termsString)
                .Replace("#FieldName", fielddName)
                .Replace("#SortField", sortField);
        }

        public static void TemplatingPrototypeWithRegx(IEnumerable<string> terms, string fielddName, string sortField)
        {
            var query2 = File.ReadAllText(@"C:\Users\Hamid\source\repos\TemplatingPrototype\Query2.txt");

            var termsString = string.Join(',', terms);

            var variableValues = new Dictionary<string, object>
            {
                ["Terms"] = termsString,
                ["FieldName"] = fielddName,
                ["SortField"] = sortField,
            };

            var renderResult = Template.Render(query2, variableValues);
        }

        public static void TemplatingPrototypeWithNest()
        {
            var client = new ElasticClient();

            var query = new SearchDescriptor<Question>()
                .Query(q => q
                    .QueryString(m => m
                        .Fields(f => f
                            .Field(ff => ff.Title)
                            .Field(ff => ff.Body)
                        )
                        .Query("{{query_string}}")
                    )
                )
                .Aggregations(a => a
                    .Terms("tags", ta => ta
                        .Field(f => f.Tags)
                    )
                );

            var finalQuery = client.RequestResponseSerializer.SerializeToString(query);
        }

        static void Main(string[] args)
        {
            var terms = new List<string> { @"""City""", @"""Country""" };

            TemplatingPrototypeSimple(terms, "Title", "Id");
            TemplatingPrototypeWithRegx(terms, "Title", "Id");
            TemplatingPrototypeWithNest();

            Console.WriteLine("Hello World!");
        }
    }
}
