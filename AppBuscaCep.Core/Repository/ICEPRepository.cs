using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppBuscaCep.Core.Repository
{
    public interface ICEPRepository
    {
        bool Salvar(ViaCedDto cep);
        TableQuery<ViaCedDto> Obter();
        ViaCedDto ObterPor(string CEP);
    }
}