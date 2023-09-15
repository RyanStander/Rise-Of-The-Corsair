using System;
using Crew.Enums;
using UnityEngine;

namespace Crew
{
    [CreateAssetMenu(fileName = "CrewSprites", menuName = "Scriptables/Crew/Sprites", order = 0)]
    public class CrewSprites : ScriptableObject
    {
        public Sprite StrengthSprite;
        public Sprite AgilitySprite;
        public Sprite MarksmanshipSprite;
        public Sprite SailingSprite;
        public Sprite RepairSprite;
        public Sprite MedicineSprite;
        public Sprite LeadershipSprite;
        public Sprite NavigationSprite;
        public Sprite CookingSprite;

        public Sprite GetSprite(CrewStats specialization)
        {
            return specialization switch
            {
                CrewStats.Strength => StrengthSprite,
                CrewStats.Agility => AgilitySprite,
                CrewStats.Marksmanship => MarksmanshipSprite,
                CrewStats.Sailing => SailingSprite,
                CrewStats.Repair => RepairSprite,
                CrewStats.Medicine => MedicineSprite,
                CrewStats.Leadership => LeadershipSprite,
                CrewStats.Navigation => NavigationSprite,
                CrewStats.Cooking => CookingSprite,
                _ => throw new ArgumentOutOfRangeException(nameof(specialization), specialization, null)
            };
        }
    }
}
