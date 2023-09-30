using UnityEngine;
using UnityEngine.Serialization;

namespace Database.SettingsDatabase.Impl
{
    [CreateAssetMenu(menuName = "Database/SettingsDatabase", fileName = "SettingsDatabase")]
    public class SettingsDatabase : ScriptableObject
    { 
        [Header("BallScaleConfigs")] 
        [Tooltip("Таймер утримання курсору на шарі")]
        [SerializeField] 
        public float delayTime;
    
        [FormerlySerializedAs("reductionPercent")]
        [Tooltip("Максимальна місткість шару (1 = 100%)")]
        [SerializeField] 
        public float scalePercent;
    
        [Tooltip("Скейл шару (x,y,z)")]
        [SerializeField] 
        public float originalScale;

        [Tooltip("Зменьшення шару за заряд (0.1 = 10%)")]
        [SerializeField] 
        public float scaleReduction;
        
        [Tooltip("Скейл для програшу")]
        [SerializeField] 
        public float scaleChangeLimit;
    }
}
