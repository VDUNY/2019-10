namespace Huddled.ToDos.Models
{
    /// <summary>
    /// A task you need to do
    /// </summary>
    public class ToDoTask
    {
        /// <summary>
        /// The ID of the task (used in uniquely identifying and sorting)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The description of the task
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The person who has to do the task
        /// </summary>
        public string Owner { get; set; }
    }
}
