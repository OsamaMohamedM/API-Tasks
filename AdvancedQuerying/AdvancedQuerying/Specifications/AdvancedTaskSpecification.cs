using AdvancedQuerying.Models.Entities;

namespace AdvancedQuerying.Specifications;

public class AdvancedTaskSpecification : BaseSpecification<TaskEntity>
{
    public AdvancedTaskSpecification(
        string? searchTerm,
        string? status,
        int? minHours,
        int? maxHours,
        string? sortBy)
    {
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var searchTermLower = searchTerm.ToLower();
            AddCriteria(t => t.Title.ToLower().Contains(searchTermLower) || 
                           t.Description.ToLower().Contains(searchTermLower));
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            AddCriteria(t => t.Status == status);
        }

        if (minHours.HasValue)
        {
            AddCriteria(t => t.EstimatedHours >= minHours.Value);
        }

        if (maxHours.HasValue)
        {
            AddCriteria(t => t.EstimatedHours <= maxHours.Value);
        }

        ApplySorting(sortBy);
    }

    private void ApplySorting(string? sortBy)
    {
        switch (sortBy?.ToLower())
        {
            case "date":
                ApplyOrderByDescending(t => t.DueDate);
                break;
            case "hours":
                ApplyOrderBy(t => t.EstimatedHours);
                break;
            default:
                ApplyOrderBy(t => t.Id);
                break;
        }
    }
}
