using System.Collections;
using Core;
using Core.Locator;
using Data;
using Modules.AdsRegister;
using UnityEngine;
using UnityEngine.Video;

namespace AdsBanner
{
    [RequireComponent(typeof(VideoPlayer)), RequireComponent(typeof(MeshRenderer))]
    public class AdsBannerHandler : MonoBehaviour, IAdsBanner, IInit
    {
        [Space(5)]
        [SerializeField] float _secDelay;
        
        readonly SuitableModuleResolver<IAdsRegister> _adsRegister = new SuitableModuleResolver<IAdsRegister>();
        bool _initialized, _enabled;
        
        VideoPlayer _videoPlayer;
        Coroutine _waitEnd;
        AdInfo _info;
        
        
        void Awake()
        {
            _videoPlayer = GetComponent<VideoPlayer>();
            _videoPlayer.source = VideoSource.Url;
            _videoPlayer.isLooping = false;
        }

        
        public void Init(IApp app)
        {
            if (gameObject.activeSelf) _adsRegister.Resolve.CheckIn(this);
            _initialized = true;
        }
        
        public void SetInfo(AdInfo info)
        {
            if (_info == null)
            {
                _info = info;
                tryShow();
            }
            
            if (_info.Id.Equals(info.Id) && _info.Source == AdSource.URL && info.Source == AdSource.File)
            {
                _info = info;
            }
        }
        
        
        void OnEnable()
        {
            if (!_initialized || _enabled) return;
            _adsRegister.Resolve.CheckIn(this);
            tryShow();
            _enabled = true;
        }
        
        void OnDisable()
        {
            if (!_initialized || !_enabled) return;
            _adsRegister.Resolve.CheckOut(this);
            _enabled = false;
        }
        
        void OnDestroy()
        {
            if (!_initialized || !_enabled) return;
            _adsRegister.Resolve.CheckOut(this);
            _enabled = false;
        }


        IEnumerator waitEnd()
        {
            while (!_videoPlayer.isPlaying)
            {
                yield return null;
            }
            
            while (_videoPlayer.isPlaying)
            {
                yield return null;
            }

            _waitEnd = null;
            yield return new WaitForSeconds(_secDelay);
            tryShow();
        }
        

        void tryShow()
        {
            if (_waitEnd != null)
            {
                StopCoroutine(_waitEnd);
                _waitEnd = null;
            }

            if (_info == null) return;
            
            _videoPlayer.url = _info.URL;
            _videoPlayer.Play();
            _waitEnd = StartCoroutine(waitEnd());
        }
    }
}