using System;

namespace Chat_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Chat("http://localhost:3000");

            Console.WriteLine("Press any key to continue...");
            Console.Read();
        }

        async static void Chat(string url)
        {
            var client = new SocketIOClient.SocketIO(url);

            client.On("hi", response =>
            {
                Console.WriteLine(response);

                string text = response.GetValue<string>();
                Console.WriteLine(text);
            });

            client.OnConnected += async (sender, e) =>
            {
                await client.EmitAsync("chat message", "socket.io");
            };

            await client.ConnectAsync();
        }
    }
}
