using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using RetornoCadenaDeConexion;
using System.Data.Odbc;
using System.Collections;

namespace datos
{
    public class capaDatosBitacora
    {


        //GET CURRENTE HOST
        private string getCurrentHost()
        {

            CadenaDeConexion cdc = new CadenaDeConexion();
            OdbcDataReader dr = null;
            string host = "";

            try
            {
                using (var conn = new OdbcConnection(cdc.Conexion()))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT SUBSTRING_INDEX(USER(), '@', -1) AS HOST,  @@hostname as hostname, @@port as port, DATABASE() as current_database;";
                        dr = cmd.ExecuteReader();
                        dr.Read();

                        host = dr["HOST"].ToString();
                        dr.Close();
                        conn.Close();

                        return host;
                    }

                }
            }
            catch (Exception ex)
            {

                return host;
            }
    }

        //GET TIME
        private string getTime()
        {
 

            CadenaDeConexion cdc = new CadenaDeConexion();
            OdbcDataReader dr = null;
            string hora = "";

            try
            {
                using (var conn = new OdbcConnection(cdc.Conexion()))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT time (NOW()) as HORA;";
                        dr = cmd.ExecuteReader();
                        dr.Read();

                        hora = dr["HORA"].ToString();
                        dr.Close();
                        conn.Close();

                        return hora;
                    }

                }
            }
            catch (Exception ex)
            {

                return hora;
            }



        }

        //GET DATE
        private string getDate()
        {


            CadenaDeConexion cdc = new CadenaDeConexion();
            OdbcDataReader dr = null;
            string fecha = "";

            try
            {
                using (var conn = new OdbcConnection(cdc.Conexion()))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT DATE_FORMAT(NOW(), \"%Y-%m-%d\" ) AS FECHA;";
                        dr = cmd.ExecuteReader();
                        dr.Read();

                        fecha = dr["FECHA"].ToString();
                        dr.Close();
                        conn.Close();

                        return fecha;
                    }

                }
            }
            catch (Exception ex)
            {

                return fecha;
            }




        }




        //ACCION FROM CAPA_LOGICA
        public bool setBitacora(string _accion)
        {

            string host = "";
            string time = "";
            string date = "";

            //HOST
            host = getCurrentHost();

            //TIME

            time = getTime();


            //DATE
            date = getDate();

            //ENVIO DE PARAMETROS A INSERT ACCION
            if (insertBitacora(1, host, _accion, time, date))
            {
                return true;
            }

            return false;

        }




        //INSERT ACCION (BITACORA)
        private bool insertBitacora(int usu_cod, string host, string accion, string hora, string fecha)
        {
            

            CadenaDeConexion cdc = new CadenaDeConexion();
     
            try
            {
                using (var conn = new OdbcConnection(cdc.Conexion()))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO bitacora VALUES('','" + usu_cod + "','" + host + "','" + accion + "','" + hora + "','" + fecha + "')";
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;

                    }

                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        //MOSTRAR BITACORA

        public DataTable showBitacora()
        {

            CadenaDeConexion cdc = new CadenaDeConexion();
            OdbcDataReader dr = null;
            DataTable dt = new DataTable();

            try
            {
                using (var conn = new OdbcConnection(cdc.Conexion()))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM bitacora";
                        dr = cmd.ExecuteReader();

                    
                        dt.Columns.Add("Codigo de usuario");
                        dt.Columns.Add("Host");
                        dt.Columns.Add("Accion");
                        dt.Columns.Add("Hora");
                        dt.Columns.Add("Fecha");


                        while (dr.Read())
                        {
                            DataRow row = dt.NewRow();
                            row["Codigo de usuario"] = dr["usuario_codigo"];
                            row["Host"] = dr["host"];
                            row["Accion"] = dr["acccion"];
                            row["Hora"] = dr["hora"];
                            row["Fecha"] = dr["fecha"];
                            dt.Rows.Add(row);

                        }

                        dr.Close();

                        return dt;

                    }

                }
            }
            catch (Exception ex)
            {
                return dt;
            }



        }

    }
}

