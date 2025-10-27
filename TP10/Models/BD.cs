namespace TP10.Models;
using Microsoft.Data.SqlClient;
using Dapper;

public static class BD
{
    private static string _connectionString = @"Server=localhost; 
   DataBase = TP10Hamu_Perel_Abas; Integrated Security=True; TrustServerCertificate=True;";
    public static List<Categorias> ObtenerCategorias()
    {
        List<Categorias> categorias = new List<Categorias>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Categorias";
             categorias = connection.Query<Categorias>(query).ToList();

        }
        return categorias;
    }
    public static List<Preguntas> ObtenerPreguntas(int categoria)
    {
      List<Preguntas> pregunta = new List<Preguntas>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Preguntas WHERE IdCategoria = @pcategoria";
            pregunta = connection.Query<Preguntas>(query, new { pcategoria = categoria }).ToList();
        }
        return pregunta;
    }
     public static List<Respuestas> ObtenerRespuestas(int IdPregunta)
    {
      List<Respuestas> respuesta = new List<Respuestas>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Respuesta WHERE IdPregunta = @ppregunta";
            respuesta = connection.Query<Respuestas>(query, new { ppregunta = IdPregunta }).ToList();

        }
        return respuesta;
    }

}