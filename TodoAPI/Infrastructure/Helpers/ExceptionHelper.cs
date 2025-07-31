namespace TodoAPI.Infrastructure.Helpers;

public static class ExceptionHelper
{
    public static KeyNotFoundException NotFound<T>(object id)
    {
        var entityName = typeof(T).Name;
        return new KeyNotFoundException($"{entityName} with ID {id} not found.");
    }
}