using AppBuscaCEP.Data.Dto;
using AppBuscaCEP.Pages;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppBuscaCEP.ViewModels
{
    sealed class CepsViewModel : ViewModelBase
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

                if (Data.DatabaseService.Current.Get<ViaCedDto>(e => e.cep.Equals(Regex.Replace(Cep, @"[^\d]", string.Empty))).Any())
                {
                    await App.Current.MainPage.DisplayAlert("Oops", "O CEP Já foi cadastrado.","Ok");
                    return;
                }

                IsBusy = true;
                BuscarCommand.ChangeCanExecute();

                using (var client = new HttpClient())
                {
                    ///viacep.com.br/ws/01001000/json/01001000
                    using (var response = await client.GetAsync($"https://viacep.com.br/ws/{Cep}/json/"))
                    {
                        response.EnsureSuccessStatusCode();

                        var content = await response.Content.ReadAsStringAsync();

                        if (string.IsNullOrWhiteSpace(content))
                            throw new InvalidOperationException();

                        var cepDto = Newtonsoft.Json.JsonConvert.DeserializeObject<ViaCedDto>(content);

                        if (cepDto.erro)
                            throw new InvalidOperationException();

                        Data.DatabaseService.Current.Save(cepDto);

                        RefreshCommand.Execute(true);
                    }

                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ops", "Ocorre algo de errado na consulta com a API", "Ok");
            }
            finally
            {
                IsBusy = false;

                BuscarCommand.ChangeCanExecute();
            }
        }

        public ObservableCollection<ViaCedDto> ViaCedDtos { get; set; } = new ObservableCollection<ViaCedDto>();

        private Command _refreshCommand;

        public Command RefreshCommand
        {
            get
            {
                if (_refreshCommand is null)
                {
                    _refreshCommand = new Command<bool>(async (args) => await RefreshCommandExecute(args), (args) => RefreshCommandCanExecute());
                }

                return _refreshCommand;
            }
        }

        private bool RefreshCommandCanExecute()
        {
            return IsNotBusy;
        }

        private async Task RefreshCommandExecute(bool force = false)
        {
            try
            {
                if (!force && IsBusy) return;

                IsBusy = true;
                BuscarCommand.ChangeCanExecute();

                ViaCedDtos.Clear();

                await Task.Factory.StartNew(() =>
                {
                    foreach (var cep in Data.DatabaseService.Current.Get<ViaCedDto>())
                    {
                        ViaCedDtos.Add(cep);
                    }
                });
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ops", "Ocorre algo de errado na consulta com a API", "Ok");
            }
            finally
            {
                IsBusy = false;

                BuscarCommand.ChangeCanExecute();
            }
        }

        private Command _selecionarCommand;

        public Command SelecionarCommand
        {
            get
            {
                if (_selecionarCommand is null)
                {
                    _selecionarCommand = new Command<object>(async (args) => await SelecionarCommandExecute(args));
                }

                return _selecionarCommand;
            }
        }

        private async Task SelecionarCommandExecute(object cepDto)
        {
            try
            {
                if (cepDto is ViaCedDto)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new CepPage((ViaCedDto)cepDto));
                }

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ops", "Ocorre algo de errado na consulta com a API", "Ok");
            }
        }
    }
}