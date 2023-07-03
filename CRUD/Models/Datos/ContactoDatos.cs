using CRUD.Models;
using System.Data.SqlClient;    /////(1) Importar estas librerias
using System.Data;
using CRUD.Models.Datos;

namespace CRUD.Models.Datos
{
    public class ContactoDatos
    {
       

        public String _VerificarConexion()
        {
            /**
             * Metodo para devolver datos y probar la conexion a la DB.
             */
            String _Verificar = "";
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                var cmd = new SqlCommand("sp_listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        _Verificar = dr["nombre"].ToString() + " " + dr["apellido"].ToString();
                    }
                }

            }
            return _Verificar;
        }
        public List<ContactosModel> Listar() /// (2) Crear una lista del Modelo de contactos
        {
            var o_lista = new List<ContactosModel>(); // (3) se define el objeto que contendra la lista
            var cn = new Conexion(); // (4) se crea una instancia de la conexion.
            using (var conexion = new SqlConnection(cn.getCadenaSQL())) // (5) se importa la cadena de conexion desde cn
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        o_lista.Add(new ContactosModel() {
                            Id          = Convert.ToInt32(dr["id"]),
                            Nombre      = dr["nombre"].ToString(),
                            Apellido    = dr["apellido"].ToString(),
                            Cedula      = dr["cedula"].ToString(),
                            Telefono    = dr["telefono"].ToString(),
                            Correo      = dr["correo"].ToString(),
                            Direccion   = dr["direccion"].ToString(),
                        });;
                    }
                }
            }
            return o_lista;
        }

        public ContactosModel Obterner(int IdContacto)
        {
            var o_contacto = new ContactosModel();

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_obtener", conexion);
                cmd.Parameters.AddWithValue("id", IdContacto);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader()) {
                    while (dr.Read())
                    {
                        o_contacto.Id        = Convert.ToInt32(dr["id"]);
                        o_contacto.Nombre    = dr["nombre"].ToString();
                        o_contacto.Apellido  = dr["apellido"].ToString();
                        o_contacto.Cedula    = dr["cedula"].ToString();
                        o_contacto.Telefono  = dr["telefono"].ToString();
                        o_contacto.Correo    = dr["correo"].ToString();
                        o_contacto.Direccion = dr["direccion"].ToString();
                    }
                }
            }

                return o_contacto;
        }

        public Boolean Eliminar(ContactosModel ocm)
        {
            Boolean rp;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_eliminar @id", conexion);
                    cmd.Parameters.AddWithValue("@id", ocm.Id);
                    cmd.ExecuteNonQuery();
                    rp = true;
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                rp = false;
            }
            return rp;
        }

        public Boolean Editar(ContactosModel ocm)
        {
            Boolean rp;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_editar @nombre,@apellido,@cedula,@telefono,@correo,@direccion,@id", conexion);
                    cmd.Parameters.AddWithValue("@nombre", ocm.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", ocm.Apellido);
                    cmd.Parameters.AddWithValue("@cedula", ocm.Cedula);
                    cmd.Parameters.AddWithValue("@telefono", ocm.Telefono);
                    cmd.Parameters.AddWithValue("@correo", ocm.Correo);
                    cmd.Parameters.AddWithValue("@direccion", ocm.Direccion);
                    cmd.Parameters.AddWithValue("@id", ocm.Id);
                    cmd.ExecuteNonQuery();
                    rp = true;
                }
            }catch(Exception e)
            {
                rp = false;
                string msg = e.Message;
                Console.WriteLine(msg);
            }
            return rp;
        }

        public Boolean Guardar(ContactosModel o_contacto)
        {
            Boolean rpta;
            //String query = "insert into contactos (nombre, apellido, cedula, telefono, correo, direccion) values ('" + o_contacto.Nombre + "','" + o_contacto.Apellido + "','" + o_contacto.Cedula + "','" + o_contacto.Telefono + "','" + o_contacto.Correo + "','" + o_contacto.Direccion + "')";
            try {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_guardar @nombre,@apellido,@cedula,@telefono,@correo,@direccion", conexion);
                    cmd.Parameters.AddWithValue("@nombre", o_contacto.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", o_contacto.Apellido);
                    cmd.Parameters.AddWithValue("@cedula", o_contacto.Cedula);
                    cmd.Parameters.AddWithValue("@telefono", o_contacto.Telefono);
                    cmd.Parameters.AddWithValue("@correo", o_contacto.Correo);
                    cmd.Parameters.AddWithValue("@direccion", o_contacto.Direccion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            } catch(Exception e){
                rpta = false;
                string msg = e.Message;
                Console.WriteLine(msg);
            }
            return rpta;
        }
    }
}
