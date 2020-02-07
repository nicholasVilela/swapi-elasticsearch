using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Elasticsearch.Net;
using Nest;

namespace swapi_elasticsearch
{
    public class Program
    {
        public static void Main()
        {
            var uri = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(uri)
                .DefaultIndex("people");

            var client = new ElasticClient(settings);

            var person = new Person
            {
                Id = 1,
                Name = "Nicholas",
                Height = 666,
                Mass = 123,
                Gender = "Male"
            };

            var indexResponse = client.IndexDocument(person);

            var searchResponse = client.Search<Person>(s => s
                .From(0)
                .Size(10)
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Name)
                        .Query("Nicholas")
                    )
                )
            );

            var people = searchResponse.Documents;;
        }

        // public static void Main(string[] args)
        // {
        //     CreateHostBuilder(args).Build().Run();

        //     var settings = new ConnectionSettings(new Uri("http://localhost:5000"))
        //         .DefaultIndex("people");

        //     var client = new ElasticClient(settings);

        //     var person = new Person
        //     {
        //         Id = 1,
        //         Name = "Nicholas Vilela",
        //         Height = 666,
        //         Mass = 123,
        //         Gender = "Male"
        //     };

        //     var indexResponse = client.IndexDocument(person);

        //     var searchResponse = client.Search<Person>(s => s
        //         .From(0)
        //         .Size(10)
        //         .Query(q => q
        //             .Match(m => m
        //                 .Field(f => f.Name)
        //                 .Query("Nicholas")
        //             )
        //         )
        //     );

        //     var people = searchResponse.Documents;
        // }

        // public static IHostBuilder CreateHostBuilder(string[] args) =>
        //     Host.CreateDefaultBuilder(args)
        //         .ConfigureWebHostDefaults(webBuilder =>
        //         {
        //             webBuilder.UseStartup<Startup>();
        //         });

    }
}
