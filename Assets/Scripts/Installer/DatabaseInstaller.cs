using Database.SettingsDatabase.Impl;
using UnityEngine;
using Zenject;

namespace Installer
{
    [CreateAssetMenu(fileName = "DatabaseInstaller", menuName = "Installers/DatabaseInstaller")]
    public class DatabaseInstaller : ScriptableObjectInstaller<DatabaseInstaller>
    {
        [SerializeField] private SettingsDatabase settingsDatabase;
        public override void InstallBindings()
        {
            Container.BindInstance(settingsDatabase);
        }
    }
}