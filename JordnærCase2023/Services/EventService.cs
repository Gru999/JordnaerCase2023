using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Numerics;
using System.Xml.Linq;
using System.Windows.Input;

namespace JordnærCase2023.Services
{
    public class EventService : Connection, IEventService
    {
        private string queryCreate = "insert into JEvent (Event_Name, Event_Description, Date_From, Date_To, Event_Img, Max_EventMembers)" +
                                     "values (@EventName, @EventDescription, @DateFrom, @DateTo, @EventImg, @MaxEventMembers)";
        private string queryDeleteEvent = "delete from JEvent where Event_ID = @EventId";
        private string queryUpdate = "update JEvent set Event_Name = @EventName, Event_Description = @EventDescription, Date_From = @DateFrom, Date_To = @DateTo, " +
                                     "Event_Img = @EventImg, Max_EventMembers = @MaxEventMembers where Event_ID = @EventID";
        private string queryEventFromId = "select * from JEvent where Event_ID = @EventID";
        private string queryGetAllEvent = "select * from JEvent";
        private string queryGetAllEventByName = "select * from JEvent where Event_Name like @EventName";

        //For EventMember
        private string queryGetAllEventsForMember = "select JEvent.Event_ID, JEvent.Event_Name, JEvent.Event_Description, JEvent.Date_From, JEvent.Date_To, JEvent.Event_Img, JEvent.Max_EventMembers from JEvent, EventMember where JEvent.Event_ID = EventMember.Event_ID AND EventMember.Member_ID = @MemberId";
        private string queryGetAllMembersForEvent = "select Member.Member_ID, Member.Member_Name, Member.Member_Img, Member.Member_Phone, Member.Member_Email, Member.Member_Password, Member.Member_SanitationCourse, Member.Member_Admin from Member, EventMember where Member.Member_ID = EventMember.Member_ID AND EventMember.Event_ID = @EventId";
        private string queryCreateEventMember = "insert into EventMember values (@MemberId, @EventId)";
        private string queryDeleteEventMember = "delete from EventMember where Event_ID = @EventId AND Member_ID = @MemberId";
        private string queryEventMemberFromId = "select * from EventMember where Member_ID = @MemberId AND Event_ID = @EventId";

        public EventService(IConfiguration configuration) : base(configuration)
        {
        }

        public EventService(string connectionString) :base(connectionString)
        {   
        }


        public async Task<bool> CreateEventAsync(Event _event)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryCreate, connection);
                    command.Parameters.AddWithValue("@EventName", _event.EventName);
                    if (_event.EventDescription == null) {
                        command.Parameters.AddWithValue("@EventDescription", DBNull.Value);
                    } else {
                        command.Parameters.AddWithValue("@EventDescription", _event.EventDescription);
                    }
                    command.Parameters.AddWithValue("@DateFrom", _event.EventDateFrom);
                    command.Parameters.AddWithValue("@DateTo", _event.EventDateTo);
                    if (_event.EventImg == null) {
                        command.Parameters.AddWithValue("@EventImg", DBNull.Value);
                    } else {
                        command.Parameters.AddWithValue("@EventImg", _event.EventImg);
                    }
                    command.Parameters.AddWithValue("@MaxEventMembers", _event.EventMaxAttendance);
                    await command.Connection.OpenAsync();
                    int resultCreate = await command.ExecuteNonQueryAsync();
                    return resultCreate == 1;
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
                    SqlCommand command = new SqlCommand(queryDeleteEvent, connection);
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

                            Event _event = new Event(eventId, eventName, eventDescription, dateFrom, dateTo, eventImg, maxEventMembers);
                            events.Add(_event);
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

                        Event _event = new Event(eventId, eventName, eventDescription, dateFrom, dateTo, eventImg, maxEventMembers);
                        events.Add(_event);
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

