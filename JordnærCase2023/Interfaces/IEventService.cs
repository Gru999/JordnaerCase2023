using JordnærCase2023.Models;
namespace JordnærCase2023.Interfaces
{
    public interface IEventService
    {
        Task<List<Event>> GetAllEventsAsync();
        Task<Event> GetEventFromIdAsync(int eventId);
        Task<bool> CreateEventAsync(Event _event);
        Task<bool> UpdateEventAsync(Event _event);
        Task<Event> DeleteEventAsync(int eventId);
        Task<List<Event>> GetEventsByNameAsync(string name);
        Task<bool> CreateEventMemberAsync(int memberId, int eventId);
        Task<List<Event>> GetEventsForMemberAsync(int memberId);
    }
}
