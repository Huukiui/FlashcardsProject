using FlashcardsProject.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Globalization;

namespace FlashcardsProject.Controllers
{
    public static class FlashCardsController
    {
        public static string connectionString = "Data Source=(localdb)\\LocalDB;Database=FlashCardsDB;Trusted_Connection=true";

        public static void Add(Flashcard card)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sql = "INSERT INTO Flashcards (Front, Back, StackId) VALUES (@Front, @Back, @StackId)";

                connection.Execute(sql, card);

                connection.Close();
            }
        }

        public static void Update(Flashcard card)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sql = "UPDATE Flashcards SET Front = @Front, Back = @Back WHERE CardId = @CardId";

                connection.Execute(sql, card);

                connection.Close();
            }
        }

        public static void Delete(int cardId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sql = "DELETE FROM Flashcards WHERE CardId = @CardId";

                connection.Execute(sql, new {CardId = cardId});

                connection.Close();
            }
        }

        public static List<Flashcard> GetAll()
        {
            var cardsList = new List<Flashcard>() { };

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sql = "SELECT * FROM Flashcards";

                cardsList = connection.Query<Flashcard>(sql).ToList();
            }

            return cardsList;
        }

        public static bool DoesCardExist(int cardId)
        {
            var cardsList = new List<Flashcard>() { };

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sql = "SELECT * FROM Flashcards WHERE CardId = @CardId";

                cardsList = connection.Query<Flashcard>(sql).ToList();
            }

            return cardsList.Count > 0;
        }

        public static List<Flashcard> GetByStackId(int stackId)
        {
            var cardsList = new List<Flashcard>() { };

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sql = "SELECT * FROM Flashcards WHERE StackId = @StackId";

                cardsList = connection.Query<Flashcard>(sql, new {StackId = stackId}).ToList();
            }

            return cardsList;
        }
    }
}
