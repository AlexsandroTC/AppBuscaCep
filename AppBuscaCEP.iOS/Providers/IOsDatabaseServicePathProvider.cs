using AppBuscaCEP.iOS.Providers;
using AppBuscaCEP.Providers;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOsDatabaseServicePathProvider))]

namespace AppBuscaCEP.iOS.Providers
{
    class IOsDatabaseServicePathProvider : IDatabaseServicePathProvider
    {
        public string GetPath()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"..." , "Library" ,"Databases");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            return Path.Combine(folder, "AppBuscaCep.db3"); ;
        }
    }
}