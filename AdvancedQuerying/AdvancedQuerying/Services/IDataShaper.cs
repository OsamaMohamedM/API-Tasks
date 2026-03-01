using System.Dynamic;

namespace AdvancedQuerying.Services;

public interface IDataShaper<T>
{
    IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string? fieldsString);
    ExpandoObject ShapeData(T entity, string? fieldsString);
}
