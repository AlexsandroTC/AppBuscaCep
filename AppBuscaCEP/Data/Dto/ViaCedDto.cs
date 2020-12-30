using SQLite;
using System;
using System.Text.RegularExpressions;

namespace AppBuscaCEP.Data.Dto
{
    [Table("Cep")]
    class ViaCedDto
    {
        [PrimaryKey]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string cep 
        { 
            get => _cep; 
            set => _cep = string.IsNullOrEmpty(value) ? value : Regex.Replace(value, @"[^\d]", string.Empty); 
        }

        private string _cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string ibge { get; set; }
        public string gia { get; set; }
        public string ddd { get; set; }
        public string siafi { get; set; }
        public bool erro { get; set; } = false;

        [Ignore]
        public string Detalhes
        {
            get
            {
                var detalhes = $"{logradouro}, {bairro}, {localidade}/{uf}";
                return detalhes;
            }
        }
    }
}