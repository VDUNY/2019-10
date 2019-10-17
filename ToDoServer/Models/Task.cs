using System;

namespace Huddled.ToDo
{
    /// <summary>
    /// A task you need to do
    /// </summary>
    public class Task
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
        public string Details { get; set; }

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
    }
}
