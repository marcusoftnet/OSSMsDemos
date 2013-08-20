using System;
using RestSharp;
using Xunit;

namespace DemoRestSharp
{
    public class RestSharpTests
    {
        private const string OAUTH_TOKEN = "268846e56751293fe10ef679485036da0b077608";
        private const string SEARCH_STRING = "/search/repositories?q={0}&sort=stars&order=desc";

        [Fact]
        public void should_get_search_result_from_github_api()
        {
            // Arrange
            var client = new RestClient("https://api.github.com") {
                Authenticator = new OAuth2UriQueryParameterAuthenticator(OAUTH_TOKEN)
            };

            var request = new RestRequest(string.Format(SEARCH_STRING, "SpecFlow.Assist.Dynamic"), Method.GET);
            request.AddHeader("Accept", "application/vnd.github.preview");

            // Act
            var content = client.Execute(request).Content;
            
            // Assert
            Console.WriteLine(content);
            Assert.NotEmpty(content);
        }
    }


    public class RepositorySearchResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
    }
}