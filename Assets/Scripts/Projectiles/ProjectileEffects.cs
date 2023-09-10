using UnityEngine;

namespace Projectiles
{
    [CreateAssetMenu(menuName = "Scriptables/Projectiles/ProjectileEffects")]
    public class ProjectileEffects : ScriptableObject
    {
        [field: SerializeField] public GameObject hitEffectOnWater { get; private set; }
        [field: SerializeField] public GameObject hitEffectOnLand { get; private set; }
        [field: SerializeField] public GameObject hitEffectOnCannon { get; private set; }
        [field: SerializeField] public GameObject hitEffectOnMast { get; private set; }
        [field: SerializeField] public GameObject hitEffectOnSail { get; private set; }
        [field: SerializeField] public GameObject hitEffectOnHull { get; private set; }
        [field: SerializeField] public GameObject hitEffectOnCrew { get; private set; }
    }
}
