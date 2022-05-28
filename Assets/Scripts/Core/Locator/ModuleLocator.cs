using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Modules.AdsRegister;
using Modules.Backend;
using Modules.DataStorage;
using Modules.VideoAds;
using Modules.WebRequest;

namespace Core.Locator
{
    public static class ModuleLocator
    {
        static readonly Dictionary<string, IModule> Pool = new Dictionary<string, IModule>();

        public static IEnumerator Init(IApp app)
        {
            Pool.Add(nameof(IWebRequest), new WebRequestModule());
            Pool.Add(nameof(IBackend), new BackendModule());
            Pool.Add(nameof(IVideoAds), new VideoAdsModule());
            Pool.Add(nameof(IDataStorage), new DataStorageModule());
            Pool.Add(nameof(IAdsRegister), new AdsRegisterModule());

            foreach (var module in Pool.Values) yield return module.Init(app);
            foreach (var module in Pool.Values) module.Link();
        }

        /// <summary>
        /// Checks module instance existing.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Contains<T>() where T : IModule => Pool.ContainsKey(typeof(T).Name);

        /// <summary>
        /// here's the method that SuitableModuleResolver.cs calls when we call it to get module
        /// from some part of the project
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>() where T : class, IModule
        {
            var key = typeof(T).Name;
            
            if (!Pool.ContainsKey(key))
                return null;
            
            return (T) Pool.First(i => i.Key == key).Value;
        }
    }
}
