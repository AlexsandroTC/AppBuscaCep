using AppBuscaCep.Core;
using AppBuscaCep.Core.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppBuscaCep.Infra.Http
{
    public class APIServices : IAPIServices
    {
        public ViaCedDto ObterCep(string cep)
        {
            ViaCedDto viaCep;

            using (var client = new HttpClient())
            {
                using (var response = client.GetAsync($"https://viacep.com.br/ws/{cep}/json/"))
                {
                    var content = response.Result.Content.ReadAsStringAsync(); //await response.Content.ReadAsStringAsync();

                    viaCep = Newtonsoft.Json.JsonConvert.DeserializeObject<ViaCedDto>(content.Result);
                }
            }

            return viaCep;
        }
    }
}
