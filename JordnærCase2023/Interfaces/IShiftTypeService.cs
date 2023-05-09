using JordnærCase2023.Models;

namespace JordnærCase2023.Interfaces
{
    public interface IShiftTypeService
    {
        Task GetShiftById();
        Task<List<Shift>> GetAllShifts();
    }
}
