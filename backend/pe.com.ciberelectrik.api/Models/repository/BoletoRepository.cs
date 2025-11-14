using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using pe.com.ciberelectrik.api.Models;
using pe.com.ciberelectrik.api.Models.db;

namespace pe.com.ciberelectrik.api.Models.repository
{
    public class BoletoRepository
    {
        private Conexion objconexion = new Conexion();
        private SqlCommand cmd;
        private SqlDataReader dr;
        int res = 0;

        public List<Boleto> findAll()
        {
            List<Boleto> boletos = new List<Boleto>();
            try
            {
                cmd = new SqlCommand("SP_MostrarBoletos", objconexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Boleto obj = new Boleto
                    {
                        IdBoleto = Convert.ToInt64(dr["idboleto"]),
                        CodigoBoleto = dr["boleto"].ToString(),
                        FechaApartado = dr["fechaApartado"] as DateTime?,
                        FechaCompra = dr["fechaCompra"] as DateTime?,
                        Comprado = Convert.ToInt64(dr["comprado"]),
                        CreatedAt = dr["created_at"] as DateTime?,
                        UpdatedAt = dr["updated_at"] as DateTime?
                    };
                    boletos.Add(obj);
                }
                return boletos;
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

        public Boleto findById(long id)
        {
            Boleto boleto = null;
            try
            {
                cmd = new SqlCommand("SP_BuscarBoletoPorId", objconexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@idboleto", id);

                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    boleto = new Boleto
                    {
                        IdBoleto = Convert.ToInt64(dr["idboleto"]),
                        CodigoBoleto = dr["boleto"].ToString(),
                        FechaApartado = dr["fechaApartado"] as DateTime?,
                        FechaCompra = dr["fechaCompra"] as DateTime?,
                        Comprado = Convert.ToInt64(dr["comprado"]),
                        CreatedAt = dr["created_at"] as DateTime?,
                        UpdatedAt = dr["updated_at"] as DateTime?
                    };
                }
                return boleto;
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

        public bool add(Boleto b)
        {
            try
            {
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_RegistrarBoleto";
                cmd.Connection = objconexion.Conectar();

                cmd.Parameters.AddWithValue("@boleto", b.CodigoBoleto);
                cmd.Parameters.AddWithValue("@fechaApartado", (object)b.FechaApartado ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@fechaCompra", (object)b.FechaCompra ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@comprado", b.Comprado);

                int res = cmd.ExecuteNonQuery();
                return res == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en add boleto: " + ex.Message);
                throw;
            }
            finally
            {
                objconexion.CerrarConexion();
            }
        }

        public bool update(Boleto b)
        {
            try
            {
                cmd = new SqlCommand("SP_ActualizarBoleto", objconexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@idboleto", b.IdBoleto);
                cmd.Parameters.AddWithValue("@boleto", b.CodigoBoleto);
                cmd.Parameters.AddWithValue("@fechaApartado", (object)b.FechaApartado ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@fechaCompra", (object)b.FechaCompra ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@comprado", b.Comprado);

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

        public bool delete(long id)
        {
            try
            {
                cmd = new SqlCommand("SP_EliminarBoleto", objconexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@idboleto", id);
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

        public bool enable(long id)
        {
            try
            {
                cmd = new SqlCommand("SP_HabilitarBoleto", objconexion.Conectar())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@idboleto", id);
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