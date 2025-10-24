# asp.net

## Conectar con SQL

### Web.config
```
<connectionStrings>
  <add name="cadena" providerName="System.Data.ProviderName" connectionString="Data Source=DESKTOP-8D535GO\SQLEXPRESS;Initial Catalog=MiProyecto; Integrated Security=True; TrustServerCertificate=True;" />
</connectionStrings>
```
### Clase Datos
Instalar System.Configuration
```
using System.Configuration;

namespace Datos
{
    public class Conexion
    {
        public static string cn = ConfigurationManager.ConnectionStrings["cadena"].ToString();
    }
}
```

```
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
```

### Entity

Clase para tener los objetos de la DB

### Negocio 

```
using Datos;
using Entidad;

namespace Negocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objDatos = new CD_Usuarios();

        public List<Usuario> listar ()
        {
            return objDatos.Lista_user();
        }
    }
}
```

## Mostrar en la pagina 

### HomeController

```
[HttpGet]
public JsonResult listarUsuarios()
{
    List<Usuario> oLista = new List<Usuario>();
    oLista = new CN_Usuarios().listar();
    return Json(new {data = oLista }, JsonRequestBehavior.AllowGet);
}

```

### Vista Usuario

```

@{
    ViewBag.Title = "Usuarios";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>Vista Usuarios</h2>

<ol class="breadcrumb mb-4 mt-4">
    <li class="breadcrumb-item"><a href="@Url.Action("Index","Home")">Dashboard</a></li>
    <li class="breadcrumb-item active">Usuarios</li>
</ol>

<div class="card">
    <div class="card-header">
        <i class="fas fa-users me-1"></i>Lista de Usuarios
    </div>
    <div class="card-body">
        <div class="row">
            <div class="col-12">
                <button type="button" class="btn btn-success">Crear nuevo</button>
            </div>
            <hr />
            <table id="tabla_usuario" class="display cell-border" style="width: 100%">
                <thead>
                    <tr>
                        <th>Nombre</th>
                        <th>Correo</th>
                        <th>Activo</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>

                </tbody>
            </table>
        </div>

    </div>
</div>

@section scripts{
    <script>

        var tabladata;

        jQuery.ajax({
            url: '@Url.Action("listarUsuarios", "Home")',
            type: 'GET',
            dataType: 'json',
            contentType: "Application/json; charset=utf-8",
            success: function (data) {
                console.log(data);
                //tabla.clear().draw();
                //$.each(data, function (index, usuario) {
                //    tabla.row.add([
                //        usuario.Nombre,
                //        usuario.Correo,
                //        usuario.Activo ? 'Sí' : 'No'
                //    ]).draw(false);
                //});
            },
        })

        tabladata = $("#tabla_usuario").DataTable({
            responsive: true,
            ordering: false,
            "ajax": {
                url: '@Url.Action("listarUsuarios", "Home")',
                type: 'GET',
                dataType: 'json',
            },
            "columns": [
                { "data": "Nombre" },
                { "data": "Correo" },
                {
                    "data": "Activo", "render": function (data) {
                        if (!data) return 'Inactivo'
                        return 'Activo'
                    }
                },
                {
                    "defaultContent": '<button type="button" class="btn btn-primary"><i class="fas fa-pen"></i></button>' +
                        '<button type="button" class="btn btn-danger ms-2"><i class="fas fa-trash"></i></button>',
                    "orderable": false,
                    "searchable": false,
                    "width": "90px"
                }
            ]
        })
    </script>
```

## Editar en SQL

```
namespace Datos
{
    public class CD_Usuarios
    {
        public int Registrar(Usuario obj, out string Mensaje) {
            using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue())
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                }
        }
    }
}

```

