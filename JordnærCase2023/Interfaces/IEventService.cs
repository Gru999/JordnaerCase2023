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
        Task<bool> CreateEMConnectionAsync(int memberId, int eventId);
        Task<List<Event>> GetEventsForMemberAsync(int memberId);
        //Task<List<Member>> GetMembersFromEventAsync(int eventId);
        //Task<List<Tuple<int, int>>> GetEventMemberFromIdAsync(int eventId, int memberId);
        Task<Event> DeleteEMAsync(int eventId, int memberId);

    }
}
