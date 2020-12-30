using AppBuscaCEP.Droid.Providers;
using AppBuscaCEP.Providers;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(DroidDatabaseServicePathProvider))]

namespace AppBuscaCEP.Droid.Providers
{
    class DroidDatabaseServicePathProvider : IDatabaseServicePathProvider
    {
        public string GetPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"AppBuscaCep.db3");
        }
    }
}