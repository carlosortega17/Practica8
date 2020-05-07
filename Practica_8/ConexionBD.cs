using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//libreria para usar sql 
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Data.Sql;


namespace Practica_8
{
    class ConexionBD
    {
        //Creacion de conexion
        SqlConnection ConertarBD;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        private void Conectar()
        {
            if (ConertarBD != null)
            {
                ConertarBD = null;
            }
            //se conectar ala base de datos
            ConertarBD = new SqlConnection("Data Source=DESKTOP-AQ0I65S\\SQLEXPRESS;Initial Catalog=PracticaTAP;Integrated Security=True");
            //se abre la base de datos
            ConertarBD.Open();
        }
        //Incertar datos en la base de datos
        public string Insertar(string Nombre, string Apellido, string FechaNacimiento)
        {
            string salida = "Valor ingresado";
            try
            {
                Conectar();
                cmd = new SqlCommand("insert into dbo.Persona (Nombre, Apellido, FechaNacimiento) values('" + Nombre + "','" + Apellido + "','" + FechaNacimiento + "')",ConertarBD);
                cmd.ExecuteNonQuery();
                MessageBox.Show(" Datos ingresados ");
            }
            catch (Exception ex)
            {

                salida = "No se Pudo insertar el valor " + ex.ToString();
            }
            ConertarBD.Close();
            return salida;      
        }

        
        //validacion para ver que no se repitan los datos
        public int Registros(int Id)
        {
            //tipo de dato que retornamoss
            int contador = 0;
            try
            {
                Conectar();
                //seleciona todos los datos de la tabla que este el id
                cmd = new SqlCommand("Select * from dbo.Persona where Id=" + Id + "", ConertarBD);
                //nos regresa el numero de personas que tenga un identificador 
                dr = cmd.ExecuteReader();
                //recorre el numero de veces que obtierne el registrO
                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();
                ConertarBD.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se Encontro Consulta" + ex.ToString());
            }
            return contador;
        }
        public void cargarPersonas(DataGridView DgvPersonas)
        {
            try
            {
                Conectar();
                da = new SqlDataAdapter("Select * from dbo.Persona", ConertarBD);
                dt = new DataTable();
                da.Fill(dt);
                DgvPersonas.DataSource = dt;
                //Para auto ajustar los cuadros o celdas
                //DgvPersonas.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                ConertarBD.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo llenar el Datagridview: " + ex.ToString());
            }
        }


        public void llenarTexBoxConsulta(int id, TextBox txtNombre, TextBox txtApellidos, DateTimePicker dtpFecha)
        {
            try
            {
                Conectar();
                cmd = new SqlCommand("Select * from dbo.Persona where Id=" + id + "", ConertarBD);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtNombre.Text = dr["Nombre"].ToString();
                    txtApellidos.Text = dr["Apellidos"].ToString();
                    dtpFecha.Text = dr["FechaNacimiento"].ToString();

                }
                dr.Close();
                ConertarBD.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo llenar los campos: " + ex.ToString());
            }
        }

        public string atualizar(int Id, string Nombre, string Apellido, string FechaNacimiento)
        {
            string salida = "Se actualizaron los datos";
            try
            {
                Conectar();
                cmd = new SqlCommand("Update dbo.Persona set Nombre ='" + Nombre + "' , Apellidos='" + Apellido + "', FechaNacimiento='" + FechaNacimiento + "' where Id=" + Id + "", ConertarBD);
                cmd.ExecuteNonQuery();
                MessageBox.Show(" Datos Actualizados ");
                ConertarBD.Close();
            }
            catch (Exception ex)
            {
                salida = "No se actualizo: " + ex.ToString();
            }
            return salida;
        }
        public string Eliminar(int Id)
        {
            string eliminar = "Se Eliminaron los Datos Correctamente";
            try
            {
                Conectar();
                cmd = new SqlCommand("DELETE from dbo.Persona where Id =" + Id + "", ConertarBD);
                cmd.ExecuteNonQuery();
                MessageBox.Show(" Datos Actualizados ");
                ConertarBD.Close();
            }
            catch (Exception ex)
            {
                eliminar = " Erro al Eliminar datos " + ex.ToString();
            }
            return eliminar;
        }


    }
}
