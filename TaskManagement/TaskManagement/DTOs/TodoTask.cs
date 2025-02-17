public class TodoTask
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? DueDate { get; set; }
    public Priority Priority { get; set; }
    public TaskCategory Category { get; set; }
    public TimeSpan? EstimatedTime { get; set; }
    public int? DependentTaskId { get; set; }
    public TaskStatus Status { get; set; }
    public int UserId { get; set; }
}