using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Locator;
using Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    /// <summary>
    /// It's app's entry point.
    /// </summary>
    public class App : MonoBehaviour, IApp
    {
        [Space(5)]
        [SerializeField] BackendConfig _backendConfig;

        
        IEnumerator Start()
        {
            yield return ModuleLocator.Init(this);

            var rootObjectsInScene = new List<GameObject>();
            SceneManager.GetActiveScene().GetRootGameObjects(rootObjectsInScene);
            foreach (var component in rootObjectsInScene.SelectMany(t => t.GetComponentsInChildren<IInit>(true)))
                component.Init(this);
        }
        
        
        public IBackendConfig BackendConfig => _backendConfig;
    }
}
