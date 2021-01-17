using AppBuscaCep.Core.Repository;
using AppBuscaCep.Core.Services;
using System;
using System.Threading.Tasks;

namespace AppBuscaCep.Core.Command
{
    public class BuscarCEPCommandHandler : ICommandHandler<BuscarCEPCommand>
    {
        private readonly ICEPRepository _iCEPRepository;
        private readonly IAPIServices _apiServices;

        public BuscarCEPCommandHandler(ICEPRepository iCEPRepository, IAPIServices apiServices)
        {
            _iCEPRepository = iCEPRepository;
            _apiServices = apiServices;
        }

        public Task HandleAsync(BuscarCEPCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
