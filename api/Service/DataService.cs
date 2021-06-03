using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using challenge.Models;
using Newtonsoft.Json.Linq;
using System.Linq;
using Newtonsoft.Json;

namespace challenge.Service
    //Author: TallesC Data management and transform to make a response
{
    public class DataService
    {
        private static readonly HttpClient client = new HttpClient();
       //Deserialize and take data from GitHub Api - Take Repo in Created_at asc order.
        public static async Task<String> ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/takenet/repos?per_page=100");
            var repositories = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
            return ExtractRepositories(repositories);
        }

        //Filter C# repositories from data extracted
        private static String ExtractRepositories(IList<Repository> repositories) 
        {
            IList<Repository> newRepositories = new List<Repository>();
            int cont = 0;
       
            foreach (Repository repo in repositories)
            {
                if (repo.Language == "C#")
                {
                    newRepositories.Add(repo);
                    cont++;
                } 
                if (cont == 5)
                {
                    break;
                }
            }

            return SerializeJson(newRepositories);
        }

        //Serialize and make the formated response
        private static String SerializeJson(IList<Repository> repositories)
        {
            String cabecalhoJson = @"{
                'itemType': 'application/vnd.lime.document-select+json',
                'items':
                ";

            JArray responseArray = new JArray(          
                
                    repositories.Select(p => new JObject
                {
                    {
                         "header", new JObject             
                         {
                             { "type", "application/vnd.lime.media-link+json" },
                             { "value", new JObject
                                {
                                    { "title", p.Name },
                                    { "text", p.Description },
                                    { "type", "image/png" },
                                    { "uri", (new Uri (p.Owner.GitAvatar_url+".png")) }
                                }
                             },
                         }
                    }

                    })
      
            );

            string json = JsonConvert.SerializeObject(responseArray, Formatting.Indented);
            return (cabecalhoJson + json + "}");
        }

    }
}
