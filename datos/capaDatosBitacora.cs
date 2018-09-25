using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

namespace datos
{
    public class capaDatosBitacora
    {
        MySqlConnection conn = new MySqlConnection();


        //CONEXION CON LA BASE DE DATOS
        private MySqlConnection conectar()
        {
            string conexionString = "server=localhost; userid=root;password='';database=seguridad_bd;SslMode=none";
            MySqlConnection conexionDB = new MySqlConnection(conexionString);

            try
            {
                conexionDB.Open();
                return conexionDB;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        //GET CURRENTE HOST
        private MySqlDataReader getCurrentHost()
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr = null;

            try
            {
                cmd.Connection = conectar();
                cmd.CommandText = "SELECT SUBSTRING_INDEX(USER(), '@', -1) AS HOST,  @@hostname as hostname, @@port as port, DATABASE() as current_database;";
                dr = cmd.ExecuteReader();
                conectar().Close();
                return dr;

            }
            catch (Exception ex)
            {
                return dr;
            }

        }

        //GET TIME
        private MySqlDataReader getTime()
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr = null;


            try
            {
                cmd.Connection = conectar();
                cmd.CommandText = "SELECT time (NOW()) as HORA;";
                dr = cmd.ExecuteReader();
                conectar().Close();
                return dr;

            }
            catch (Exception ex)
            {
                return dr;
            }

        }

        //GET DATE
        private MySqlDataReader getDate()
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr = null;


            try
            {
                cmd.Connection = conectar();
                cmd.CommandText = "SELECT DATE_FORMAT(NOW(), \"%Y-%m-%d\" ) AS FECHA;";
                dr = cmd.ExecuteReader();
                conectar().Close();
                return dr;

            }
            catch (Exception ex)
            {
                return dr;
            }

        }




        //ACCION FROM CAPA_LOGICA
        public bool setBitacora(string _accion)
        {

            string host = "";
            string time = "";
            string date = "";

            //HOST
            try
            {
                MySqlDataReader rdHost;
                rdHost = getCurrentHost();
                rdHost.Read();

                host = rdHost.GetString("HOST");
            }
            catch (Exception ex)
            {

            }

            //TIME
            try
            {
                MySqlDataReader rdTime;
                rdTime = getTime();
                rdTime.Read();

                time = rdTime.GetString("HORA");
            }
            catch (Exception ex)
            {

            }

            //DATE
            try
            {
                MySqlDataReader rdDate;
                rdDate = getDate();
                rdDate.Read();

                date = rdDate.GetString("FECHA");
            }
            catch (Exception ex)
            {

            }

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
            MySqlCommand cmd = new MySqlCommand();



            try
            {
                cmd.Connection = conectar();
                cmd.CommandText = "INSERT INTO bitacora VALUES('','" + usu_cod + "','" + host + "','" + accion + "','" + hora + "','" + fecha + "')";
                cmd.ExecuteNonQuery();
                conectar().Close();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }


        //MOSTRAR BITACORA

        public DataTable showBitacora()
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr = null;

            try
            {
                cmd.Connection = conectar();
                cmd.CommandText = "SELECT * FROM bitacora";
                dr = cmd.ExecuteReader();

                DataTable dt = new DataTable();
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


                return dt;


            }
            catch (Exception ex)
            {
                return null;
            }
        }



    }
}