        public async Task<bool> UpdateEventAsync(Event _event)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryUpdate, connection);
                    command.Parameters.AddWithValue("@EventID", _event.EventId);
                    command.Parameters.AddWithValue("@EventName", _event.EventName);
                    if (_event.EventDescription == null)
                    {
                        command.Parameters.AddWithValue("@EventDescription", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@EventDescription", _event.EventDescription);
                    }
                    command.Parameters.AddWithValue("@DateFrom", _event.EventDateFrom);
                    command.Parameters.AddWithValue("@DateTo", _event.EventDateTo);
                    if (_event.EventImg == null)
                    {
                        command.Parameters.AddWithValue("@EventImg", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@EventImg", _event.EventImg);
                    }
                    command.Parameters.AddWithValue("@MaxEventMembers", _event.EventMaxAttendance);
                    await command.Connection.OpenAsync();
                    int noOfRows = await command.ExecuteNonQueryAsync();
                    if (noOfRows == 1)
                    {
                        return true;
                    }

                    return false;
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
            return false;
        }

        //For EventMember - Done?
        public async Task<bool> CreateEMConnectionAsync(int memberId, int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryCreateEventMember, connection))
                {
                    command.Parameters.AddWithValue("@MemberId", memberId);
                    command.Parameters.AddWithValue("@EventId", eventId);
                    try
                    {
                        command.Connection.Open();
                        int noOfRows = await command.ExecuteNonQueryAsync();

                        if (noOfRows == 1)
                        {
                            return true;
                        }

                        return false;
                    }
                    catch (SqlException sqlex)
                    {
                        Console.WriteLine("Database error");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel fejl " + ex.Message);

                    }
                }

            }
            return false;
        }

        //For EventMember = Done? 
        public async Task<List<Event>> GetEventsForMemberAsync(int memberId)
        {
            List<Event> events = new List<Event>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand commmand = new SqlCommand(queryGetAllEventsForMember, connection);
                    commmand.Parameters.AddWithValue("@MemberId", memberId);
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

                        Event _event = new Event(eventId, eventName, eventDescription, dateFrom, dateTo, eventImg, maxEventMembers);
                        events.Add(_event);
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
            }
            return events;
        }

        public async Task<List<Member>> GetMembersFromEventAsync(int eventId)
        {
            List<Member> members = new List<Member>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand commmand = new SqlCommand(queryGetAllMembersForEvent, connection);
                    commmand.Parameters.AddWithValue("@MemberId", eventId);
                    await commmand.Connection.OpenAsync();
                    SqlDataReader reader = await commmand.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int memberId = reader.GetInt32(0);
                        string memberName = reader.GetString(1);
                        string? image = null;
                        if (!reader.IsDBNull(2))
                        {
                            image = reader.GetString(2);
                        }
                        int phone = reader.GetInt32(3);
                        string email = reader.GetString(4);
                        string password = reader.GetString(5);
                        bool sanitationcourse = reader.GetBoolean(6);
                        bool admin = reader.GetBoolean(7);

                        Member m = new Member(memberId, memberName, image, phone, email, password, sanitationcourse, admin);
                        members.Add(m);
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
            }
            return members;
        }

        //getEventMemberFromId
        //First member is always creator
        public async Task<List<Tuple<int, int>>> GetEventMemberFromIdAsync(int eventId, int memberId)
        {
            List<Tuple<int, int>> eventList = new List<Tuple<int, int>>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryEventMemberFromId, connection);
                    command.Parameters.AddWithValue("@MemberId", memberId);
                    command.Parameters.AddWithValue("@EventId", eventId);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int memberID = reader.GetInt32(1);
                        int eventID = reader.GetInt32(2);
                        Tuple<int, int> eventTuple = new Tuple<int, int>(memberID, eventID);
                        eventList.Add(eventTuple);
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
                return eventList;
            }
            return null;
        }

        public async Task<Event> DeleteEMAsync(int eventId, int memberId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryDeleteEventMember, connection);
                    command.Parameters.AddWithValue("@EventId", eventId);
                    command.Parameters.AddWithValue("@MemberId", memberId);
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
    }
}
