using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace JordnærCase2023.Services
{
    public class ShiftService : Connection, IShiftService
    {
        // Query string -- Sql funktioner mangler.
        private string createSql = "insert into JShift (ShiftType_ID, Date_From, Date_To) " +
                                    "Values (@ShiftType, @DateFrom, @DateTo)";
        private string updateSql = "update JShift set Member_ID = @MemberId, ShiftType_ID = @ShiftTypeId," +
            "Date_From = @DateFrom, Date_To = @DateTo";
        private string deleteSql = "delete from JShift where Shift_ID = @ShiftId";
        private string getAllShiftsSql = "select * from JShift";
        private string getShiftsByIdSql = "select * from JShift where Shift_ID = @ShiftId";

        public ShiftService(IConfiguration configuration) : base(configuration)
        {

        }
        public ShiftService(string connectionString) : base(connectionString)
        {

        }

        // Mangler referencer til Foreign Keys (FK) når VagtTyper er oprettet.
        public async Task<bool> CreateShiftAsync(Shift shift)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(createSql, connection))
                {
                    command.Parameters.AddWithValue("@ShiftType", shift.ShiftType);
                    command.Parameters.AddWithValue("@DateFrom", shift.DateFrom);
                    command.Parameters.AddWithValue("@DateTo", shift.DateTo);
                    try
                    {
                        await command.Connection.OpenAsync();
                        int noOfRows = await command.ExecuteNonQueryAsync();
                        if (noOfRows == 1)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database Error: " + sqlEx.Message);
                    }
                }
            }
            return false;
        }
        public async Task<bool> UpdateShiftAsync(Shift shift)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateSql, connection))
                {
                    command.Parameters.AddWithValue("@MemberID", shift.MemberID);
                    command.Parameters.AddWithValue("@ShiftTypeId", shift.ShiftType);
                    command.Parameters.AddWithValue("@DateFrom", shift.DateFrom);
                    command.Parameters.AddWithValue("@DateTo", shift.DateTo);
                    try
                    {
                        await command.Connection.OpenAsync();
                        int noOfRows = await command.ExecuteNonQueryAsync();
                        if (noOfRows == 1)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database Error: " + sqlEx.Message);
                    }
                }
            }
            return false;
        }

        public async Task<Shift> DeleteShiftAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@ShiftId", id);
                    try
                    {
                        Shift shiftToReturn = await GetShiftsByIdAsync(id);
                        await command.Connection.OpenAsync();
                        int noOfRows = await command.ExecuteNonQueryAsync();
                        return shiftToReturn;
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database Error: " + sqlEx.Message);
                    }
                }
            }
            return null;
        }

        public async Task<List<Shift>> GetAllShiftsAsync()
        {
            List<Shift> shifts = new List<Shift>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(getAllShiftsSql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int shiftId = reader.GetInt32(0);
                            int? memberId = null;
                            if (!reader.IsDBNull(1))
                            {
                                memberId = reader.GetInt32(1);
                            }
                            int shiftTypeId = reader.GetInt32(2);
                            DateTime dateFrom = reader.GetDateTime(3);
                            DateTime dateTo = reader.GetDateTime(4);
                            Shift shift;
                            if (memberId == null)
                            {
                                shift = new Shift(shiftId, shiftTypeId, dateFrom, dateTo);
                            }
                            else
                                shift = new Shift(shiftId, memberId, shiftTypeId, dateFrom, dateTo);
                            shifts.Add(shift);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database Error: " + sqlEx.Message);
                    }
                }
            }
            return shifts;
        }

        public async Task<Shift> GetShiftsByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(getShiftsByIdSql, connection))
                {
                    command.Parameters.AddWithValue("@ShiftId", id);
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if (await reader.ReadAsync())
                        {
                            int shiftId = reader.GetInt32(0);
                            int? memberId = null;
                            if (!reader.IsDBNull(1))
                            {
                                memberId = reader.GetInt32(1);
                            }
                            int shiftTypeId = reader.GetInt32(2);
                            DateTime dateFrom = reader.GetDateTime(3);
                            DateTime dateTo = reader.GetDateTime(4);
                            Shift shift;
                            if (memberId == null)
                            {
                                shift = new Shift(shiftId, shiftTypeId, dateFrom, dateTo);
                            }
                            else
                                shift = new Shift(shiftId, memberId, shiftTypeId, dateFrom, dateTo);
                            return shift;
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database Error: " + sqlEx.Message);
                    }
                }
            }
            return null;
        }
    }
}
