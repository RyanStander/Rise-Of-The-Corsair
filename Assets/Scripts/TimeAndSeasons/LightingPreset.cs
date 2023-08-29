using UnityEngine;

namespace TimeAndSeasons
{
    [System.Serializable]
    [CreateAssetMenu(menuName ="Scriptables/Lighting Preset")]
    public class LightingPreset : ScriptableObject
    {
        public Gradient ambientColor;
        public Gradient directionalColor;
        public Gradient fogColor;

    }
}
