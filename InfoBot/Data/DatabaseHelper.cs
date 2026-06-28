using Microsoft.Data.Sqlite;
using InfoBot.Models;
using System;

namespace InfoBot.Data
{
    public class DatabaseHelper
    {
        private const string connectionString = "Data Source=InfoBot.db";

        public DatabaseHelper()
        {
            using var conexion = new SqliteConnection(connectionString);
            conexion.Open();
            string sql = @"CREATE TABLE IF NOT EXISTS InfoRecaudada (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Titulo TEXT,
    Resumen TEXT,
    Url TEXT,
    Fuente TEXT,
    FechaConsulta TEXT
)";

            using var cmd = new SqliteCommand(sql, conexion);
            cmd.ExecuteNonQuery();
        }

        public void Guardar(InfoRecord record)
        {
            using var conexion = new SqliteConnection(connectionString);
            conexion.Open();
            string sql = "INSERT INTO InfoRecaudada (Titulo, Resumen, Url, Fuente, FechaConsulta) VALUES (@titulo, @resumen, @url, @fuente, @fechaConsulta)";
            using var cmd = new SqliteCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@titulo", record.Titulo ?? "");
            cmd.Parameters.AddWithValue("@resumen", record.Resumen ?? "");
            cmd.Parameters.AddWithValue("@url", record.Url ?? "");
            cmd.Parameters.AddWithValue("@fuente", record.Fuente ?? "");
            cmd.Parameters.AddWithValue("@fechaConsulta", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            cmd.ExecuteNonQuery();
        }

        public void MostrarRegistros()
        {
            using var conexion = new SqliteConnection(connectionString);
            conexion.Open();
            string sql = "SELECT Id, Titulo, Resumen, Url, Fuente, FechaConsulta FROM InfoRecaudada ORDER BY FechaConsulta DESC";
            using var cmd = new SqliteCommand(sql, conexion);
            using var reader = cmd.ExecuteReader();

            Console.WriteLine("\n=== Registros guardados ===");
            while (reader.Read())
            {
                Console.WriteLine($"[{reader["Id"]}] {reader["Titulo"]} ({reader["Fuente"]}) - {reader["FechaConsulta"]}");
                Console.WriteLine($"Resumen: {reader["Resumen"]}\n");
            }
        }

        public void Eliminar(int id)
        {
            using var conexion = new SqliteConnection(connectionString);
            conexion.Open();
            string sql = "DELETE FROM InfoRecaudada WHERE Id = @id";
            using var cmd = new SqliteCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@id", id);
            int filas = cmd.ExecuteNonQuery();
            Console.WriteLine(filas > 0 ? "Registro eliminado." : "No se encontró el registro.");
        }
    }
}
