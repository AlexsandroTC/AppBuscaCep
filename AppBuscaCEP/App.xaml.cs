﻿using AppBuscaCEP.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppBuscaCEP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new BuscaCepPage();
            MainPage = new CepsPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
