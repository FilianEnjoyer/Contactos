﻿using Gestor.Views;
namespace Gestor
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ContactosPage), typeof(ContactosPage));

            Routing.RegisterRoute(nameof(EditarContactosPage), typeof(EditarContactosPage));

            Routing.RegisterRoute(nameof(AñadirContactosPage), typeof(AñadirContactosPage));

            Routing.RegisterRoute(nameof(RegistroPage), typeof(RegistroPage));

            Routing.RegisterRoute(nameof(RecuperarContraseña), typeof(RecuperarContraseña));

            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }
    }
}
