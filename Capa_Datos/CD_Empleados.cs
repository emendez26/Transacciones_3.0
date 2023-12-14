﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Objetos;

namespace Capa_Datos
{
    public class CD_Empleados
    {
        static int Epicor10Live = 1;
        static int DB_TecnoFuego = 2;

        CO_Empleados empleado = new CO_Empleados();
        List<CO_Empleados> empleados = new List<CO_Empleados>();

        private Conexion conexion = new Conexion();

        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();

        public List<CO_Empleados> Read()
        {
            try
            {
                conexion.iniciarBD(DB_TecnoFuego);
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "sp_Read_Empleado";
                comando.CommandType = CommandType.StoredProcedure;
                leer = comando.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(leer);
                foreach (DataRow dr in table.Rows)
                {
                    empleado.id = int.Parse(dr[0].ToString());
                    empleado.identificacion = dr[1].ToString();
                    empleado.departamento = dr[2].ToString();
                    empleado.area = dr[3].ToString();
                    empleado.activo = bool.Parse(dr[4].ToString());
                    empleado.ubicacion = dr[5].ToString();
                    empleado.nombre = dr[6].ToString();
                    empleado.apellido = dr[7].ToString();

                    empleados.Add(empleado);
                    empleado = new CO_Empleados();
                }
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return empleados;
        }
        public int Insert(CO_Empleados empleado)
        {
            int exitoso = 0;
            //PROCEDIMNIENTO
            try
            {
                conexion.iniciarBD(DB_TecnoFuego);
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "sp_Insert_Empleado";
                comando.Parameters.AddWithValue("@identificacion", empleado.identificacion);
                comando.Parameters.AddWithValue("@departamento", empleado.departamento);
                comando.Parameters.AddWithValue("@area", empleado.area);
                comando.Parameters.AddWithValue("@activo", empleado.activo);
                comando.Parameters.AddWithValue("@ubicacion", empleado.ubicacion);
                comando.Parameters.AddWithValue("nombre", empleado.nombre);
                comando.Parameters.AddWithValue("apellido", empleado.apellido);

                exitoso = comando.ExecuteNonQuery();

                comando.Parameters.Clear();
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Message error : " + ex, "Error al guardar");
            }

            return exitoso;
        }

        public int Update(int id, CO_Empleados empleado)
        {
            int exitoso = 0;
            try
            {

                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "sp_Update_Empleado";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@identificacion", empleado.identificacion);
                comando.Parameters.AddWithValue("@departamento", empleado.departamento);
                comando.Parameters.AddWithValue("@area", empleado.area);
                comando.Parameters.AddWithValue("@activo", empleado.activo);
                comando.Parameters.AddWithValue("@ubicacion", empleado.ubicacion);
                comando.Parameters.AddWithValue("nombre", empleado.nombre);
                comando.Parameters.AddWithValue("apellido", empleado.apellido);

                exitoso = comando.ExecuteNonQuery();

                comando.Parameters.Clear();
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Message error : " + ex, "Error al guardar");
            }

            return exitoso;
        }

        public int Delete(int id)
        {
            int result = 0;
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "sp_Delete_Empleado";
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@id_Empleado", id);

                result = comando.ExecuteNonQuery();

                comando.Parameters.Clear();
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Message error : " + ex, "Error al Editar");
            }


            return result;
        }
    }


}
