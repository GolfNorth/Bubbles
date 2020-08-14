using UnityEngine;

namespace Bubbles
{
    [CreateAssetMenu(fileName = "ScoreSettings", menuName = "Bubbles/Score Settings", order = 2)]
    public sealed class ScoreSettings : ScriptableObject
    {
        [SerializeField] private float minPoints;
        [SerializeField] private float maxPoints;

        public float MinPoints => minPoints;
        public float MaxPoints => maxPoints;
    }
}