namespace Crew
{
    public class CrewMemberNames
    {
        public NameSet EnglishNames { get; private set; }
        public NameSet DutchNames { get; private set; }
        public NameSet SpanishNames { get; private set; }
        public NameSet FrenchNames { get; private set; }

        public class NameSet
        {
            public string[] MaleNames { get; private set; }
            public string[] FemaleNames { get; private set; }
            public string[] Surnames { get; private set; }
        }
    }
}
