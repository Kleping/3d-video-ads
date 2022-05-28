using System.IO;
using Modules.Backend;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Data
{
    public class BackendConfig : ScriptableObject, IBackendConfig
    {
        [Space(5)]
        [SerializeField] BackendEnvironment _environment;
        
        [Space(5)]
        [SerializeField] string _domain;

        [Space(2.5F)]
        [SerializeField] string _endpointGetVideoAds;
        [SerializeField] string _endpointGetPurchaseInfo;
        [SerializeField] string _endpointConfirmPurchase;
        

        const string Development = "dev";
        const string Production = "prod";

        
        public string GetEndpoint(EndpointType type)
        {
            var command = getCommand(type);
            return $"{_domain}{(_environment == BackendEnvironment.Development ? Development : Production)}/{command}";
        }
        
        string getCommand(EndpointType type)
        {
            switch (type)
            {
                case EndpointType.GetVideoAds:
                    return _endpointGetVideoAds;
                
                case EndpointType.GetPurchaseInfo:
                    return _endpointGetPurchaseInfo;
                
                case EndpointType.ConfirmPurchase:
                    return _endpointConfirmPurchase;
                
                default:
                    return string.Empty;
            }
        }
        
        #if UNITY_EDITOR
        [MenuItem("Assets/Create/Scriptable Objects/Backend Config")]
        static void CreateScriptableObjectInstance()
        {
            var asset = ScriptableObject.CreateInstance<BackendConfig>();

            var path = AssetDatabase.GetAssetPath (Selection.activeObject);
            
            if (string.IsNullOrEmpty(path)) path = "Assets";
            else if (!string.IsNullOrEmpty(Path.GetExtension(path)))
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), string.Empty);

            var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath($"{path}/New {nameof(BackendConfig)}.asset");
            AssetDatabase.CreateAsset(asset, assetPathAndName);
 
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
        #endif
    }
}
