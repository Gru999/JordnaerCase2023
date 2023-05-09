namespace JordnærCase2023.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool SanitationCourse { get; set; }
        public bool Admin { get; set; }

        public Member()
        {

        }

        public Member(int id, string name, int phone, string email, string password, bool sanitationcourse, bool admin)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
            Password = password;
            SanitationCourse = sanitationcourse;
            Admin = admin;
        }

        public Member(int id, string name, string? image, int phone, string email, string password, bool sanitationcourse, bool admin)
        {
            Id = id;
            Name = name;
            Image = image;
            Phone = phone;
            Email = email;
            Password = password;
            SanitationCourse = sanitationcourse;
            Admin = admin;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
