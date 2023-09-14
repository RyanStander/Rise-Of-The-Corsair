using UnityEngine;

namespace Crew
{
    [System.Serializable]
    public class CrewMemberNames
    {
        public NameSet EnglishNames;
        public NameSet DutchNames;
        public NameSet SpanishNames;
        public NameSet FrenchNames;

        public static CrewMemberNames CreateFromJson(TextAsset textAsset)
        {
            var crewMemberNames = JsonUtility.FromJson<CrewMemberNames>(textAsset.text);

            return crewMemberNames;
        }
    }
}
