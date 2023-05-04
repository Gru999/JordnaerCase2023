using JordnaerMedlemmer.Models;

namespace JordnærCase2023.Models
{
    public class Shift
    {
        public int ShiftID { get; set; }
        public int? MemberID { get; set; }
        public int ShiftType { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public Shift()
        {

        }
        public Shift(int shiftID, int shiftType, DateTime dateFrom, DateTime dateTo)
        {
            ShiftID = shiftID;
            ShiftType = shiftType;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        public Shift(int shiftID, int? memberID, int shiftType, DateTime dateFrom, DateTime dateTo)
        {
            ShiftID = shiftID;
            MemberID = memberID;
            ShiftType = shiftType;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        public override string ToString()
        {
            return $"{nameof(ShiftID)}: {ShiftID}, {nameof(MemberID)}: {MemberID}, {nameof(ShiftType)}, {ShiftType}";
        }
    }
}
