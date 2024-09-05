using FlashcardsProject.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FlashcardsProject.Controllers
{
    public static class StudySessionsController
    {
        public static string connectionString = "Data Source=(localdb)\\LocalDB;Database=FlashCardsDB;Trusted_Connection=true";

        public static void Add(StudySession session)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sql = "INSERT INTO StudySessions (Date, Score, StackId) VALUES (@Date, @Score, @StackId)";

                connection.Execute(sql, session);

                connection.Close();
            }
        }
    }
}
