using JordnærCase2023.Models;

namespace JordnærCase2023.Data
{
    public class MockData
    {
        public static List<ShiftType> ShiftTypes = new List<ShiftType> 
        { 
        new ShiftType { Id = 1, Name = "CafeVagt", Description = ""}, 
        new ShiftType { Id = 2, Name = "BagerVagt", Description = ""}, 
        new ShiftType { Id = 3, Name = "CafeVagtFøl", Description = ""}, 
        new ShiftType { Id = 4, Name = "BagerVagtFøl", Description = ""}
        };
    }
}
