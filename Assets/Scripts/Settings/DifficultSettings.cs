using UnityEngine;

namespace Bubbles
{
    [CreateAssetMenu(fileName = "DifficultSettings", menuName = "Bubbles/Difficult Settings", order = 0)]
    public sealed class DifficultSettings : ScriptableObject
    {
        [SerializeField] private int maxAmount;
        [SerializeField] private float spawnDelay;
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float finalSpeedFactor;
        [SerializeField] private float minRadius;
        [SerializeField] private float maxRadius;

        public float MaxAmount => maxAmount;
        
        public float SpawnDelay => spawnDelay;
        
        public float MinSpeed => minSpeed;

        public float MaxSpeed => maxSpeed;

        public float FinalSpeedFactor => finalSpeedFactor;

        public float MinRadius => minRadius;

        public float MaxRadius => maxRadius;
    }
}