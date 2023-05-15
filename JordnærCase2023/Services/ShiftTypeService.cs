using JordnærCase2023.Data;
using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;

namespace JordnærCase2023.Services
{
    public class ShiftTypeService : IShiftTypeService
    {
        public List<ShiftType> GetAllShiftTypes()
        {
            return MockData.ShiftTypes;
        }

        public List<ShiftType> GetShiftTypeById(int id) 
        {
            List<ShiftType> shiftTypes = new List<ShiftType>();
            for (int i = 0; i < 5; i++)
            {
                shiftTypes.Add(MockData.ShiftTypes[i]);
            }
            return shiftTypes;
        }
    }
}

