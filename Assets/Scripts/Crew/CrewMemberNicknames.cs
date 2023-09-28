using System.Collections.Generic;
using UnityEngine;

namespace Crew
{
    public class CrewMemberNicknames
    {
        public string[] EnglishNicknames;
        public string[] DutchNicknames;
        public string[] SpanishNicknames;
        public string[] FrenchNicknames;

        public static CrewMemberNicknames CreateFromJson(TextAsset textAsset)
        {
            var crewMemberNicknames = JsonUtility.FromJson<CrewMemberNicknames>(textAsset.text);

            return crewMemberNicknames;
        }
    }
}
