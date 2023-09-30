using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ExplosivePlatform : MonoBehaviour
{
    [Inject] private PlatformSpawner _platformSpawner;
    
    [SerializeField] private GameObject[] objectsToActivate;
    [SerializeField] private ParticleSystem explosionParticleSystem;
    
    private  List<GameObject> _activatedObjects = new();
    private int _damageReceived;
    
    private void Start()
    {
        ActivateRandomObjects();
        Projectile.OnBulletHit += HandleBulletHit;
    }

    private void HandleBulletHit(int damage)
    {
        _damageReceived += damage;

        for (var i = 0; i < _damageReceived; i++)
        {
            if (i < _activatedObjects.Count && _activatedObjects[i].activeSelf)
            {
                _activatedObjects[i].SetActive(false);
            }
        }

        if (_damageReceived >= _activatedObjects.Count)
        {
            StartCoroutine(ExplodeAndDestroyPlatform());
        }
    }

    private void OnDestroy()
    {
        Projectile.OnBulletHit -= HandleBulletHit;
        StartCoroutine(ExplodeAndDestroyPlatform());
    }
    
    private IEnumerator ExplodeAndDestroyPlatform()
    {
        explosionParticleSystem.Play();

        yield return new WaitForSeconds(0.5f);

        _platformSpawner.DestroyPlatform();
        explosionParticleSystem.Stop();
    }

    private void ActivateRandomObjects()
    {
        var objectsCount = Random.Range(1, 6);

        for (var i = 0; i < objectsCount; i++)
        {
            if (i < objectsToActivate.Length)
            {
                var objectToActivate = objectsToActivate[i];
                objectToActivate.SetActive(true);
                _activatedObjects.Add(objectToActivate);
            }
        }
    }
}
