using UnityEngine;

namespace Crew
{
    public class CrewMemberNames
    {
        public NameSet EnglishNames;
        public NameSet DutchNames;
        public NameSet SpanishNames;
        public NameSet FrenchNames;

        public static CrewMemberNames CreateFromJson(string jsonString)
        {
            return JsonUtility.FromJson<CrewMemberNames>(jsonString);
        }
    }
}
