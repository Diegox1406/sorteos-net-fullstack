using pe.com.ciberelectrik.api.Models;
using pe.com.ciberelectrik.api.Models.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace pe.com.ciberelectrik.api.Models.repository
{
    public class TipoDocumentoRepository
    {
        private Conexion objconexion = new Conexion();
        private SqlCommand cmd;
        private SqlDataReader dr;
        private int res = 0;

        public List<TipoDocumento> findAll()
        {
            List<TipoDocumento> tiposDocumento = new List<TipoDocumento>();
            try
            {
                cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_MostrarTipoDocumento",
                    Connection = objconexion.Conectar()
                };
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tiposDocumento.Add(new TipoDocumento
                    {
                        codigo = Convert.ToInt32(dr["codtipd"]),
                        nombre = dr["nomtipd"].ToString(),
                        estado = Convert.ToBoolean(dr["esttipd"])
                    });
                }
                return tiposDocumento;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }


        public List<TipoDocumento> findAllCustom()
        {
            List<TipoDocumento> tiposDocumento = new List<TipoDocumento>();
            try
            {
                cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_MostrarTipoDocumentoTodo",
                    Connection = objconexion.Conectar()
                };
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tiposDocumento.Add(new TipoDocumento
                    {
                        codigo = Convert.ToInt32(dr["codtipd"]),
                        nombre = dr["nomtipd"].ToString(),
                        estado = Convert.ToBoolean(dr["esttipd"])
                    });
                }
                return tiposDocumento;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en findAllCustom TipoDocumento: " + ex.ToString());
                return null;
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }
        public bool add(TipoDocumento t)
        {
            try
            {
                cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_RegistrarTipoDocumento",
                    Connection = objconexion.Conectar()
                };
                cmd.Parameters.AddWithValue("@nombre", t.nombre);
                cmd.Parameters.AddWithValue("@estado", t.estado);
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

        public bool update(TipoDocumento t)
        {
            try
            {
                cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_ActualizarTipoDocumento",
                    Connection = objconexion.Conectar()
                };
                cmd.Parameters.AddWithValue("@codigo", t.codigo);
                cmd.Parameters.AddWithValue("@nombre", t.nombre);
                cmd.Parameters.AddWithValue("@estado", t.estado);
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

        public bool delete(TipoDocumento t)
        {
            try
            {
                cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_EliminarTipoDocumento",
                    Connection = objconexion.Conectar()
                };
                cmd.Parameters.AddWithValue("@codigo", t.codigo);
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
                    CommandText = "SP_CodigoTipoDocumento",
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

        public bool enable(TipoDocumento t)
        {
            try
            {
                cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_HabilitarTipoDocumento", // Asegúrate que tu SP se llama así
                    Connection = objconexion.Conectar()
                };
                cmd.Parameters.AddWithValue("@codigo", t.codigo);
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
    }
}
