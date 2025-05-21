
using Gestor.Clases;
using Gestor.Views.Controles;
using Gestor = Gestor.Clases.ClaseContactos;
namespace Gestor.Views;
[QueryProperty(nameof(IDContacto),"ID")]
public partial class EditarContactosPage : ContentPage
{
    private readonly ContactoDatabase _database;
    private ClaseContactos _contacto;
    public EditarContactosPage()
	{
		InitializeComponent();
        _database = new ContactoDatabase();
    }
    // Este setter se invoca cuando Shell pasa el parámetro "ID"
    public string IDContacto
    {
        set
        {
            
            LoadContactoAsync(int.Parse(value));
        }
    }
    private async void LoadContactoAsync(int id)
    {
        _contacto = await _database.GetItemAsync(id);
        if (_contacto != null)
        {
            ContactoCntrl.Nombre = _contacto.Nombre;
            ContactoCntrl.Telefono = _contacto.Telefono;
            ContactoCntrl.Correo = _contacto.Correo;
            ContactoCntrl.Direccion = _contacto.Direccion;
        }
        else
        {
            await DisplayAlert("Error", "No se encontró el contacto.", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        // Actualizamos la instancia con los valores del control
        _contacto.Nombre = ContactoCntrl.Nombre;
        _contacto.Telefono = ContactoCntrl.Telefono;
        _contacto.Correo = ContactoCntrl.Correo;
        _contacto.Direccion = ContactoCntrl.Direccion;

        // Guardamos (insert/update) en SQLite
        await _database.GuardarContactoAsync(_contacto);

        // Volvemos a la página anterior
        await Shell.Current.GoToAsync("..");
    }

    private void ContactoCntrl_EnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}