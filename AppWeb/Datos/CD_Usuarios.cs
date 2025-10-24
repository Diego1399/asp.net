using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidad;
using System.Data.SqlClient;
using System.Data;


namespace Datos
{
    public class CD_Usuarios
    {
        public List<Usuario> Lista_user()
        {
            List<Usuario> lista = new List<Usuario>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select id_usuario, nombre, correo, contrasena, rol, activo, restablecer from usuario";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["id_usuario"]),
                                Nombre = dr["nombre"].ToString(),
                                Correo = dr["correo"].ToString(),
                                Clave = dr["contrasena"].ToString(),
                                Activo = Convert.ToBoolean(dr["activo"]),
                                Restablecer = Convert.ToBoolean(dr["restablecer"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lista;
        }
    }
}
