using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Opgave_5
{
    public class ClientHandler
    {

        private readonly TcpClient _client;
        public ClientHandler(TcpClient client)
        {
            _client = client;
            HandleClient();
        }

        public void HandleClient()
        {
            Console.WriteLine("Client connected: " + _client.Client.RemoteEndPoint);
            //gør det muligt at benytte TcpClient
            NetworkStream ns = _client.GetStream();
            //vi modtager fra Client
            StreamReader reader = new StreamReader(ns);
            //Vi sender tilbage til Client
            StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };


            try
            {
                string receivedData = reader.ReadLine() ?? "";
                Request? request;

                try
                {
                    request = JsonSerializer.Deserialize<Request>(receivedData);

                    if (request == null)
                        throw new Exception("Invalid JSON format: Result is null.");
                }
                catch (JsonException jsonEx)
                {
                    throw new Exception("JSON error: " + jsonEx.Message);
                }

                var response = Requests(request);
                writer.WriteLine(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                var errorResponse = new Response("Error", ex.Message);
                writer.WriteLine(JsonSerializer.Serialize(errorResponse));
            }
            finally
            {
                _client.Close();
            }

        }

        public static Response Requests(Request request)
        {
            switch (request.Method)
            {
                case "Random":
                    int randomValue = new Random().Next(request.Num1, request.Num2 + 1);
                    return new Response("Random", randomValue.ToString());
                    

                case "Add":
                    return new Response("Add", (request.Num1 + request.Num2).ToString());

                case "Subtract":
                    return new Response("Subtract", (request.Num1 - request.Num2).ToString());

                default:
                    return new Response("Error", "Unknown method");
            }
        }
    }
}
