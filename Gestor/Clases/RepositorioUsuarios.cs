using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.Clases
{
    internal class RepositorioUsuarios
    {
        public static List<Usuarios> ListaUsuarios { get; set; } = new List<Usuarios>()
        {
            new Usuarios { Id= 1, Nombre= "U1", Correo="U1@algo.es", Contraseña="U1C"},
            new Usuarios { Id= 2, Nombre= "U2", Correo="U2@algo.es", Contraseña="U2C"},
            new Usuarios { Id= 3, Nombre= "U3", Correo="U3@algo.es", Contraseña="U3C"}
        };
        public static List<Usuarios> ObtenerUsuarios() => ListaUsuarios;

        public static ClaseContactos ObtenerUsuarioConID(int id)
        {
            var contacto = ListaUsuarios.FirstOrDefault(x => x.Id == id);
            if (contacto != null)
            {
                return new ClaseContactos
                {
                    Id = contacto.Id,
                    Nombre = contacto.Nombre,
                    Correo = contacto.Correo,
                    Direccion = contacto.Contraseña,
                };
            }
            return null;
        }
        public static void ActualizarUsuario(int Id, Usuarios usuario)
        {
            if (Id != usuario.Id) return;

            var usuarioParaActualizar = ListaUsuarios.FirstOrDefault(x => x.Id == Id);
            if (usuarioParaActualizar != null)
            {
                usuarioParaActualizar.Nombre = usuario.Nombre;
                usuarioParaActualizar.Correo = usuario.Correo;
                usuarioParaActualizar.Contraseña = usuario.Contraseña;
            }
        }
        public static void AgregarUsuario(Usuarios usuario)
        {
            var IDMaxima = ListaUsuarios.Max(x => x.Id);
            usuario.Id = IDMaxima + 1;
            ListaUsuarios.Add(usuario);
        }
        public static void EliminarUsuario(int id)
        {
            var usuarios = ListaUsuarios.FirstOrDefault(x => x.Id == id);
            if (usuarios != null)
            {
                ListaUsuarios.Remove(usuarios);
            }
        }
        public static List<Usuarios> BuscarUsuario(string busqueda)
        {
            var usuarios = ListaUsuarios.Where(x => !string.IsNullOrWhiteSpace(x.Nombre) && x.Nombre.StartsWith(busqueda, StringComparison.OrdinalIgnoreCase))?.ToList();
            if (usuarios == null || usuarios.Count <= 0)
                usuarios = ListaUsuarios.Where(x => !string.IsNullOrWhiteSpace(x.Correo) && x.Correo.StartsWith(busqueda, StringComparison.OrdinalIgnoreCase))?.ToList();
            else
                return usuarios;
            if (usuarios == null || usuarios.Count <= 0)
                usuarios = ListaUsuarios.Where(x => !string.IsNullOrWhiteSpace(x.Contraseña) && x.Contraseña.StartsWith(busqueda, StringComparison.OrdinalIgnoreCase))?.ToList();
            else
                return usuarios;
            return usuarios;
        }
    }
}
