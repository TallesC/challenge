using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using challenge.Models;

namespace challenge.Service
{
    public class DataService
    {
        private static readonly HttpClient client = new HttpClient();
        public static async Task<List<Repository>> ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/takenet/repos?per_page=100");
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
            return ExtractRepositories(repositories);
        }

        private static List<Repository> ExtractRepositories(List<Repository> repositories) 
        {
            var newRepositories = new List<Repository>();
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



            return newRepositories;
        }

    }
}
