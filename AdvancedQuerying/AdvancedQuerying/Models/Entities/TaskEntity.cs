namespace AdvancedQuerying.Models.Entities;

public class TaskEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int EstimatedHours { get; set; }
    public DateTime DueDate { get; set; }
    public string? SecretManagerNotes { get; set; }
}
