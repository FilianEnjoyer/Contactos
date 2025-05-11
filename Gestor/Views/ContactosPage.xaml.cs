using Gestor.Clases;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
namespace Gestor.Views;

public partial class ContactosPage : ContentPage
{
	public ContactosPage()
	{
		InitializeComponent();
		
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarContactos();
    }
    private async void listContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    { if (listContacts.SelectedItem != null)
        {
            await Shell.Current.GoToAsync($"{nameof(EditarContactosPage)}?ID={((ClaseContactos)listContacts.SelectedItem).Id}");
        }
    }

    private void listContacts_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        listContacts.SelectedItem = null;
    }

    private void btnAñadir_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AñadirContactosPage));
    }
    
    private void Borrar_Pulsado(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var contacto = menuItem.CommandParameter as ClaseContactos;
        RepositorioContactos.EliminarContacto(contacto.Id);
        CargarContactos();
    }
    private void CargarContactos()
    {
        var contactos = new ObservableCollection<ClaseContactos>(RepositorioContactos.ObtenerContactos());
        listContacts.ItemsSource = contactos;
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var contactos = new ObservableCollection<ClaseContactos>(RepositorioContactos.BuscarContactos(((SearchBar)sender).Text));
        listContacts.ItemsSource = contactos;
    }
}