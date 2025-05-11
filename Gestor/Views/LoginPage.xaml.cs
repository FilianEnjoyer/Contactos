namespace Gestor.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    protected override bool OnBackButtonPressed()
    {
        Application.Current.Quit();
        return true;
    }
    private async void BotonLogin_Clicked(object sender, EventArgs e)
    {
        if (IsCredentialCorrect(Usuario.Text, Contrase�a.Text))
        {
            Preferences.Set("UsuarioActual", Usuario.Text.Trim());
            await SecureStorage.SetAsync("hasAuth", "true");
            await Shell.Current.GoToAsync("//ContactosPage");
        }
        else
        {
            Preferences.Remove("UsuarioActual");
            await DisplayAlert("Login failed", "Username or password if invalid", "Try again");
        }
    }

    private void TapGestureRecognizer_Registrarse(object sender, TappedEventArgs e)
    {
        Label Reg = (sender as Label);
        var Msg = Reg.FormattedText.Spans[0].Text;
        //var customerName = (sender as Label).Text;
        DisplayAlert("Registrar Usuario", $"Name : {Msg}", "ok");
    }

    private void TapGestureRecognizer_OlvideContrase�a(object sender, TappedEventArgs e)
    {
        Label Reg = (sender as Label);
        var Msg = Reg.FormattedText.Spans[1].Text;
        //var customerName = (sender as Label).Text;
        DisplayAlert("Recuperar Password", $"Name : {Msg}", "ok");
    }
    bool IsCredentialCorrect(string username, string password)
    {
        return Usuario.Text == "admin" && Contrase�a.Text == "1234";
    }
}