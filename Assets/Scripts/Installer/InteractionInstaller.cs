using UnityEngine;
using Zenject;

namespace Installer
{
    public class InteractionInstaller : MonoInstaller
    {
        [SerializeField] private BallShoot _ballShoot;
        [SerializeField] private AnimationManager _animationManager;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private PlatformSpawner _platformSpawner;
        public override void InstallBindings()
        {
            Container.Bind<BallShoot>()
                .FromInstance(_ballShoot)
                .AsSingle();
            Container.Bind<AnimationManager>()
                .FromInstance(_animationManager)
                .AsSingle();
            Container.Bind<Projectile>()
                .FromInstance(_projectile)
                .AsSingle();
            Container.Bind<PlatformSpawner>()
                .FromInstance(_platformSpawner)
                .AsSingle();
        }
    }
}