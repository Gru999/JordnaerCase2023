using JordnærCase2023.Models;

namespace JordnærCase2023.Data
{
    public class MockData
    {
        public static List<ShiftType> ShiftTypes = new List<ShiftType> 
        {  
        new ShiftType { Id = 1, Name = "BagerVagt"},
        new ShiftType { Id = 2, Name = "CafeVagt"},
        new ShiftType { Id = 3, Name = "BagerVagtFøl"}, 
        new ShiftType { Id = 4, Name = "CafeVagtFøl"}
        };
    }
}
