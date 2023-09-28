namespace Crew
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Scriptables/CrewLevelData")]
    public class CrewLevelData : ScriptableObject
    {
        public int MaxLevel;
        public int MaxXP;
        public AnimationCurve LevelCurve;
    }
}
