namespace JordnærCase2023.Models
{
    public class ShiftType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Valid { get; set; }
        public ShiftType()
        {

        }

        public ShiftType(int id, string name, bool valid)
        {
            Id = id;
            Name = name;
            Valid = valid;
        }
        //public override string ToString()
        //{
        //    return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}";
        //}
    }
}
