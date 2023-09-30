using UnityEngine;
using Zenject;

public class Projectile : MonoBehaviour
{
    [Inject] private AnimationManager _animationManager;

    public delegate void BulletHitAction(int damage);
    public static event BulletHitAction OnBulletHit;

    [SerializeField] private int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("ExplosivePlatform")) return;

        OnBulletHit?.Invoke(damage);

        Destroy(gameObject);
    }

    public void Shoot(int charges)
    {
        damage = charges;
        _animationManager.ShootAnimation(transform);
    }
}
