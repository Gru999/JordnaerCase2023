using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using Microsoft.AspNetCore.Http;
using System.Windows.Input;
using System.Data.SqlClient;

namespace JordnærCase2023.Services
{
    public class ItemService : Connection, IItemService
    {
        private String queryString = "SELECT * FROM Items";
        private String queryStringFromID = "SELECT * FROM Items WHERE Item_ID = @ID";
        private String insertSQL = "INSERT INTO Items (Item_ID, Item_Name, Item_Img, Item_Price, Item_Description, Item_Type) VALUES (@ID, @Name, @Img, @Price, @Description, @Type)";
        private String DeleteSQL = "DELETE FROM Items WHERE Item_ID = @ID";
        private String UpdateSQL = "UPDATE Items SET Item_Name = @Name, Item_Img = @Img, Item_Price = @Price, Item_Description = @Description, Item_Type = @Type WHERE Item_ID = @ID";
        private String ItemByNameSQl = "SELECT * FROM Items WHERE Item_Name LIKE @Name";



        public ItemService(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<bool> CreateItemAsync(Item item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertSQL, connection))
                {
                    command.Parameters.AddWithValue("@Item_ID", item.ItemID);
                    command.Parameters.AddWithValue("@Item_Name", item.ItemName);
                    command.Parameters.AddWithValue("@Item_Img", item.ItemImg);
                    command.Parameters.AddWithValue("@Item_Price", item.ItemPrice);
                    command.Parameters.AddWithValue("@Item_Description", item.ItemDescription);
                    command.Parameters.AddWithValue("@Item_Type", item.ItemType);
                    try
                    {
                        command.Connection.Open();
                        int noOfRows = await command.ExecuteNonQueryAsync(); //bruges ved update, delete, insert
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

        public Task<Item> DeleteItemAsync(int itemId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Item>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetItemFromIdAsync(int itemId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Item>> GetItemsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(Item item, int itemId)
        {
            throw new NotImplementedException();
        }
    }
}
