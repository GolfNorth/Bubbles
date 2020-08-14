using UnityEngine;

namespace Bubbles
{
    [CreateAssetMenu(fileName = "RoundSettings", menuName = "Bubbles/Round Settings", order = 1)]
    public sealed class RoundSettings : ScriptableObject
    {
        [SerializeField] private float countdown;
        [SerializeField] private float duration;

        public float Сountdown => countdown;

        public float Duration => duration;
    }
}