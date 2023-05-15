namespace JordnærCase2023.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string? EventDescription { get; set; }
        public DateTime EventDateFrom { get; set; }
        public DateTime EventDateTo { get; set;}
        public string? EventImg { get; set; }
        public int EventMaxAttendance { get; set; }

        public Event()
        {
        }

        public Event(int eventId, string eventName, string? eventDescription, DateTime eventDateFrom, DateTime eventDateTo, string? eventImg, int eventMaxAttendance)
        {
            EventId = eventId;
            EventName = eventName;
            EventDescription = eventDescription;
            EventDateFrom = eventDateFrom;
            EventDateTo = eventDateTo;
            EventImg = eventImg;
            EventMaxAttendance = eventMaxAttendance;            
        }
        public Event(int eventId, string eventName, string? eventDescription, DateTime eventDateFrom, DateTime eventDateTo, int eventMaxAttendance)
        {
            EventId = eventId;
            EventName = eventName;
            EventDescription = eventDescription;
            EventDateFrom = eventDateFrom;
            EventDateTo = eventDateTo;
            EventMaxAttendance = eventMaxAttendance;
        }

        public Event(int eventId, string eventName, DateTime eventDateFrom, DateTime eventDateTo, string? eventImg, int eventMaxAttendance)
        {
            EventId = eventId;
            EventName = eventName;
            EventDateFrom = eventDateFrom;
            EventDateTo = eventDateTo;
            EventImg = eventImg;
            EventMaxAttendance = eventMaxAttendance;
        }

        public Event(int eventId, string eventName, DateTime eventDateFrom, DateTime eventDateTo, int eventMaxAttendance)
        {
            EventId = eventId;
            EventName = eventName;
            EventDateFrom = eventDateFrom;
            EventDateTo = eventDateTo;
            EventMaxAttendance = eventMaxAttendance;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
