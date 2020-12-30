using AppBuscaCEP.Data.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppBuscaCEP.ViewModels
{
    class CepViewModel : ViewModelBase
    {
        private readonly ViaCedDto _cepDto;

        public string Cep { get => _cepDto?.cep; }

        public bool HasCep { get => !(_cepDto is null); }

        public string Logradouro { get => _cepDto?.logradouro; }

        public string Comeplemento { get => _cepDto?.complemento; }

        public string Bairro { get => _cepDto?.bairro; }

        public string Localidade { get => _cepDto?.localidade; }

        public string UF { get => _cepDto?.uf; }

        public string IBGE { get => _cepDto?.ibge; }

        public string DDD { get => _cepDto?.ddd; }

        public CepViewModel(ViaCedDto cepDto)
        {
            _cepDto = cepDto;
        }
    }
}
