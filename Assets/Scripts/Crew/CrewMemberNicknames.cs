using UnityEngine;

namespace Crew
{
    public class CrewMemberNicknames
    {
        public string[] EnglishNames;
        public string[] DutchNames;
        public string[] SpanishNames;
        public string[] FrenchNames;

        public static CrewMemberNicknames CreateFromJson(string jsonString)
        {
            return JsonUtility.FromJson<CrewMemberNicknames>(jsonString);
        }
    }
}
