public class CreateTaskDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public TaskCategory Category { get; set; }
    public DateTime? DueDate { get; set; }
    public TimeSpan? EstimatedTime { get; set; }
    public int? DependentTaskId { get; set; }
}