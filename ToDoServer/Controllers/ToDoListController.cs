using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoServer.Models;

namespace ToDoServer.Controllers
{
    // [ApiVersion("1.0")]
    [Route("[controller]")]
    public class ToDoListController : ControllerBase
    {
        private static Dictionary<int, ToDoTask> mockData = new Dictionary<int, ToDoTask>();

        static ToDoListController()
        {
            mockData.Add(0, new ToDoTask { Id = 0, Owner = "*", Description = "feed the dog" });
            mockData.Add(1, new ToDoTask { Id = 1, Owner = "*", Description = "take the dog on a walk" });
        }

        /// <summary>
        /// Get a (list of) <see cref="ToDoTask"/> for the specified user
        /// </summary>
        /// <param name="owner">The user to filter by</param>
        /// <param name="id">An id to retrieve only one <see cref="ToDoTask"/></param>
        /// <returns>A (potentially empty) collection of <see cref="ToDoTask"/></returns>
        [HttpGet("{owner}", Name = "GetList")]
        public IEnumerable<ToDoTask> Get([FromRoute]string owner)
        {
            return mockData.Values.Where(m => m.Owner == owner || owner == "*");
        }

        /// <summary>
        /// Get a (list of) <see cref="ToDoTask"/> for the specified user
        /// </summary>
        /// <param name="owner">The user to filter by</param>
        /// <param name="id">An id to retrieve only one <see cref="ToDoTask"/></param>
        /// <returns>A (potentially empty) collection of <see cref="ToDoTask"/></returns>
        [HttpGet("{owner}/{id}", Name = "GetTask")]
        public ToDoTask GetTask([FromRoute]string owner, [FromRoute]int id)
        {
            return mockData.Values.Where(m => (m.Owner == owner || owner == "*" ) && m.Id == id).First();
        }

        /// <summary>
        /// Add a <see cref="ToDoTask"/>
        /// </summary>
        /// <param name="todo">The <see cref="ToDoTask"/> to add</param>
        [HttpPost]
        public ActionResult<ToDoTask> Post(ToDoTask todo)
        {
            todo.Id = mockData.Count > 0 ? mockData.Keys.Max() + 1 : 1;
            mockData.Add(todo.Id, todo);

            return CreatedAtRoute("GetTask", new { owner = todo.Owner, id = todo.Id }, todo);
        }

        /// <summary>
        /// Modify a <see cref="ToDoTask"/>
        /// </summary>
        /// <param name="todo">The <see cref="ToDoTask"/> with a modified description</param>
        [HttpPut]
        public void Put(ToDoTask todo)
        {
            ToDoTask xtodo = mockData.Values.First(a => (a.Owner == todo.Owner || todo.Owner == "*") && a.Id == todo.Id);
            if (todo != null && xtodo != null)
            {
                xtodo.Description = todo.Description;
            }
        }

        /// <summary>
        /// Delete a <see cref="ToDoTask"/>
        /// </summary>
        /// <param name="owner">The user that owns the <see cref="ToDoTask"/> to delete</param>
        /// <param name="id">The id of the <see cref="ToDoTask"/> to delete</param>
        [HttpDelete]
        public void Delete([FromRoute]string owner, [FromRoute]int id)
        {
            ToDoTask todo = mockData.Values.First(a => (a.Owner == owner || owner == "*") && a.Id == id);
            if (todo != null)
            {
                mockData.Remove(todo.Id);
            }
        }
    }
}

