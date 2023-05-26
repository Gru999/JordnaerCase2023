using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace JordnærCase2023.Services
{
    public class ShiftTypeMemberService : Connection, IShiftTypeMemberService
    {

        private string insertSql = "insert into ShiftTypeMember Values (@MemberId, @ShiftTypeId)";
        private string deleteSql = "delete from ShiftTypeMember where Member_ID = @ID and ShiftType_ID = @ShiftTypeId";
        private string shiftTypeByMemberSql = "select * from ShiftTypeMember where Member_ID = @MemberId";

        public ShiftTypeMemberService(IConfiguration configuration) : base(configuration)
        {
        }

        public ShiftTypeMemberService(string connectionString) : base(connectionString)
        {
        }

        public async Task<bool> CreateShiftTypeMember(int memberId, int shiftTypeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@MemberId", memberId);
                    command.Parameters.AddWithValue("@ShiftTypeId", shiftTypeId);
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

        public async Task<bool> DeleteShiftTypeMember(int memberId, int shiftTypeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", memberId);
                    command.Parameters.AddWithValue("@ShiftTypeId", shiftTypeId);
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

        public async Task<List<int>> MemberShiftTypes(int memberId)
        {
            List<int> shiftTypes = new List<int>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(shiftTypeByMemberSql, connection))
                {
                    command.Parameters.AddWithValue("@MemberId", memberId);
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int shiftTypeMemberID = reader.GetInt32(0);
                            int memberID = reader.GetInt32(1);
                            int shiftTypeId = reader.GetInt32(2);

                            shiftTypes.Add(shiftTypeId);
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
            return shiftTypes;
        }
    }
}
