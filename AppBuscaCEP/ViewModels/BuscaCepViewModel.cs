using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppBuscaCEP.ViewModels
{
    public class BuscaCepViewModel : ViewModelBase
    {
        private string _Cep;
        public string Cep 
        { 
            get => _Cep; 
            set 
            {
                _Cep = value; 
                OnPropertyChanged(); 
                BuscarCommand.ChangeCanExecute();
            } 
        }

        public bool HasCep { get => !(_cepDto is null); }

        public string Logradouro { get => _cepDto?.logradouro; }

        public string Comeplemento { get => _cepDto?.complemento; }

        public string Bairro { get => _cepDto?.bairro; }
       
        public string Localidade { get => _cepDto?.localidade; }

        public string UF { get => _cepDto?.uf; }

        public string IBGE { get => _cepDto?.ibge;  }

        public string DDD { get => _cepDto?.ddd; }

        private ViaCedDto _cepDto = null;

        private Command _buscarCommand;

        //public Command BuscarCommand => _buscarCommand ?? (_buscarCommand = new Command(async () => BuscarCommandExecute()));
        public Command BuscarCommand 
        { 
            get
            {
                if (_buscarCommand is null)
                {
                    _buscarCommand = new Command(async () => await BuscarCommandExecute(), () => BuscarCommandCanExecute());

                }

                return _buscarCommand;
            }
        }

        private bool BuscarCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Cep) && Cep.Length >= 8 && IsNotBusy;
        }

        private async Task BuscarCommandExecute()
        {
            try
            {
                if (IsBusy) return;

                IsBusy = true;
                BuscarCommand.ChangeCanExecute();

                using (var client = new HttpClient())
                {
                    //viacep.com.br/ws/01001000/json/01001000
                    using (var response = await client.GetAsync($"https://viacep.com.br/ws/{Cep}/json/"))
                    {
                        response.EnsureSuccessStatusCode();

                        var content = await response.Content.ReadAsStringAsync();

                        if (string.IsNullOrWhiteSpace(content))
                            throw new InvalidOperationException();

                        _cepDto = Newtonsoft.Json.JsonConvert.DeserializeObject<ViaCedDto>(content);

                        if (_cepDto.erro)
                            throw new InvalidOperationException();
                    }

                }
            }
            catch (Exception ex)
            {
                _cepDto = null;
                await App.Current.MainPage.DisplayAlert("Ops", "Ocorre algo de errado na consulta com a API", "Ok");
            }
            finally
            {
                OnPropertyChanged(nameof(HasCep));
                OnPropertyChanged(nameof(Logradouro));
                OnPropertyChanged(nameof(Localidade));
                OnPropertyChanged(nameof(Comeplemento));
                OnPropertyChanged(nameof(Bairro));
                OnPropertyChanged(nameof(UF));
                OnPropertyChanged(nameof(IBGE));
                OnPropertyChanged(nameof(DDD));

                IsBusy = false;

                BuscarCommand.ChangeCanExecute();
            }
        }
    }
}
