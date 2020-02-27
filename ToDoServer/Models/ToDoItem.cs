using System;

namespace Huddled.ToDo
{
    /// <summary>
    /// A task you need to do
    /// </summary>
    public class ToDoItem
    {
        /// <summary>
        /// The ID of the task (used in uniquely identifying and sorting)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// A one-line description of the task
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Details about the task. Not visible in lists
        /// </summary>
        public string Description { get; set; }

        private bool _completed;
        /// <summary>
        /// Indicates whether this task has been completed or not
        /// </summary>
        public bool Completed
        {
            get
            {
                return _completed;
            }

            set
            {
                if (value && !_completed)
                {
                    CompletedAt = DateTimeOffset.Now;
                }
                _completed = value;
            }
        }

        /// <summary>
        /// Date and time for a reminder
        /// </summary>
        public DateTimeOffset ReminderAt { get; set; }

        /// <summary>
        /// Date and time the task was created
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// The due date, or when the task was completed
        /// </summary>
        public DateTimeOffset CompletedAt { get; set; }

        /// <summary>
        /// Update this ToDo from another
        /// </summary>
        /// <param name="other">A copy with updated information</param>
        public void MergeFrom(ToDoItem other)
        {
            if (other == null)
            {
                return;
            }
            if (other.Id != 0)
            {
                Id = other.Id;
            }
            if (other.Title.Length != 0)
            {
                Title = other.Title;
            }
            if (other.Description.Length != 0)
            {
                Description = other.Description;
            }
            if (other.Completed != false)
            {
                Completed = other.Completed;
            }
            if (other.CreatedAt != null)
            {
                CreatedAt = other.CreatedAt;
            }
            if (Completed && other.CompletedAt != null)
            {
                CompletedAt = other.CompletedAt;
            }
            // The reminder we set even if it's empty, because you're allowed to clear this
            ReminderAt = other.ReminderAt;
        }
    }
}
