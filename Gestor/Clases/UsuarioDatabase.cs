using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Gestor.Clases;
namespace Gestor.Clases
{
    public class UsuarioDatabase
    {
        private readonly SQLiteAsyncConnection _db;

        public UsuarioDatabase()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "usuarios.db");
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<Usuarios>().Wait();
        }

        public Task<List<Usuarios>> ObtenerUsuariosAsync()
        {
            return _db.Table<Usuarios>().ToListAsync();
        }

        public Task<Usuarios> ObtenerUsuarioPorIdAsync(int id)
        {
            return _db.Table<Usuarios>().Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> GuardarUsuarioAsync(Usuarios usuario)
        {
            return usuario.Id != 0 ? _db.UpdateAsync(usuario) : _db.InsertAsync(usuario);
        }

        public Task<int> EliminarUsuarioAsync(Usuarios usuario)
        {
            return _db.DeleteAsync(usuario);
        }

        public Task<List<Usuarios>> BuscarUsuariosAsync(string texto)
        {
            var patron = texto.ToLower() + "%";
            return _db.QueryAsync<Usuarios>(
                @"SELECT *
                  FROM Usuarios
                  WHERE LOWER(Nombre) LIKE ?
                     OR LOWER(Correo) LIKE ?
                     OR LOWER(Contraseña) LIKE ?",
                patron, patron, patron
            );
        }
    }
}
