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
    public class ToDoService : ToDo.ToDoBase
    {
        private static List<ToDoItem> mockData = new List<ToDoItem>();

        static ToDoService()
        {
            mockData.Add(new ToDoItem {
                Id = 0,
                Title = "Plants",
                Description = "Water the plants, except for the succulents",
                CreatedAt = Timestamp.FromDateTimeOffset(DateTimeOffset.Now)
            });
            mockData.Add(new ToDoItem {
                Id = 1,
                Title = "Cats",
                Description = "Feed the cats wet food in the kitchen, and dry food downstairs",
                CreatedAt = Timestamp.FromDateTimeOffset(DateTimeOffset.Now.AddHours(-2))
            });
        }


        public override Task<ToDoItem> CreateToDo(CreateToDoRequest request, ServerCallContext context)
        {
            var ToDoItem = new ToDoItem
            {
                Id = mockData.Count > 0 ? mockData.Max(t => t.Id) + 1 : 1,
                Title = request.Title,
                Description = request.Description
            };
            mockData.Add(ToDoItem);

            return Task.FromResult(ToDoItem);
        }

        public async override Task AllToDo(ListToDoRequest request, IServerStreamWriter<ToDoItem> responseStream, ServerCallContext context)
        {
            foreach (ToDoItem t in mockData.Where(t => !t.Completed || request.NotCompleted).Skip(request.Offset).Take(request.Limit > 0 ? request.Limit : int.MaxValue))
            {
                await responseStream.WriteAsync(t);
            }

            return;
        }

        public override Task<ToDoList> ListToDo(ListToDoRequest request, ServerCallContext context)
        {
            var list = new ToDoList();
            list.List.AddRange(mockData.Where(t => !t.Completed || request.NotCompleted).Skip(request.Offset).Take(request.Limit > 0 ? request.Limit : int.MaxValue));
            
            return Task.FromResult(list);
        }


        public override Task<ToDoItem> GetToDo(ToDoId request, ServerCallContext context)
        {
            return Task.FromResult(mockData.FirstOrDefault(t => t.Id == request.Id));
        }

        public override Task<ToDoItem> UpdateToDo(ToDoItem request, ServerCallContext context)
        {
            var item = mockData.FirstOrDefault(t => t.Id == request.Id);
            item.MergeFrom(request);

            return Task.FromResult(item);
        }

        public override Task<ToDoId> CompleteToDo(ToDoId request, ServerCallContext context)
        {
            var item = mockData.FirstOrDefault(t => t.Id == request.Id);
            item.Completed = true;
            item.CompletedAt = Timestamp.FromDateTimeOffset(DateTimeOffset.Now);

            return Task.FromResult(new ToDoId { Id = item.Id });
        }

        public override Task<ToDoId> DeleteToDo(ToDoId request, ServerCallContext context)
        {
            if (mockData.RemoveAll(t => t.Id == request.Id) < 1)
            {
                request.Id *= -1;
            }

            return Task.FromResult(new ToDoId { Id = request.Id });
        }
    }
}