using JordnærCase2023.Models;
namespace JordnærCase2023.Interfaces
{
    public interface IEventService
    {
        Task<List<Event>> GetAllEventsAsync();
        Task<Event> GetEventFromIdAsync(int eventId);
        Task<bool> CreateEventAsync(Event @event);
        Task<bool> UpdateEventAsync(Event @event, int eventId);
        Task<Event> DeleteEventAsync(int eventId);
        Task<List<Event>> GetEventsByNameAsync(string name);
    }
}
