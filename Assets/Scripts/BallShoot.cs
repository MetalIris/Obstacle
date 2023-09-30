using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class BallShoot : MonoBehaviour
{
    [Inject] private DiContainer _container;

    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Image[] _chargeBar;
    [SerializeField] private Transform _spawnPoint;

    private int _currentChargeIndex;
    private GameObject _projectileSpawned;

    private void Start()
    {
        ResetCharge();
    }

    public void ResetCharge()
    {
        _currentChargeIndex = 0;
        foreach (var chargeImage in _chargeBar)
        {
            chargeImage.fillAmount = 0f;
            chargeImage.DOKill();
        }
    }

    public void FillCharge()
    {
        if (_currentChargeIndex >= _chargeBar.Length) return;
        _chargeBar[_currentChargeIndex].DOFillAmount(1f, 0.4f).SetEase(Ease.Linear);
        _currentChargeIndex++;
    }

    public void Shoot(int charges)
    {
        StartCoroutine(ShootWithDelay(charges));
        ResetCharge();
        Debug.Log(charges + " charges were shot");
    }

    private IEnumerator ShootWithDelay(int charge)
    {
        _projectileSpawned.GetComponent<Projectile>().Shoot(charge);
        yield return new WaitForSeconds(2.0f);
    }

    public void ProjectileAppear()
    {
        _projectileSpawned = Instantiate(_projectilePrefab, _spawnPoint.position, _spawnPoint.rotation);
        _container.InjectGameObject(_projectileSpawned);
        _projectileSpawned.transform.DOScale(Vector3.one * 0.2f, 0.1f).SetEase(Ease.Flash);
    }

    public void ProjectileScaleUp()
    {
        if (_projectileSpawned != null)
        {
            _projectileSpawned.transform.DOScale(_projectileSpawned.transform.localScale + Vector3.one * 0.1f, 0.4f);
        }
    }
}
