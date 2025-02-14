using System;

namespace Library
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string AdminResponse { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Feedback(Guid userId, string message)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Message = message;
            Status = "В обработке";
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}