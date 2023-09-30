using Database.SettingsDatabase.Impl;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class BallInteraction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Inject] private SettingsDatabase _settingsDatabase;
    [Inject] private BallShoot _ballShoot;
    [Inject] private AnimationManager _animationManager;

    private float _delayTime;
    private bool _isPointerDown;
    private float _timer;

    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;

    
    [Header("ScaleSettings")]
    private float _originalScale;
    private float _scalePercent;
    private float _scaleReduction;
    private int _scaleChangeCount;
    private bool _isAnimating;
    private float _scaleChangeLimit = .8f;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isAnimating)
        {
            _isPointerDown = true;
            _timer = 0f;

            _ballShoot.ProjectileAppear();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isAnimating)
        {
            _isPointerDown = false;
            _ballShoot.ResetCharge();
            _ballShoot.Shoot(_scaleChangeCount);
            _scaleChangeCount = 0;
        }
    }
    
    private void Start()
    {
        _delayTime = _settingsDatabase.delayTime;
        _originalScale = _settingsDatabase.originalScale;
        _scalePercent = _settingsDatabase.scalePercent;
        _scaleReduction = _settingsDatabase.scaleReduction;
        _scaleChangeLimit = _settingsDatabase.scaleChangeLimit;
        
        PlatformSpawner.PlatformDestroyed += OnPlatformDestroyed;
        PlatformSpawner.AllPlatformsDestroyed += LevelCompleted;
    }

    private void Update()
    {
        if (!_isPointerDown)
        {
            _ballShoot.ResetCharge();
        }
        else
        {
            _timer += Time.deltaTime;
        
            if (_timer >= _delayTime && _scaleChangeCount < 5)
            {
                _timer = 0f;
                BallScaleChange();
                _scaleChangeCount++;
                
                _ballShoot.FillCharge();
            }
        }
    }

    private void BallScaleChange()
    {
        var newScale = _originalScale * (_scalePercent - _scaleReduction);
        _animationManager.ScaleChangeAnimation(newScale);
        _scalePercent -= 0.01f;

        _ballShoot.ProjectileScaleUp();
        
        GameOverCheck(newScale);
    }

    private void GameOverCheck(float scale)
    {
        if (scale < _scaleChangeLimit)
        {
            _losePanel.SetActive(true);
        }
    }
    
    private void OnPlatformDestroyed()
    {
        _isAnimating = true;
        _animationManager.MoveAndRotate(1, 10, () => { _isAnimating = false; });
    }
    
    private void OnDestroy()
    {
        PlatformSpawner.PlatformDestroyed -= OnPlatformDestroyed;
    }
    
    private void LevelCompleted()
    {
        _animationManager.LevelComplete();
        _winPanel.SetActive(true);
    }
}