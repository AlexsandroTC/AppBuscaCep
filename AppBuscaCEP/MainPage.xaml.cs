using AppBuscaCEP.Data.Dto;
using AppBuscaCEP.ViewModels;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppBuscaCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        BuscaCepViewModel buscaCepViewModel  { get => ((BuscaCepViewModel)BindingContext); }

        public MainPage()
        {
            InitializeComponent();

            //BindingContext = new BuscaCepViewModel();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(buscaCepViewModel.Cep))
                    return;
                
                using (var client = new HttpClient())
                {
                    //viacep.com.br/ws/01001000/json/  01001000
                    using (var response = await client.GetAsync($"https://viacep.com.br/ws/{buscaCepViewModel.Cep}/json/"))
                    {
                        response.EnsureSuccessStatusCode();

                        var content = await response.Content.ReadAsStringAsync();

                        if (string.IsNullOrWhiteSpace(content))
                            throw new InvalidOperationException();

                        var retorno = Newtonsoft.Json.JsonConvert.DeserializeObject<ViaCedDto>(content);

                        if (retorno.erro)
                            throw new InvalidOperationException();

                        /*txtCEP.Text = retorno.cep;
                        txtLogradouro.Text = retorno.logradouro;
                        txtComeplemento.Text = retorno.complemento;
                        txtBairro.Text = retorno.bairro;
                        txtLocalidade.Text = retorno.localidade;
                        txtUF.Text = retorno.uf;
                        txtIBGE.Text = retorno.ibge;
                        txtDDD.Text = retorno.ddd;*/

                    }

                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Ops", "Ocorre algo de errado na consulta com a API", "Ok"); 
            }
        }
    }
}
