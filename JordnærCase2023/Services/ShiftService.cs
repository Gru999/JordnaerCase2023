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
        private string updateSql = "";
        private string deleteSql = "";
        private string getAllShiftsSql = "";
        private string getShiftsByIdSql = "";

        public ShiftService(IConfiguration configuration) : base(configuration)
        {

        }
        public ShiftService(string connectionString) : base(connectionString)
        {

        }

        // Async CRUD - ShiftCRUD -- Value Parametre mangler -- AllShift, ShiftById mangler yderligere info.
        // Mangler referencer til Foreign Keys (FK) når VagtTyper er merged.
        public async Task<bool> CreateShiftAsync(Shift shift)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(createSql, connection))
                {
                    command.Parameters.AddWithValue("@ShiftType", shift.ShiftType );
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
            return false;
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    using (SqlCommand command = new SqlCommand(updateSql, connection))
            //    {
            //        command.Parameters.AddWithValue();
            //        command.Parameters.AddWithValue();
            //        command.Parameters.AddWithValue();
            //        try
            //        {
            //            await command.Connection.OpenAsync();
            //            int noOfRows = await command.ExecuteNonQueryAsync();
            //            if (noOfRows == 1)
            //            {
            //                return true;
            //            }
            //            return false;
            //        }
            //        catch (SqlException sqlEx)
            //        {
            //            Console.WriteLine("Database Error: " + sqlEx.Message);
            //        }
            //    }
            //}
        }

        public async Task<Shift> DeleteShiftAsync(int id)
        {
            return null;
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    using (SqlCommand command = new SqlCommand(deleteSql, connection))
            //    {
            //        command.Parameters.AddWithValue();
            //        command.Parameters.AddWithValue();
            //        command.Parameters.AddWithValue();
            //        try
            //        {
            //            Shift shiftToReturn = await GetShiftsByIdAsync(id);
            //            await command.Connection.OpenAsync();
            //            int noOfRows = await command.ExecuteNonQueryAsync();
            //            return shiftToReturn;
            //        }
            //        catch (SqlException sqlEx)
            //        {
            //            Console.WriteLine("Database Error: " + sqlEx.Message);
            //        }
            //    }
            //}
        }

        public Task<List<Shift>> GetAllShiftsAsync()
        {
            return null;
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    using (SqlCommand command = new SqlCommand(getAllShiftsSql, connection))
            //    {
            //        command.Parameters.AddWithValue();
            //        command.Parameters.AddWithValue();
            //        command.Parameters.AddWithValue();
            //        try
            //        {

            //        }
            //        catch (SqlException sqlEx)
            //        {
            //            Console.WriteLine("Database Error: " + sqlEx.Message);
            //        }
            //    }
            //}
        }

        public Task<Shift> GetShiftsByIdAsync(int id)
        {
            return null;
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    using (SqlCommand command = new SqlCommand(getShiftsByIdSql, connection))
            //    {
            //        command.Parameters.AddWithValue();
            //        command.Parameters.AddWithValue();
            //        command.Parameters.AddWithValue();
            //        try
            //        {

            //        }
            //        catch (SqlException sqlEx)
            //        {
            //            Console.WriteLine("Database Error: " + sqlEx.Message);
            //        }
            //    }
            //}
        }
    }
}
