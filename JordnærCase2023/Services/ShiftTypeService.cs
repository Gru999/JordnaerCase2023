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

        public ShiftType GetShiftTypeById(int id)
        {
            foreach (var item in MockData.ShiftTypes)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }
    }
}

