using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppBuscaCEP.ViewModels
{
    abstract class BasePageViewModel : ViewModelBase
    {
        internal abstract Task InitializeAsync(object parametro);
    }
}
