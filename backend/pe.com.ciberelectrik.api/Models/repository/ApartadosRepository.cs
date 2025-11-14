using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;  // <-- para Debug.WriteLine
using pe.com.ciberelectrik.api.Models;
using pe.com.ciberelectrik.api.Models.db;

namespace pe.com.ciberelectrik.api.Models.repository
{
    public class ApartadosRepository
    {
        private Conexion objconexion = new Conexion();
        private SqlCommand cmd;
        private SqlDataReader dr;
        int res = 0;

        public List<Apartados> findAll()
        {
            List<Apartados> apartados = new List<Apartados>();
            try
            {
                cmd = new SqlCommand("SP_MostrarApartados", objconexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Apartados obj = new Apartados
                    {
                        IdApartados = Convert.ToInt64(dr["idapartados"]),
                        Boleto = Convert.ToInt32(dr["boleto"]),
                        FechaApartados = dr["fechaapartados"] as DateTime?,
                        BoletoId = dr["boleto_id"] != DBNull.Value ? Convert.ToInt64(dr["boleto_id"]) : (long?)null,
                        CreatedAt = dr["created_at"] as DateTime?,
                        UpdatedAt = dr["updated_at"] as DateTime?
                    };
                    apartados.Add(obj);
                }
                return apartados;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error en findAll apartados: " + ex.Message);
                return null;
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }

        public Apartados findById(long id)
        {
            Apartados apartado = null;
            try
            {
                cmd = new SqlCommand("SP_BuscarApartadoPorId", objconexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@idapartados", id);

                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    apartado = new Apartados
                    {
                        IdApartados = Convert.ToInt64(dr["idapartados"]),
                        Boleto = Convert.ToInt32(dr["boleto"]),
                        FechaApartados = dr["fechaapartados"] as DateTime?,
                        BoletoId = dr["boleto_id"] != DBNull.Value ? Convert.ToInt64(dr["boleto_id"]) : (long?)null,
                        CreatedAt = dr["created_at"] as DateTime?,
                        UpdatedAt = dr["updated_at"] as DateTime?
                    };
                }
                return apartado;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error en findById apartados: " + ex.Message);
                return null;
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }

        public (bool Success, string Message) add(Apartados a)
        {
            try
            {
                cmd = new SqlCommand("SP_RegistrarApartado", objconexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@boleto", a.Boleto);
                cmd.Parameters.AddWithValue("@fechaapartados", a.FechaApartados.Value);
                cmd.Parameters.AddWithValue("@boleto_id", a.BoletoId ?? (object)DBNull.Value);

                SqlParameter resultado = new SqlParameter("@resultado", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(resultado);

                cmd.ExecuteNonQuery();

                int res = (int)resultado.Value;
                switch (res)
                {
                    case 1:
                        return (true, "Apartado registrado correctamente.");
                    case -1:
                        return (false, "El boleto ya está apartado.");
                    default:
                        return (false, "Ocurrió un error al registrar el apartado.");
                }
            }
            catch (SqlException ex)
            {
                return (false, $"Error SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}");
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }

        public bool update(Apartados a)
        {
            try
            {
                cmd = new SqlCommand("SP_ActualizarApartado", objconexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@idapartados", a.IdApartados);
                cmd.Parameters.AddWithValue("@boleto", a.Boleto);
                cmd.Parameters.AddWithValue("@fechaapartados", a.FechaApartados ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@boleto_id", a.BoletoId ?? (object)DBNull.Value);

                res = cmd.ExecuteNonQuery();
                return res == 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error en update apartado: " + ex.Message);
                return false;
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }

        public bool delete(long id)
        {
            try
            {
                cmd = new SqlCommand("SP_EliminarApartado", objconexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@idapartados", id);
                res = cmd.ExecuteNonQuery();
                return res == 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error en delete apartado: " + ex.Message);
                return false;
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }
    }
}