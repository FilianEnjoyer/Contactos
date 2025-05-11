using Gestor.Clases;
using System.Threading.Tasks;
namespace Gestor.Views;

public partial class ContactosPage : ContentPage
{
	public ContactosPage()
	{
		InitializeComponent();
		List<ClaseContactos> contactos = RepositorioContactos.ObtenerContactos();
        listContacts.ItemsSource = contactos;
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
}