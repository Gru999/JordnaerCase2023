namespace JordnærCase2023.Models
{
    public class ShiftType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ShiftType()
        {

        }

        public ShiftType(int id, string name)
        {
            Id = id;
            Name = name;
        }
        //public override string ToString()
        //{
        //    return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}";
        //}
    }
}
