using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CalculadoraWebAPI.Models;

namespace CalculadoraWebAPI.Services
{

    public class CalculoService
    {
        private readonly string _connectionString;

        public CalculoService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Calculo> ObtenerTodosLosCalculos()
        {
            var calculos = new List<Calculo>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT Id, Valor1, Valor2, Operacion, Resultado, Fecha FROM Calculos ORDER BY Fecha DESC";
                
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        calculos.Add(new Calculo
                        {
                            Id = reader.GetInt32(0),
                            Valor1 = reader.GetDouble(1),
                            Valor2 = reader.GetDouble(2),
                            Operacion = reader.GetString(3),
                            Resultado = reader.GetDouble(4),
                            Fecha = reader.GetDateTime(5)
                        });
                    }
                }
            }

            return calculos;
        }


        public List<Calculo> ObtenerSumas()
        {
            return ObtenerCalculosPorOperacion("+");
        }


        public List<Calculo> ObtenerRestas()
        {
            return ObtenerCalculosPorOperacion("-");
        }


        public List<Calculo> ObtenerMultiplicaciones()
        {
            return ObtenerCalculosPorOperacion("x");
        }


        public List<Calculo> ObtenerDivisiones()
        {
            return ObtenerCalculosPorOperacion("/");
        }


        public List<Calculo> ObtenerPotencias()
        {
            return ObtenerCalculosPorOperacion("^");
        }


        public List<Calculo> ObtenerRaices()
        {
            return ObtenerCalculosPorOperacion("√");
        }


        public List<Calculo> ObtenerCalculosRecientes(int dias = 7)
        {
            var calculos = new List<Calculo>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"SELECT Id, Valor1, Valor2, Operacion, Resultado, Fecha 
                                FROM Calculos 
                                WHERE Fecha >= DATEADD(day, -@dias, GETDATE())
                                ORDER BY Fecha DESC";
                
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@dias", dias);
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            calculos.Add(new Calculo
                            {
                                Id = reader.GetInt32(0),
                                Valor1 = reader.GetDouble(1),
                                Valor2 = reader.GetDouble(2),
                                Operacion = reader.GetString(3),
                                Resultado = reader.GetDouble(4),
                                Fecha = reader.GetDateTime(5)
                            });
                        }
                    }
                }
            }

            return calculos;
        }


        public Calculo GuardarCalculo(double valor1, double valor2, string operacion, double resultado)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Calculos (Valor1, Valor2, Operacion, Resultado, Fecha) 
                                VALUES (@Valor1, @Valor2, @Operacion, @Resultado, GETDATE());
                                SELECT CAST(SCOPE_IDENTITY() as int);";
                
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Valor1", valor1);
                    cmd.Parameters.AddWithValue("@Valor2", valor2);
                    cmd.Parameters.AddWithValue("@Operacion", operacion);
                    cmd.Parameters.AddWithValue("@Resultado", resultado);
                    
                    int nuevoId = (int)cmd.ExecuteScalar();
                    
                    return new Calculo
                    {
                        Id = nuevoId,
                        Valor1 = valor1,
                        Valor2 = valor2,
                        Operacion = operacion,
                        Resultado = resultado,
                        Fecha = DateTime.Now
                    };
                }
            }
        }


        public Dictionary<string, int> ObtenerEstadisticas()
        {
            var estadisticas = new Dictionary<string, int>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"SELECT Operacion, COUNT(*) as Cantidad 
                                FROM Calculos 
                                GROUP BY Operacion";
                
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        estadisticas[reader.GetString(0)] = reader.GetInt32(1);
                    }
                }
            }

            return estadisticas;
        }


        private List<Calculo> ObtenerCalculosPorOperacion(string operacion)
        {
            var calculos = new List<Calculo>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"SELECT Id, Valor1, Valor2, Operacion, Resultado, Fecha 
                                FROM Calculos 
                                WHERE Operacion = @Operacion 
                                ORDER BY Fecha DESC";
                
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Operacion", operacion);
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            calculos.Add(new Calculo
                            {
                                Id = reader.GetInt32(0),
                                Valor1 = reader.GetDouble(1),
                                Valor2 = reader.GetDouble(2),
                                Operacion = reader.GetString(3),
                                Resultado = reader.GetDouble(4),
                                Fecha = reader.GetDateTime(5)
                            });
                        }
                    }
                }
            }

            return calculos;
        }
    }
}
