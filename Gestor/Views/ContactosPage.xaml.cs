using Gestor.Clases;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
namespace Gestor.Views;

public partial class ContactosPage : ContentPage
{
    private readonly ContactoDatabase _database;
    public ContactosPage()
	{
		InitializeComponent();
        _database = new ContactoDatabase();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarContactosAsync();
    }

    private async Task CargarContactosAsync()
    {
        // Obtener todos los contactos de SQLite
        var lista = await _database.ObtenerContactosAsync();
        var contactos = new ObservableCollection<ClaseContactos>(lista);
        listContacts.ItemsSource = contactos;
    }
    private async void listContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is ClaseContactos contacto)
        {
            // Navegar a la página de edición pasando el ID
            await Shell.Current.GoToAsync(
                $"{nameof(EditarContactosPage)}?ID={contacto.Id}"
            );
        }
    }

    private void listContacts_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        listContacts.SelectedItem = null;
    }

    private async void btnAñadir_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AñadirContactosPage));
    }
    
    private async void Borrar_Pulsado(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        if (menuItem?.CommandParameter is ClaseContactos contacto)
        {
            // Eliminar del DB y recargar
            await _database.EliminarContactoAsync(contacto);
            await CargarContactosAsync();
        }
    }
    

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var texto = e.NewTextValue?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(texto))
        {
            // Si la barra está vacía, recarga todos los contactos
            await CargarContactosAsync();
        }
        else
        {
            // Llama al método que busca en SQLite sin distinguir mayúsculas/minúsculas
            var resultados = await _database.BuscarContactosAsync(texto);
            listContacts.ItemsSource = new ObservableCollection<ClaseContactos>(resultados);
        }
    }
}