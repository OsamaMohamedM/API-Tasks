using AdvancedQuerying.Models.Entities;

namespace AdvancedQuerying.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Tasks.Any())
        {
            return;
        }

        var tasks = new List<TaskEntity>
        {
            new TaskEntity
            {
                Title = "Complete API Documentation",
                Description = "Write comprehensive API documentation for the new endpoints",
                Status = "InProgress",
                EstimatedHours = 8,
                DueDate = DateTime.Now.AddDays(5),
                SecretManagerNotes = "High priority - CEO requested this"
            },
            new TaskEntity
            {
                Title = "Fix Login Bug",
                Description = "Users are unable to login with special characters in password",
                Status = "Completed",
                EstimatedHours = 3,
                DueDate = DateTime.Now.AddDays(-2),
                SecretManagerNotes = "Critical security issue - handle with care"
            },
            new TaskEntity
            {
                Title = "Database Migration",
                Description = "Migrate production database to new server",
                Status = "Pending",
                EstimatedHours = 20,
                DueDate = DateTime.Now.AddDays(15),
                SecretManagerNotes = "Schedule downtime with ops team"
            },
            new TaskEntity
            {
                Title = "Code Review",
                Description = "Review pull requests from team members",
                Status = "InProgress",
                EstimatedHours = 5,
                DueDate = DateTime.Now.AddDays(2),
                SecretManagerNotes = "Focus on security aspects"
            },
            new TaskEntity
            {
                Title = "Update Dependencies",
                Description = "Update all NuGet packages to latest stable versions",
                Status = "Pending",
                EstimatedHours = 4,
                DueDate = DateTime.Now.AddDays(10),
                SecretManagerNotes = "Test thoroughly before deployment"
            },
            new TaskEntity
            {
                Title = "Performance Optimization",
                Description = "Optimize database queries in reporting module",
                Status = "InProgress",
                EstimatedHours = 12,
                DueDate = DateTime.Now.AddDays(7),
                SecretManagerNotes = "User complaints about slow reports"
            },
            new TaskEntity
            {
                Title = "Setup CI/CD Pipeline",
                Description = "Configure automated deployment pipeline",
                Status = "Completed",
                EstimatedHours = 6,
                DueDate = DateTime.Now.AddDays(-5),
                SecretManagerNotes = "Use Azure DevOps as discussed"
            },
            new TaskEntity
            {
                Title = "Mobile App Testing",
                Description = "Test new features on iOS and Android devices",
                Status = "Pending",
                EstimatedHours = 15,
                DueDate = DateTime.Now.AddDays(20),
                SecretManagerNotes = "Coordinate with QA team lead"
            }
        };

        context.Tasks.AddRange(tasks);
        context.SaveChanges();
    }
}
