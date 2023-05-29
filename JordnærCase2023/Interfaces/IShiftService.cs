using JordnærCase2023.Models;

namespace JordnærCase2023.Interfaces
{
    public interface IShiftService
    {
        Task<bool> CreateShiftAsync(Shift shift);
        Task<bool> UpdateShiftAsync(Shift shift, int shiftTypeId);
        Task<Shift> DeleteShiftAsync(int id);
        Task<List<Shift>> GetAllShiftsAsync();
        Task<Shift> GetShiftsByIdAsync(int id);
        Task<List<Shift>> ShiftsByMember(int id);
        Task<bool> MemberToShift(int shiftId, int memberId);
    } 
}
