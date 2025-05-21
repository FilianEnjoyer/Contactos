using Gestor.Clases;
namespace Gestor.Views;

public partial class AñadirContactosPage : ContentPage
{
    private readonly ContactoDatabase _database;
    public AñadirContactosPage()
	{
		InitializeComponent();
        _database = new ContactoDatabase();
    }

    private async void ContactoCntrl_AlGuardar(object sender, EventArgs e)
    {
        var nuevo = new ClaseContactos
        {
            Nombre = ContactoCntrl.Nombre,
            Telefono = ContactoCntrl.Telefono,
            Correo = ContactoCntrl.Correo,
            Direccion = ContactoCntrl.Direccion,
            Activo = true  // Opcional: dar de alta como activo
        };

        // Inserta en SQLite
        await _database.GuardarContactoAsync(nuevo);

        // Regresa a la página anterior
        await Shell.Current.GoToAsync("..");
    }

    private void ContactoCntrl_AlCancelar(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void ContactoCntrl_EnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}