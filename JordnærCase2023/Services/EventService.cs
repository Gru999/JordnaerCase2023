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
        private string queryUpdate = "update JEvent set Event_ID = @EventID, Event_Name = @EventName, Event_Description = @EventDescription, Date_From = @DateFrom, Date_To = @DateTo, Event_Img = @EventImg, Max_EventMembers = @MaxEventMembers where Event_ID = @EventID";
        private string queryEventFromId = "select * from JEvent where Event_ID = @EventID";
        private string queryGetAllEvent = "select * from JEvent";
        private string queryGetAllEventByName = "select * from JEvent where Event_Name = @EventName";

        public EventService(IConfiguration configuration) : base(configuration)
        {
        }

        public EventService(string connectionString) :base(connectionString)
        {
            
        }


        public async Task<bool> CreateEventAsync(Event @event)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryCreate, connection);
                    command.Parameters.AddWithValue("@EventID", @event.EventId);
                    command.Parameters.AddWithValue("@EventName", @event.EventName);
                    command.Parameters.AddWithValue("@EventDescription", @event.EventDescription);
                    command.Parameters.AddWithValue("@DateFrom", @event.EventDateFrom);
                    command.Parameters.AddWithValue("@DateTo", @event.EventDateTo);
                    command.Parameters.AddWithValue("@EventImg", @event.EventImg);
                    command.Parameters.AddWithValue("@MaxEventMemebers", @event.EventMaxAttendance);
                    await command.Connection.OpenAsync();
                    int result = await command.ExecuteNonQueryAsync();
                    return result == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel error " + ex.Message);
                }
            }
            return false;
        }

        public async Task<Event> DeleteEventAsync(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryDelete, connection);
                    command.Parameters.AddWithValue("@EventId", eventId);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        return await GetEventFromIdAsync(eventId);
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);
                }
            }
            return null;
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

        public async Task<Event> GetEventFromIdAsync(int eventID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryEventFromId, connection);
                    command.Parameters.AddWithValue("@EventId", eventID);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        int eventId = reader.GetInt32(0);
                        string eventName = reader.GetString(1);
                        string? eventDescription = null;
                        if (!reader.IsDBNull(2))
                        {
                            eventDescription = reader.GetString(2);
                        }
                        DateTime dateFrom = reader.GetDateTime(3);
                        DateTime dateTo = reader.GetDateTime(4);
                        string? eventImg = null;
                        if (!reader.IsDBNull(5))
                        {
                            eventImg = reader.GetString(5);
                        }
                        int maxEventMembers = reader.GetInt32(6);

                        Event e = new Event(eventId, eventName, eventDescription, dateFrom, dateTo, eventImg, maxEventMembers);
                        return e;
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);
                }
            }
            return null;
        }

        public async Task<List<Event>> GetEventsByNameAsync(string name)
        {
            List<Event> events = new List<Event>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand commmand = new SqlCommand(queryGetAllEventByName, connection);
                    string nameWildcard = "%" + name + "%";
                    commmand.Parameters.AddWithValue("@EventName", nameWildcard);
                    await commmand.Connection.OpenAsync();
                    SqlDataReader reader = await commmand.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int eventId = reader.GetInt32(0);
                        string eventName = reader.GetString(1);
                        string? eventDescription = null;
                        if (!reader.IsDBNull(2))
                        {
                            eventDescription = reader.GetString(2);
                        }
                        DateTime dateFrom = reader.GetDateTime(3);
                        DateTime dateTo = reader.GetDateTime(4);
                        string? eventImg = null;
                        if (!reader.IsDBNull(5))
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
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel error " + ex.Message);
                }
                return events;
            }
            return null;
        }

        public async Task<bool> UpdateEventAsync(Event @event, int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryUpdate, connection);
                    command.Parameters.AddWithValue("@EventID", @event.EventId);
                    command.Parameters.AddWithValue("@EventName", @event.EventName);
                    command.Parameters.AddWithValue("@EventDescription", @event.EventDescription);
                    command.Parameters.AddWithValue("@DateFrom", @event.EventDateFrom);
                    command.Parameters.AddWithValue("@DateTo", @event.EventDateTo);
                    command.Parameters.AddWithValue("@EventImg", @event.EventImg);
                    command.Parameters.AddWithValue("@MaxEventMemebers", @event.EventMaxAttendance);
                    await command.Connection.OpenAsync();
                    int updated = await command.ExecuteNonQueryAsync();
                    return updated == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);
                    throw ex;
                }
            }
            return false;
        }
    }
}
