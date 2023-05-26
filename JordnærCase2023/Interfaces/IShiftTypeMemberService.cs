namespace JordnærCase2023.Interfaces
{
    using JordnærCase2023.Models;

    public interface IShiftTypeMemberService
    {
        Task<bool> CreateShiftTypeMember(int memberId, int shiftTypeId);
        Task<bool> DeleteShiftTypeMember(int memberId, int shiftTypeId);
        Task<List<int>> MemberShiftTypes(int memberId);
    }
}
