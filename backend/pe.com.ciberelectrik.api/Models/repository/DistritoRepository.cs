using pe.com.ciberelectrik.api.Models;
using pe.com.ciberelectrik.api.Models.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace pe.com.ciberelectrik.api.Models.repository
{
    public class DistritoRepository
    {
        private Conexion objconexion = new Conexion();
        private SqlCommand cmd;
        private SqlDataReader dr;
        private int res = 0;

        // ✅ Este método ahora usa el SP que devuelve correctamente los datos
        public List<Distrito> findAll()
        {
            List<Distrito> distritos = new List<Distrito>();
            try
            {
                cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_MostrarDistritoTodo", // Cambiado aquí
                    Connection = objconexion.Conectar()
                };
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    distritos.Add(new Distrito
                    {
                        codigo = Convert.ToInt32(dr["coddis"]),
                        nombre = dr["nomdis"].ToString(),
                        estado = Convert.ToBoolean(dr["estdis"])
                    });
                }
                return distritos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en findAll: " + ex.Message);
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }

        public List<Distrito> findAllCustom()
        {
            List<Distrito> distritos = new List<Distrito>();
            try
            {
                cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_MostrarDistritoTodo",
                    Connection = objconexion.Conectar()
                };
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    distritos.Add(new Distrito
                    {
                        codigo = Convert.ToInt32(dr["coddis"]),
                        nombre = dr["nomdis"].ToString(),
                        estado = Convert.ToBoolean(dr["estdis"])
                    });
                }
                return distritos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en findAllCustom: " + ex.Message);
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }

        public bool add(Distrito d)
        {
            try
            {
                cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_RegistrarDistrito",
                    Connection = objconexion.Conectar()
                };
                cmd.Parameters.AddWithValue("@nombre", d.nombre);
                cmd.Parameters.AddWithValue("@estado", d.estado);
                res = cmd.ExecuteNonQuery();
                return res == 1;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }

        public bool update(Distrito d)
        {
            try
            {
                cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_ActualizarDistrito",
                    Connection = objconexion.Conectar()
                };
                cmd.Parameters.AddWithValue("@codigo", d.codigo);
                cmd.Parameters.AddWithValue("@nombre", d.nombre);
                cmd.Parameters.AddWithValue("@estado", d.estado);
                res = cmd.ExecuteNonQuery();
                return res == 1;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }

        public bool delete(Distrito d)
        {
            try
            {
                cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_EliminarDistrito",
                    Connection = objconexion.Conectar()
                };
                cmd.Parameters.AddWithValue("@codigo", d.codigo);
                res = cmd.ExecuteNonQuery();
                return res == 1;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }

        public bool enable(Distrito d)
        {
            try
            {
                cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_HabilitarDistrito",
                    Connection = objconexion.Conectar()
                };
                cmd.Parameters.AddWithValue("@codigo", d.codigo);
                res = cmd.ExecuteNonQuery();
                return res == 1;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }

        public int setCode()
        {
            try
            {
                cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_CodigoDistrito",
                    Connection = objconexion.Conectar()
                };
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }
    }
}