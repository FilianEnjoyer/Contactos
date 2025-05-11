using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.Clases
{
    static class RepositorioContactos
    {
        public static List<ClaseContactos> Contactos { get; set; } = new List<ClaseContactos>()
        {
            new ClaseContactos { Id= 1, Nombre= "Alonso1", Correo="ads@algo.es1", Direccion="algun lugar1", Telefono = "1234561"},
            new ClaseContactos { Id= 2, Nombre= "Alonso2", Correo="ads@algo.es2", Direccion="algun lugar2", Telefono = "1234562"},
            new ClaseContactos { Id= 3, Nombre= "Alonso3", Correo="ads@algo.es3", Direccion="algun lugar3", Telefono = "1234563"}
        };
        public static List<ClaseContactos> ObtenerContactos() => Contactos;

        public static ClaseContactos ObtenerContactoConID(int id)
        {
            var contacto = Contactos.FirstOrDefault(x => x.Id == id);
            if (contacto != null)
            {
                return new ClaseContactos
                {
                    Id = contacto.Id,
                    Nombre = contacto.Nombre,
                    Telefono = contacto.Telefono,
                    Correo = contacto.Correo,
                    Direccion = contacto.Direccion,
                };
            }
            return null;
        }
        public static void ActualizarContacto (int Id, ClaseContactos contacto)
        {
            if (Id != contacto.Id) return;

            var contactoParaActualizar = Contactos.FirstOrDefault(x => x.Id == Id);
            if (contactoParaActualizar != null)
            {
                contactoParaActualizar.Nombre = contacto.Nombre;
                contactoParaActualizar.Telefono = contacto.Telefono;
                contactoParaActualizar.Correo = contacto.Correo;
                contactoParaActualizar.Direccion = contacto.Direccion;
            }        
        }
        public static void AgregarContacto(ClaseContactos contacto)
        {
            var IDMaxima = Contactos.Max(x => x.Id);
            contacto.Id = IDMaxima + 1;
            Contactos.Add(contacto);
        }
        public static void EliminarContacto(int id)
        {
            var contacto = Contactos.FirstOrDefault(x => x.Id == id);
            if (contacto != null)
            {
                Contactos.Remove(contacto);
            }
        }
        public static List<ClaseContactos> BuscarContactos(string busqueda)
        {
            var contactos = Contactos.Where(x => !string.IsNullOrWhiteSpace (x.Nombre) && x.Nombre.StartsWith(busqueda, StringComparison.OrdinalIgnoreCase))?.ToList();
            if (contactos == null || contactos.Count <= 0)
                contactos = Contactos.Where(x => !string.IsNullOrWhiteSpace(x.Correo) && x.Correo.StartsWith(busqueda, StringComparison.OrdinalIgnoreCase))?.ToList();
            else
                return contactos;
            if (contactos == null || contactos.Count <= 0)
                contactos = Contactos.Where(x => !string.IsNullOrWhiteSpace(x.Telefono) && x.Telefono.StartsWith(busqueda, StringComparison.OrdinalIgnoreCase))?.ToList();
            else
                return contactos;
            if (contactos == null || contactos.Count <= 0)
                contactos = Contactos.Where(x => !string.IsNullOrWhiteSpace(x.Direccion) && x.Direccion.StartsWith(busqueda, StringComparison.OrdinalIgnoreCase))?.ToList();
            else
                return contactos;
            return contactos;   
        }


    }
}
