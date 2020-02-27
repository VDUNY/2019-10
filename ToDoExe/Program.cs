using Grpc.Net.Client;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace Huddled.ToDo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = new Channel("localhost", 5001, ChannelCredentials.Insecure);
            Huddled.Net.TunableValidator.SetValidator(true);
            var client = new ToDo.ToDoClient(channel);

            switch (args[0])
            {
                case "list":
                {
                    using (var response = client.AllToDo(new ListToDoRequest() { NotCompleted = false }))
                    {
                        while (await response.ResponseStream.MoveNext())
                        {
                            var todo = response.ResponseStream.Current;
                            Console.WriteLine(todo.Id + ": "+ todo.Title);
                        }
                    }
                    break;
                }
                case "get":
                {
                    if(args.Length > 1 && int.TryParse(args[1], out int id)) {
                        var todo = await client.GetToDoAsync(new ToDoId { Id = id });
                        Console.WriteLine(todo.Id + ": " + todo.Title);
                        if (!string.IsNullOrWhiteSpace(todo.Description))
                        {
                            Console.WriteLine(todo.Description);
                        }
                        Console.WriteLine(todo.Completed ? "Completed: " + todo.CompletedAt : "Due: " + todo.Reminder);
                    }
                    break;
                }
            }
        }
    }
}
