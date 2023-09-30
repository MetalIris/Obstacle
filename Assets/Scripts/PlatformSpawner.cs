using System;
using UnityEngine;
using Zenject;

public class PlatformSpawner : MonoBehaviour
{
    [Inject] private DiContainer _container;
    
    [SerializeField] private GameObject explosivePlatformPrefab;
    [SerializeField] private Transform spawnPoint;
    
    private GameObject _currentPlatform;
    private Vector3 _lastSpawnPosition;
    private bool _platformSpawned;
    private bool _isHandlingDestroy;
    private int _spawnCount;
    private int _destroyedCount;

    public static event Action PlatformDestroyed;
    public static event Action AllPlatformsDestroyed;

    private void Start()
    {
        _lastSpawnPosition = spawnPoint.position;
        
        SpawnPlatform();
    }

    public void DestroyPlatform()
    {
        if (_isHandlingDestroy) return;
        
        Destroy(_currentPlatform);
        _platformSpawned = false;
        _isHandlingDestroy = true;
        PlatformDestroyed?.Invoke();

        _destroyedCount++;

        if (_destroyedCount >= 11)
        {
            HandleAllPlatformsDestroyed();
        }
    }

    private void Update()
    {
        if (!_platformSpawned && _spawnCount < 10)
        {
            SpawnPlatform();
            _spawnCount++;
        }
    }

    private void SpawnPlatform()
    {
        var nextSpawnPosition = _lastSpawnPosition + new Vector3(10f, 0f, 0f);
        _currentPlatform = Instantiate(explosivePlatformPrefab, nextSpawnPosition, Quaternion.identity);
        _container.InjectGameObject(_currentPlatform);
        
        _lastSpawnPosition = nextSpawnPosition;
        _platformSpawned = true;
        _isHandlingDestroy = false;
    }

    private void HandleAllPlatformsDestroyed()
    {
        AllPlatformsDestroyed?.Invoke();
    }
}
