using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace JordnærCase2023.Services
{
    public class MemberService : Connection, IMemberService
    {

        private string queryString = "select * from Member";
        private string insertSql = "insert into Member Values (@Name, @Image, @Phone, @Email, @Password, @SanitationCourse, @Admin)";
        private string updateSql = "update Member" + "set Member_ID = @ID, Member_Name = @Name, Member_Img = @Image, Member_Phone = @Phone, Member_Email = @Email, Member_Password = @Password, Member_SanitationCourse = @SanitationCourse, Member_Admin = @Admin" + "where Member_ID = @ID";
        private string deleteSql = "delete from Member where Member_ID = @ID";
        private string queryStringFromName = "select * from Member where Member_Name like @Name";

        public MemberService(IConfiguration configuration) : base(configuration)
        {
        
        }

        public MemberService(string connectionString) : base(connectionString)
        {

        }

        public async Task<List<Member>> GetAllMembersAsync()
        {
            List<Member> members = new List<Member>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int memberID = reader.GetInt32(0);
                            string memberName = reader.GetString(1);
                            string? memberImg = null;
                            if (!reader.IsDBNull(2))
                            {
                                memberImg = reader.GetString(2);
                            }
                            int memberPhone = reader.GetInt32(3);
                            string memberEmail = reader.GetString(4);
                            string memberPassword = reader.GetString(5);
                            bool memberSanitationCourse = reader.GetBoolean(6);
                            bool memberAdmin = reader.GetBoolean(7);
                            Member member;
                            if (memberImg == null)
                            {
                                member = new Member(memberID, memberName, memberPhone, memberEmail, memberPassword, memberSanitationCourse, memberAdmin);
                            }
                            else
                            {
                                member = new Member(memberID, memberName, memberImg, memberPhone, memberEmail, memberPassword, memberSanitationCourse, memberAdmin);
                            }
                            members.Add(member);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                        return null;
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("Generel fejl" + exp.Message);
                        return null;
                    }
                }
            }
            return members;
        }

        public async Task<bool> CreateMemberAsync(Member member)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@Name", member.Name);
                    if(member.Image != null)
                    {
                        command.Parameters.AddWithValue("@Image", member.Image);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Image", DBNull.Value);
                    }
                    command.Parameters.AddWithValue("@Phone", member.Phone);
                    command.Parameters.AddWithValue("@Email", member.Email);
                    command.Parameters.AddWithValue("@Password", member.Password);
                    command.Parameters.AddWithValue("@SanitationCourse", member.SanitationCourse);
                    command.Parameters.AddWithValue("@Admin", member.Admin);
                    try
                    {
                        command.Connection.Open();
                        int noOfRows = await command.ExecuteNonQueryAsync();
                        if (noOfRows == 1)
                        {
                            return true;
                        }

                        return false;
                    }
                    catch (SqlException sqlex)
                    {
                        Console.WriteLine("Database error");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel error");
                    }
                }

            }
            return false;
        }

        public async Task<bool> UpdateMemberAsync(Member member, int memberID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", member.Id);
                    command.Parameters.AddWithValue("@Name", member.Name);
                    command.Parameters.AddWithValue("@Image", member.Image);
                    command.Parameters.AddWithValue("@Phone", member.Phone);
                    command.Parameters.AddWithValue("@Email", member.Email);
                    command.Parameters.AddWithValue("@Password", member.Password);
                    command.Parameters.AddWithValue("@SanitationCourse", member.SanitationCourse);
                    command.Parameters.AddWithValue("@Admin", member.Admin);
                    try
                    {
                        command.Connection.Open();
                        int noOfRows = await command.ExecuteNonQueryAsync();
                        if (noOfRows == 1)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("Generel fejl" + exp.Message);
                    }
                }
            }
            return false;
        }

        public async Task<bool> DeleteMemberAsync(int memberID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", memberID);
                    try
                    {
                        command.Connection.Open();
                        int noOfRows = await command.ExecuteNonQueryAsync();
                        if (noOfRows == 1)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("Generel fejl" + exp.Message);
                    }
                }
            }
            return false;
        }

        public async Task<List<Member>> GetMembersByName(string memberName)
        {
            List<Member> members = new List<Member>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryStringFromName, connection);
                    string nameWild = "%" + memberName + "%";
                    command.Parameters.AddWithValue("@Name", nameWild);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int memberID = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string? memberImg = null;
                        if (!reader.IsDBNull(2))
                        {
                            memberImg = reader.GetString(2);
                        }
                        int memberPhone = reader.GetInt32(3);
                        string memberEmail = reader.GetString(4);
                        string memberPassword = reader.GetString(5);
                        bool memberSanitationCourse = reader.GetBoolean(6);
                        bool memberAdmin = reader.GetBoolean(7);
                        Member member;
                        if (memberImg == null)
                        {
                            member = new Member(memberID, name, memberPhone, memberEmail, memberPassword, memberSanitationCourse, memberAdmin);
                        }
                        else
                        {
                            member = new Member(memberID, name, memberImg, memberPhone, memberEmail, memberPassword, memberSanitationCourse, memberAdmin);
                        }
                        members.Add(member);
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception exp)
                {
                    Console.WriteLine("Generel fejl" + exp.Message);
                }
                return members;
            }
            return null;
        }
    }
}
