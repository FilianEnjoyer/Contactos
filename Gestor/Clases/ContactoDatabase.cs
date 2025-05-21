using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Gestor.Clases
{
    public class ContactoDatabase
    {
        private readonly SQLiteAsyncConnection _db;

        public ContactoDatabase()
        {
            if (_db is not null)
                return;

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "agenda.db");
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<ClaseContactos>().Wait();
        }

        public Task<List<ClaseContactos>> ObtenerContactosAsync() => _db.Table<ClaseContactos>().ToListAsync();

        public async Task<List<ClaseContactos>> GetItemsActivosAsync()
        {
            return await _db.Table<ClaseContactos>().Where(t => t.Activo).ToListAsync();

            // SQL queries are also possible
            //return await Database.QueryAsync<Contacto>("SELECT * FROM [TodoItem] WHERE [Activo] = True");
        }

        public async Task<ClaseContactos> GetItemAsync(int id)
        {
            return await _db.Table<ClaseContactos>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }


        public Task<int> GuardarContactoAsync(ClaseContactos contacto) => contacto.Id != 0 ? _db.UpdateAsync(contacto) : _db.InsertAsync(contacto);
        public Task<int> EliminarContactoAsync(ClaseContactos contacto) => _db.DeleteAsync(contacto);

        public Task<List<ClaseContactos>> BuscarContactosAsync(string texto)
        {
            // Construimos el patrón para LIKE (starts-with)
            var patron = texto.ToLower() + "%";

            // Ejecutamos una consulta SQL cruda que compara en minúsculas
            return _db.QueryAsync<ClaseContactos>(
                @"SELECT *
          FROM ClaseContactos
          WHERE LOWER(Nombre)   LIKE ?
             OR LOWER(Correo)   LIKE ?
             OR LOWER(Telefono) LIKE ?
             OR LOWER(Direccion)LIKE ?",
                patron, patron, patron, patron
            );
        }

    }
}
