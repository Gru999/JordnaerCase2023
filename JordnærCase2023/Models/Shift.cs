namespace JordnærCase2023.Models
{
    public class Shift
    {
        public int ShiftID { get; set; }
        public int MemberID { get; set; }
        public int ShiftType { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public Shift()
        {

        }

        public Shift(int shiftID, int memberID, int shiftType)
        {
            ShiftID = shiftID;
            MemberID = memberID;
            ShiftType = shiftType;
        }

        public override string ToString()
        {
            return $"{nameof(ShiftID)}: {ShiftID}, {nameof(MemberID)}: {MemberID}, {nameof(ShiftType)}, {ShiftType}";
        }
    }
}
