namespace AdvancedQuerying.Models.DTOs;

public class PagedResult<T>
{
    public IEnumerable<T> Data { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}
