using JordnærCase2023.Models;
using JordnærCase2023.Services;

namespace EventTest
{
    [TestClass]
    public class EventServiceTest
    {
        private string connectionString = JordnærCase2023.Services.Secret.MyConnectionString;
        [TestMethod]
        public void TestCreateEvent()
        {
            //Arrange
            EventService eventService = new EventService();
            List<Event> events = eventService.GetAllEventsAsync().Result;

            //Act
            int numberOfEvents = events.Count;
            Event newEvent = new Event(20, "Strikke aften", new DateTime(2023, 06, 12, 19, 0, 0), new DateTime(2023, 06, 12, 20, 30, 0), 15);
            bool createOk = eventService.CreateEventAsync(newEvent).Result;
            events = eventService.GetAllEventsAsync().Result;

            int numberOfEventsAfterTest = events.Count;
            Event e = eventService.DeleteEventAsync(newEvent.EventId).Result;

            //Assert
            Assert.AreEqual(numberOfEvents + 1, numberOfEventsAfterTest);
            Assert.IsTrue(createOk);
            Assert.AreEqual(e.EventId, newEvent.EventId);
        }
    }
}