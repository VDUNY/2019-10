using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Huddled.Tasks.Controllers
{
    /// <summary>
    /// An implementation of a TODO task list API server
    /// </summary>
    // [ApiVersion("1.0")]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private static List<ToDo> mockData = new List<ToDo>();

        // private static Dictionary<int, ToDo> mockData = new Dictionary<int, ToDo>();

        static ToDoController()
        {
            mockData.Add(new ToDo { 
                Id = 0, 
                Title = "Plants", 
                Description = "Water the plants, except for the succulents",
                CreatedAt = DateTimeOffset.Now
            });
            mockData.Add(new ToDo { 
                Id = 1, 
                Title = "Cats", 
                Description = "Feed the cats wet food in the kitchen, and dry food downstairs",
                CreatedAt = DateTimeOffset.Now.AddHours(-2)
            });
        }

        /// <summary>
        /// Add a <see cref="ToDo"/>
        /// </summary>
        /// <param name="todo">The <see cref="ToDo"/> to add</param>
        [HttpPost]
        public ActionResult<ToDo> Create(ToDo todo)
        {
            todo.Id = mockData.Count;
            mockData.Add(todo);

            return CreatedAtRoute("Get", new { id = todo.Id }, todo);
        }

        /// <summary>
        /// Get a list of <see cref="ToDo"/>
        /// </summary>
        /// <param name="offset">The starting offset (for paging)</param>
        /// <param name="limit">The limit (count, for paging)</param>
        /// <param name="showCompleted">If true, the results include all completed jobs too</param>
        /// <returns>A (potentially empty) collection of <see cref="ToDo"/></returns>
        [HttpGet()]
        public IEnumerable<ToDo> List([FromQuery]int offset = 0, [FromQuery]int limit = int.MaxValue, [FromQuery]bool showCompleted = false)
        {
            return mockData.Where(t => !t.Completed || showCompleted).Skip(offset).Take(limit);
        }

        /// <summary>
        /// Get a specific <see cref="ToDo"/>
        /// </summary>
        /// <param name="id">An id to retrieve only one <see cref="ToDo"/></param>
        /// <returns>A single <see cref="ToDo"/></returns>
        [HttpGet("{id}", Name = "Get")]
        public ToDo Get([FromRoute]int id)
        {
            return mockData.FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Update (replace) a <see cref="ToDo"/>
        /// </summary>
        /// <param name="todo">The <see cref="ToDo"/> with a modified description</param>
        [HttpPut]
        public void Update(ToDo todo)
        {
            // yeah, it's very trusting ...
            var item = mockData.FirstOrDefault(t => t.Id == todo.Id);
            item.MergeFrom(todo);
        }

        /// <summary>
        /// Mark a specific <see cref="ToDo"/> done
        /// </summary>
        /// <param name="id"></param>
        [HttpPatch("{id}")]
        public void Complete([FromRoute]int id)
        {
            mockData.FirstOrDefault(t => t.Id == id).Completed = true;
        }

        /// <summary>
        /// Delete a <see cref="ToDo"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="ToDo"/> to delete</param>
        [HttpDelete("{id}")]
        public void Delete([FromRoute]int id)
        {
            mockData.RemoveAll(t => t.Id == id);
        }
    }
}

