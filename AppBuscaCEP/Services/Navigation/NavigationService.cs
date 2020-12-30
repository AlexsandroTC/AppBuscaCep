using AppBuscaCEP.Pages;
using AppBuscaCEP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppBuscaCEP.Services.Navigation
{
    sealed class NavigationService
    {
        private static Lazy<NavigationService> _Lazy = new Lazy<NavigationService>(() => new NavigationService());
        private readonly Dictionary<Type, Type> _Mappings;

        public static NavigationService Current { get => _Lazy.Value; }

        public NavigationService()
        {
            _Mappings = new Dictionary<Type, Type>();
            CreateViewModelMappings();
        }

        private void CreateViewModelMappings()
        {
            _Mappings.Add(typeof(CepsViewModel), typeof(CepsPage));
            _Mappings.Add(typeof(CepViewModel), typeof(CepPage));
            //_Mappings.Add(typeof(), typeof());
        }

        private Application CurrentApplication => Application.Current;

        private INavigation Navigation
        {
            get
            {
                return ((CustomNavigationPage)CurrentApplication.MainPage).Navigation;
            }
        }

        internal Task Navigate<TViewModel>(object parametro = null) where TViewModel : BasePageViewModel
        {
            return InternalNavigate(typeof(TViewModel), parametro);
        }

        private async Task InternalNavigate(Type viewModelType, object parametro = null)
        {
            try
            {
                Page page = null;

                bool redirecionandoParaAPaginaInicial = (viewModelType == typeof(CepsViewModel));
                if (redirecionandoParaAPaginaInicial)
                {
                    bool paginaInicialCarregada = Navigation.NavigationStack.Any(e => e.BindingContext.GetType() == typeof(CepsViewModel));
                    if (!paginaInicialCarregada)
                    {
                        page = CreateAndBindPage(viewModelType);

                        var pageHaRemover = Navigation.NavigationStack.ToList();

                        await Navigation.PushAsync(page);

                        foreach (var pageRemover in pageHaRemover)
                        {
                            Navigation.RemovePage(pageRemover);
                        }
                    }
                    else
                    {
                        await Voltar(toRoot: true);
                    }


                }
                else
                {
                    page = CreateAndBindPage(viewModelType);

                    if (viewModelType.BaseType == typeof(BaseModalPageViewModel))
                    {
                        await Navigation.PushModalAsync(page, true);
                    }
                    else
                    {
                        await Navigation.PushAsync(page, true);
                    }
                }

                if (!(page is null))
                {
                    await (page.BindingContext as BasePageViewModel).InitializeAsync(parametro);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private Task Voltar(bool toRoot, bool animated = true)
        {
            if (toRoot)
            {
                return Navigation.PopToRootAsync(animated);
            }

            if (Navigation.ModalStack.Count > 0)
            {
                return Navigation.PopModalAsync(animated);
            }

            return Navigation.PopAsync(animated);

        }

        private Page CreateAndBindPage(Type viewModelType)
        {
            try
            {
                Type tipoPage = GetPageTypeForViewModel(viewModelType);

                if (tipoPage == null)
                    throw new Exception($"Não foi localizado mapeamento para de página para ao viewModel {viewModelType}.");

                Page page = Activator.CreateInstance(tipoPage) as Page;
                page.BindingContext = Activator.CreateInstance(viewModelType) as BasePageViewModel;

                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            try
            {
                if (!_Mappings.ContainsKey(viewModelType))
                    throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");

                return _Mappings[viewModelType];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void Initialize(object args = null)
        {
            CurrentApplication.MainPage = new CustomNavigationPage();
            Device.BeginInvokeOnMainThread(async () => await Navigate<CepsViewModel>(args));
        }
    }
}
