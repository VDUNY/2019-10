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
        private static Dictionary<int, Task> mockData = new Dictionary<int, Task>();

        static ToDoController()
        {
            mockData.Add(0, new Task { Id = 0, Title = "Plants", Details = "Water the plants, except for the succulents" });
            mockData.Add(1, new Task { Id = 1, Title = "Cats", Details = "Feed the cats wet food in the kitchen, and dry food downstairs" });
        }

        /// <summary>
        /// Get a list of <see cref="Task"/>
        /// </summary>
        /// <param name="showCompleted">If true, the results include all completed jobs too</param>
        /// <returns>A (potentially empty) collection of <see cref="Task"/></returns>
        [HttpGet()]
        public IEnumerable<Task> List([FromQuery]bool showCompleted = false)
        {
            return mockData.Values.Where(t => !t.Completed || showCompleted);
        }

        /// <summary>
        /// Get a specific <see cref="Task"/>
        /// </summary>
        /// <param name="id">An id to retrieve only one <see cref="Task"/></param>
        /// <returns>A single <see cref="Task"/></returns>
        [HttpGet("{id}", Name = "Get")]
        public Task Get([FromRoute]int id)
        {
            return mockData[id];
        }

        /// <summary>
        /// Add a <see cref="Task"/>
        /// </summary>
        /// <param name="todo">The <see cref="Task"/> to add</param>
        [HttpPost]
        public ActionResult<Task> Create(Task todo)
        {
            todo.Id = mockData.Count > 0 ? mockData.Keys.Max() + 1 : 1;
            mockData.Add(todo.Id, todo);

            return CreatedAtRoute("Get", new { id = todo.Id }, todo);
        }

        /// <summary>
        /// Update (replace) a <see cref="Task"/>
        /// </summary>
        /// <param name="todo">The <see cref="Task"/> with a modified description</param>
        [HttpPut]
        public void Update(Task todo)
        {
            // yeah, it's very trusting ...
            mockData[todo.Id] = todo;
        }

        /// <summary>
        /// Delete a <see cref="Task"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="Task"/> to delete</param>
        [HttpDelete("{id}")]
        public void Delete([FromRoute]int id)
        {
            mockData.Remove(id);
        }

        /// <summary>
        /// Mark a specific <see cref="Task"/> done
        /// </summary>
        /// <param name="id"></param>
        [HttpPatch("{id}")]
        public void Complete([FromRoute]int id)
        {
            mockData[id].Completed = true;
        }
}
}

