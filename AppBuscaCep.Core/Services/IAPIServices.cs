using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppBuscaCep.Core.Services
{
    public interface IAPIServices
    {
        ViaCedDto ObterCep(string cep);
    }
}
