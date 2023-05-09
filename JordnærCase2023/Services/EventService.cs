using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace JordnærCase2023.Services
{
    public class EventService : Connection, IEventService
    {
        private string queryCreate = "insert into JEvent (Event_ID, Event_Name, Event_Description, Date_From, Date_To, Event_Img, Max_EventMembers)" +
                                     "values (@EventID, @EventName, @EventDescription, @DateFrom, @DateTo, @EventImg, @MaxEventMembers)";
        private string queryDelete = "delete from JEvent where Event_ID = @EventID";
        private string queryUpdate = "update JEvent set Event_ID = @EventID, Event_Name = @EventName, Event_Description = @EventDescription, Date_From = @DateFrom, Date_To = @DateTo, Event_Img = @EventImg, Max_EventMembers = @MaxEventMembers";
        private string queryEventFromId = "select * from JEvent where Event_ID = @EventID";
        private string queryGetAllEvent = "select * from JEvent";

        public EventService(IConfiguration configuration) : base(configuration)
        {
        }

        public EventService(string connectionString) :base(connectionString)
        {
            
        }


        public Task<bool> CreateEventAsync(Event @event)
        {
            throw new NotImplementedException();
        }

        public Task<Event> DeleteEventAsync(int eventId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            List<Event> events = new List<Event>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryGetAllEvent, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int eventId = reader.GetInt32(0);
                            string eventName = reader.GetString(1);
                            string? eventDescription = null ;
                            if (!reader.IsDBNull(2))
                            {
                                eventDescription = reader.GetString(2);
                            }
                            DateTime dateFrom = reader.GetDateTime(3);
                            DateTime dateTo = reader.GetDateTime(4);
                            string? eventImg = null;
                            if(!reader.IsDBNull(5))
                            {
                                eventImg = reader.GetString(5);
                            }
                            int maxEventMembers = reader.GetInt32(6);

                            Event @event = new Event(eventId, eventName, eventDescription, dateFrom, dateTo, eventImg, maxEventMembers);
                            events.Add(@event);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                        throw sqlEx;
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("Generel fejl" + exp.Message);
                        throw exp;
                    }
                }
            }
            return events;
        }

        public Task<Event> GetEventFromIdAsync(int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Event>> GetEventsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEventAsync(Event @event, int eventId)
        {
            throw new NotImplementedException();
        }
    }
}
