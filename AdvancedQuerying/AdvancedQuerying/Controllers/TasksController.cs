using AdvancedQuerying.Data;
using AdvancedQuerying.Models.DTOs;
using AdvancedQuerying.Services;
using AdvancedQuerying.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdvancedQuerying.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IDataShaper<TaskDto> _dataShaper;

    public TasksController(AppDbContext context, IMapper mapper, IDataShaper<TaskDto> dataShaper)
    {
        _context = context;
        _mapper = mapper;
        _dataShaper = dataShaper;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<object>>> GetTasks(
        [FromQuery] string? searchTerm,
        [FromQuery] string? status,
        [FromQuery] int? minHours,
        [FromQuery] int? maxHours,
        [FromQuery] string? sortBy,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? fields = null)
    {
        if (pageSize > 50)
        {
            pageSize = 50;
        }

        if (pageNumber < 1)
        {
            pageNumber = 1;
        }

        var specification = new AdvancedTaskSpecification(searchTerm, status, minHours, maxHours, sortBy);
        var query = SpecificationEvaluator<Models.Entities.TaskEntity>.GetQuery(_context.Tasks.AsQueryable(), specification);

        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var tasks = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var taskDtos = _mapper.Map<List<TaskDto>>(tasks);
        var shapedData = _dataShaper.ShapeData(taskDtos, fields);

        var result = new PagedResult<object>
        {
            Data = shapedData,
            TotalCount = totalCount,
            TotalPages = totalPages,
            CurrentPage = pageNumber
        };

        return Ok(result);
    }
}
