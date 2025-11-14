using pe.com.ciberelectrik.api.Models.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace pe.com.ciberelectrik.api.Models.repository
{
    public class UsersRepository
    {
        private Conexion conexion = new Conexion();
        private SqlCommand cmd;
        private SqlDataReader dr;

        public List<Users> findAll()
        {
            List<Users> lista = new List<Users>();
            try
            {
                cmd = new SqlCommand("SP_MostrarUsers", conexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Users
                    {
                        Id = Convert.ToInt64(dr["id"]),
                        Name = dr["name"]?.ToString(),
                        Email = dr["email"]?.ToString(),
                        Username = dr["username"]?.ToString(),
                        Password = dr["password"]?.ToString(),
                        CreatedAt = dr["created_at"] as DateTime?,
                        UpdatedAt = dr["updated_at"] as DateTime?
                    });
                }
                return lista;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener usuarios: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public bool add(Users u)
        {
            try
            {
                cmd = new SqlCommand("SP_RegistrarUser", conexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@name", (object)u.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@email", u.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@username", u.Username ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@password", u.Password ?? (object)DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al registrar usuario: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public bool update(Users u)
        {
            try
            {
                cmd = new SqlCommand("SP_ActualizarUser", conexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@id", u.Id);
                cmd.Parameters.AddWithValue("@name", (object)u.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@email", u.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@username", u.Username ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@password", u.Password ?? (object)DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al actualizar usuario: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public bool delete(long id)
        {
            try
            {
                cmd = new SqlCommand("SP_EliminarUser", conexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al eliminar usuario: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
    }
}
