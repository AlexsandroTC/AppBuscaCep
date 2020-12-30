using AppBuscaCEP.Data.Dto;
using System.Threading.Tasks;

namespace AppBuscaCEP.ViewModels
{
    sealed class CepViewModel : BasePageViewModel
    {
        public CepViewModel()
        {
        }

        private ViaCedDto _cepDto;

        public string Cep { get => _cepDto?.cep; }

        public bool HasCep { get => !(_cepDto is null); }

        public string Logradouro { get => _cepDto?.logradouro; }

        public string Complemento { get => _cepDto?.complemento; }

        public string Bairro { get => _cepDto?.bairro; }

        public string Localidade { get => _cepDto?.localidade; }

        public string UF { get => _cepDto?.uf; }

        public string IBGE { get => _cepDto?.ibge; }

        public string DDD { get => _cepDto?.ddd; }

        public CepViewModel(ViaCedDto cepDto)
        {
            _cepDto = cepDto;
        }

        internal override Task InitializeAsync(object parametro)
        {
            _cepDto = (ViaCedDto)parametro;

            OnPropertyChanged(nameof(HasCep));
            OnPropertyChanged(nameof(Logradouro));
            OnPropertyChanged(nameof(Cep));
            OnPropertyChanged(nameof(Complemento));
            OnPropertyChanged(nameof(Bairro));
            OnPropertyChanged(nameof(Localidade));
            OnPropertyChanged(nameof(UF));

            return Task.CompletedTask;
        }
    }
}
