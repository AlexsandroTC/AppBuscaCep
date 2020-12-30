using AppBuscaCEP.Data.Dto;
using AppBuscaCEP.Providers;
using SQLite;
using System;
using System.Linq.Expressions;
using Xamarin.Forms;

namespace AppBuscaCEP.Data
{
    sealed class DatabaseService
    {
        private static Lazy<DatabaseService> _lazyDatabaseService = new Lazy<DatabaseService>(() => new DatabaseService());
        private readonly SQLiteConnection _connection;

        public static DatabaseService Current { get => _lazyDatabaseService.Value; }
        
        private DatabaseService()
        {
            var path = DependencyService.Get<IDatabaseServicePathProvider>().GetPath();

            _connection = new SQLiteConnection(path);
            _connection.CreateTable<ViaCedDto>();
        }

        public bool Save<TDto>(TDto dto) where TDto : new() 
        {
            return _connection.InsertOrReplace(dto) > 0;
        }

        public TableQuery<TDto> Get<TDto>() where TDto : new()
        {
            return _connection.Table<TDto>();
        }

        public TDto Get<TDto>(Guid id) where TDto : new()
        {
            return _connection.Find<TDto>(id);
        }

        public TableQuery<TDto> Get<TDto>(Expression<Func<TDto, bool>> expression) where TDto : new()
        {
            return _connection.Table<TDto>().Where(expression);
        }
    }
}
