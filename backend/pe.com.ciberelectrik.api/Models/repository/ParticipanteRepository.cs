using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using pe.com.ciberelectrik.api.Models;
using pe.com.ciberelectrik.api.Models.db;

namespace pe.com.ciberelectrik.api.Models.repository
{
    public class ParticipanteRepository
    {
        private Conexion objconexion = new Conexion();
        private SqlCommand cmd;
        private SqlDataReader dr;
        int res = 0;

        public List<Participante> findAll()
        {
            List<Participante> participantes = new List<Participante>();
            try
            {
                cmd = new SqlCommand("SP_MostrarParticipante", objconexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Participante obj = new Participante
                    {
                        IdParticipante = Convert.ToInt64(dr["idparticipante"]),
                        Nombre = dr["nombre"].ToString(),
                        Telefono = dr["telefono"].ToString(),
                        UserId = dr["user_id"] != DBNull.Value ? Convert.ToInt64(dr["user_id"]) : (long?)null,
                        BoletoId = dr["boleto_id"] != DBNull.Value ? Convert.ToInt64(dr["boleto_id"]) : (long?)null,
                        CreatedAt = dr["created_at"] as DateTime?,
                        UpdatedAt = dr["updated_at"] as DateTime?
                    };
                    participantes.Add(obj);
                }
                return participantes;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error en findAll participantes: " + ex.Message);
                return null;
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }

        public Participante findById(long id)
        {
            Participante participante = null;
            try
            {
                cmd = new SqlCommand("SP_BuscarParticipantePorId", objconexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@idparticipante", id);

                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    participante = new Participante
                    {
                        IdParticipante = Convert.ToInt64(dr["idparticipante"]),
                        Nombre = dr["nombre"].ToString(),
                        Telefono = dr["telefono"].ToString(),
                        UserId = dr["user_id"] != DBNull.Value ? Convert.ToInt64(dr["user_id"]) : (long?)null,
                        BoletoId = dr["boleto_id"] != DBNull.Value ? Convert.ToInt64(dr["boleto_id"]) : (long?)null,
                        CreatedAt = dr["created_at"] as DateTime?,
                        UpdatedAt = dr["updated_at"] as DateTime?
                    };
                }
                return participante;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error en findById participante: " + ex.Message);
                return null;
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }

        public (bool Success, string Message) add(Participante p)
        {
            if (p == null)
                return (false, "El objeto Participante es nulo.");

            if (string.IsNullOrWhiteSpace(p.Nombre) || string.IsNullOrWhiteSpace(p.Telefono))
                return (false, "Nombre y Teléfono son campos obligatorios.");

            try
            {
                cmd = new SqlCommand("SP_RegistrarParticipante", objconexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@nombre", p.Nombre);
                cmd.Parameters.AddWithValue("@telefono", p.Telefono);
                cmd.Parameters.AddWithValue("@user_id", p.UserId.HasValue ? (object)p.UserId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@boleto_id", p.BoletoId.HasValue ? (object)p.BoletoId.Value : DBNull.Value);

                res = cmd.ExecuteNonQuery();
                Debug.WriteLine($"Filas afectadas por SP_RegistrarParticipante: {res}");

                if (res >= 1)
                    return (true, "Participante registrado correctamente.");
                else
                    return (false, "No se pudo registrar el participante.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error en add participante: " + ex.Message);
                return (false, "Error al registrar participante.");
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }

        public bool update(Participante p)
        {
            try
            {
                cmd = new SqlCommand("SP_ActualizarParticipante", objconexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@idparticipante", p.IdParticipante);
                cmd.Parameters.AddWithValue("@nombre", p.Nombre);
                cmd.Parameters.AddWithValue("@telefono", p.Telefono);
                cmd.Parameters.AddWithValue("@user_id", p.UserId.HasValue ? (object)p.UserId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@boleto_id", p.BoletoId.HasValue ? (object)p.BoletoId.Value : DBNull.Value);

                res = cmd.ExecuteNonQuery();
                return res == 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error en update participante: " + ex.Message);
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
                cmd = new SqlCommand("SP_EliminarParticipante", objconexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@idparticipante", id);
                res = cmd.ExecuteNonQuery();
                return res == 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error en delete participante: " + ex.Message);
                return false;
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }
    }
}