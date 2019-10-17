using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Huddled.ToDo.Services
{
    public class ToDoService : ToDoList.ToDoListBase
    {
        private static List<ToDo> mockData = new List<ToDo>();

        static ToDoService()
        {
            mockData.Add(new ToDo { 
                Id = 0, 
                Title = "Cat", 
                Description = "Feed the cat", 
                CreatedAt = Timestamp.FromDateTimeOffset(DateTimeOffset.Now)
            });
            mockData.Add(new ToDo { 
                Id = 1, 
                Title = "Plants",
                Description = "Water the plants",
                CreatedAt = Timestamp.FromDateTimeOffset(DateTimeOffset.Now.AddHours(-2))
            });
        }


        public override Task<ToDo> CreateToDo(CreateToDoRequest request, ServerCallContext context)
        {
            var todo = new ToDo
            {
                Id = mockData.Count > 0 ? mockData.Max(t => t.Id) + 1 : 1,
                Title = request.Title,
                Description = request.Description
            };
            mockData.Add(todo);

            return Task.FromResult(todo);
        }

        public override Task<ToDoId> DeleteToDo(UpdateToDoRequest request, ServerCallContext context)
        {
            if (mockData.RemoveAll(t => t.Id == request.Id) < 1)
            {
                request.Id = -1;
            }

            return Task.FromResult(new ToDoId { Id = request.Id });
        }

        public override Task<ToDoId> CompleteToDo(UpdateToDoRequest request, ServerCallContext context)
        {
            var item = mockData.FirstOrDefault(t => t.Id == request.Id);
            item.Completed = true;
            item.CompletedAt = request.Timestamp;

            return Task.FromResult(new ToDoId { Id = item.Id });
        }

        public override Task<ToDo> GetToDo(ToDoId request, ServerCallContext context)
        {
            return Task.FromResult(mockData.FirstOrDefault(t => t.Id == request.Id));
        }

        public async override Task ListToDo(ListToDoRequest request, IServerStreamWriter<ToDo> responseStream, ServerCallContext context)
        {
            foreach(ToDo t in mockData)
            {
                await responseStream.WriteAsync(t);
            }

            return;
        }

        public override Task<ToDo> UpdateToDo(ToDo request, ServerCallContext context)
        {
            var item = mockData.FirstOrDefault(t => t.Id == request.Id);
            item.MergeFrom(request);

            return Task.FromResult(item);
        }
    }
}