﻿using AppBuscaCEP.Data.Dto;
using AppBuscaCEP.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppBuscaCEP.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CepsPage : ContentPage
    {
        CepsViewModel ViewModel { get => (CepsViewModel)this.BindingContext; }

        public CepsPage()
        {
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            ViewModel.SelecionarCommand.Execute(e.Item);

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
