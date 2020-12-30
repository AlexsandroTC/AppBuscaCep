using AppBuscaCEP.Data.Dto;
using AppBuscaCEP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppBuscaCEP.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CepPage : ContentPage
    {
        public CepPage(ViaCedDto viaCepDto)
        {
            InitializeComponent();

            BindingContext = new CepViewModel(viaCepDto);
        }
    }
}