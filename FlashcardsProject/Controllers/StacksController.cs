using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FlashcardsProject.Models;
using Microsoft.Data.SqlClient;

namespace FlashcardsProject.Controllers
{
    public static class StacksController
    {
        public static string connectionString = "Data Source=(localdb)\\LocalDB;Database=FlashCardsDB;Trusted_Connection=true";

        public static void Add(Stack stack)
        {
            using(var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sql = "INSERT INTO Stacks (Name) VALUES (@Name)";

                connection.Execute(sql, stack);

                connection.Close();
            }
        }

        public static void Delete(string stackName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sql = "DELETE FROM Stacks WHERE Name = @Name";

                connection.Execute(sql, new {Name = stackName});
            }
        }

        public static void Update(Stack stack)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sql = "UPDATE Stacks SET Name = @Name WHERE StackId = @StackId";

                connection.Execute(sql, stack);

                connection.Close();
            }
        }

        public static List<Stack> GetAll()
        {
            var stacksList = new List<Stack>() { };

            using (var connection =  new SqlConnection(connectionString))
            {
                connection.Open();

                var sql = "SELECT * FROM Stacks";

                stacksList = connection.Query<Stack>(sql).ToList();
            }

            return stacksList;
        }

        public static Stack? GetByName(string stackName)
        {
            Stack? stack = null;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sql = "SELECT * FROM Stacks WHERE Name = @Name";

                stack = connection.Query<Stack>(sql, new {Name = stackName}).FirstOrDefault();
            }

            return stack;
        }

        public static bool DoesStackWithSuchNameExist(string name)
        {
            var stacksList = GetAll();

            return stacksList.Select(s => s.Name).Contains(name);
        }
    }
}
