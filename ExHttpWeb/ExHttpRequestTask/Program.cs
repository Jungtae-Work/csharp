using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExHttpRequestTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task<string> task = Task.Run(async () =>
            {
                string url = "https://jsonplaceholder.typicode.com/posts";
                using (HttpClient client = new HttpClient())
                {
                    var postData = new
                    {
                        title = "foo",
                        body = "bar",
                        userId = 1
                    };

                    string json = System.Text.Json.JsonSerializer.Serialize(postData);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    try
                    {
                        HttpResponseMessage response = await client.PostAsync(url, content);
                        response.EnsureSuccessStatusCode(); // 응답 코드가 성공(200-299)인지 확인

                        string responseBody = await response.Content.ReadAsStringAsync();
                        return responseBody;
                    }
                    catch (HttpRequestException e)
                    {
                        return $"Request error: {e.Message}";
                    }
                }
            });

            Console.WriteLine("Task Start");
            Console.WriteLine(task.Result);
            Console.WriteLine("Task End");
        }
    }
}
