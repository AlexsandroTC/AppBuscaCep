using AppBuscaCep.Core;
using AppBuscaCep.Core.Repository;
using AppBuscaCEP.Infra.EF;
using SQLite;
using Xamarin.Forms;

namespace AppBuscaCep.Infra.EF.Repository
{
    public class CEPRepository : ICEPRepository
    {
        private readonly SQLiteConnection _connection;

        public CEPRepository()
        {
            var path = DependencyService.Get<IDatabaseServicePathProvider>().GetPath();

            _connection = new SQLiteConnection(path);
            _connection.CreateTable<ViaCedDto>();
        }

        public TableQuery<ViaCedDto> Obter()
        {
            return _connection.Table<ViaCedDto>();
        }

        public ViaCedDto ObterPor(string CEP)
        {
            return _connection.Find<ViaCedDto>(CEP);
        }

        public bool Salvar(ViaCedDto dto)
        {
            return _connection.InsertOrReplace(dto) > 0;
        }
    }
}
