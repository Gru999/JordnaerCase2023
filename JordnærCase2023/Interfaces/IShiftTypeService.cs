using JordnærCase2023.Models;
using System.Runtime.InteropServices;

namespace JordnærCase2023.Interfaces
{
    public interface IShiftTypeService
    {
        ShiftType GetShiftTypeById(int id);
        List<ShiftType> GetAllShiftTypes();
        //Task<bool> CreateShiftTypeAsync(ShiftType shiftType);
        //Task<bool> UpdateShiftType(ShiftType shiftType, int shiftId);
        //Task<ShiftType> DeleteShiftType(int typeId);

    }
}
