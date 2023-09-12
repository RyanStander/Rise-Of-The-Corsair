using System;
using System.Collections.Generic;
using Crew.Enums;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Crew
{
    /// <summary>
    /// Stats of a crew member, strength to cooking cannot go above 40 points base, the maximum level is 20
    /// </summary>
    public class CrewMemberStats
    {
        public string Name { get; private set; }
        public string Nickname { get; private set; }
        public CrewMemberRank Rank { get; private set; }
        public CrewMemberHealth Health { get; private set; }
        public int Strength { get; private set; }
        public int Agility { get; private set; }
        public int Marksmanship { get; private set; }
        public int Sailing { get; private set; }
        public int Repair { get; private set; }
        public int Medicine { get; private set; }
        public int Leadership { get; private set; }
        public int Navigation { get; private set; }
        public int Cooking { get; private set; }
        public float Morale { get; private set; }
        public int Level { get; private set; }
        public float Experience { get; private set; }

        private string nameFileName = "CrewMemberNames";
        private string nicknameFileName = "CrewMemberNicknames";

        public CrewMemberStats GenerateCrewMemberStats(int minLevel, CrewLevelData crewLevelData)
        {
            var crewMemberStats = new CrewMemberStats();

            //TODO: This needs some form of random
            crewMemberStats.Level = minLevel;

            //TODO: change this to faction based on location or ship obtained from
            crewMemberStats.Name = GetRandomName(Faction.Dutch);
            crewMemberStats.Nickname = GetRandomNickname(Faction.Dutch);

            crewMemberStats.Health = RandomHealth();

            crewMemberStats.Rank = DetermineRank(crewMemberStats.Level);

            crewMemberStats.Experience = CrewLevelSystem.GetXpForCurrentLevel(crewLevelData, crewMemberStats.Level);

            //TODO: add traits and specialisation functionality

            crewMemberStats = SetupAttributes(crewMemberStats);

            return crewMemberStats;
        }

        #region Name Generation

        private string GetRandomName(Faction faction)
        {
            //convert the json file to a CrewMemberNames object
            var crewMemberNames = JsonUtility.FromJson<CrewMemberNames>(nameFileName);

            //randomly decide whether crew member is male or female
            var isMale = Random.Range(0, 2) == 0;

            return faction switch
            {
                Faction.English => RandomName(
                    isMale ? crewMemberNames.EnglishNames.MaleNames : crewMemberNames.EnglishNames.FemaleNames,
                    crewMemberNames.EnglishNames.Surnames),
                Faction.Dutch => RandomName(
                    isMale ? crewMemberNames.DutchNames.MaleNames : crewMemberNames.DutchNames.FemaleNames,
                    crewMemberNames.DutchNames.Surnames),
                Faction.Spanish => RandomName(
                    isMale ? crewMemberNames.SpanishNames.MaleNames : crewMemberNames.SpanishNames.FemaleNames,
                    crewMemberNames.SpanishNames.Surnames),
                Faction.French => RandomName(
                    isMale ? crewMemberNames.FrenchNames.MaleNames : crewMemberNames.FrenchNames.FemaleNames,
                    crewMemberNames.FrenchNames.Surnames),
                Faction.Pirates => throw new ArgumentOutOfRangeException(nameof(faction), faction, null),
                _ => throw new ArgumentOutOfRangeException(nameof(faction), faction, null)
            };
        }

        private static string RandomName(IReadOnlyList<string> name, IReadOnlyList<string> surname)
        {
            return name[Random.Range(0, name.Count)] + " " + surname[Random.Range(0, surname.Count)];
        }

        private string GetRandomNickname(Faction faction)
        {
            //convert the json file to a CrewMemberNicknames object
            var crewMemberNicknames = JsonUtility.FromJson<CrewMemberNicknames>(nicknameFileName);

            return faction switch
            {
                Faction.English => crewMemberNicknames.EnglishNames[
                    Random.Range(0, crewMemberNicknames.EnglishNames.Length)],
                Faction.Dutch => crewMemberNicknames.EnglishNames[
                    Random.Range(0, crewMemberNicknames.DutchNames.Length)],
                Faction.Spanish => crewMemberNicknames.EnglishNames[
                    Random.Range(0, crewMemberNicknames.SpanishNames.Length)],
                Faction.French => crewMemberNicknames.EnglishNames[
                    Random.Range(0, crewMemberNicknames.FrenchNames.Length)],
                Faction.Pirates => throw new ArgumentOutOfRangeException(nameof(faction), faction, null),
                _ => throw new ArgumentOutOfRangeException(nameof(faction), faction, null)
            };
        }

        #endregion

        private static CrewMemberRank DetermineRank(int level)
        {
            return level switch
            {
                <= 3 => CrewMemberRank.Greenhand,
                > 3 and <= 5 => CrewMemberRank.Crewman,
                > 5 and <= 8 => CrewMemberRank.Sailor,
                > 8 and <= 10 => CrewMemberRank.Mate,
                > 10 and <= 13 => CrewMemberRank.Seafarer,
                > 13 and <= 15 => CrewMemberRank.Mariner,
                > 15 and <= 18 => CrewMemberRank.Buccaneer,
                > 18 => CrewMemberRank.Privateer
            };
        }

        /// <summary>
        /// Generate a random health threshold, it has a 1% chance to be critical health and leans towards a good health
        /// </summary>
        /// <returns>A randomly generate health</returns>
        private CrewMemberHealth RandomHealth()
        {
            //Generate a random health, never critical but leans more towards good health
            var randomHealth = Random.Range(0, 100);

            return randomHealth switch
            {
                <= 1 => CrewMemberHealth.Critical,
                > 1 and <= 25 => CrewMemberHealth.Poor,
                > 25 and <= 35 => CrewMemberHealth.Fair,
                > 35 and <= 95 => CrewMemberHealth.Good,
                _ => CrewMemberHealth.Excellent
            };
        }

        /// <summary>
        /// For each level, a crew member gains 1 point to all stats, and 2 points randomly distributed to any stat
        /// </summary>
        private CrewMemberStats SetupAttributes(CrewMemberStats crewMemberStats)
        {
            for (var i = 0; i < Level; i++)
            {
                crewMemberStats.Strength++;
                crewMemberStats.Agility++;
                crewMemberStats.Marksmanship++;
                crewMemberStats.Sailing++;
                crewMemberStats.Repair++;
                crewMemberStats.Medicine++;
                crewMemberStats.Leadership++;
                crewMemberStats.Navigation++;
                crewMemberStats.Cooking++;

                for (var j = 0; j < 2; j++)
                {
                    //randomly distribute 2 points to any stat
                    var randomStat = Random.Range(0, 9);

                    switch (randomStat)
                    {
                        case 0:
                            crewMemberStats.Strength++;
                            break;
                        case 1:
                            crewMemberStats.Agility++;
                            break;
                        case 2:
                            crewMemberStats.Marksmanship++;
                            break;
                        case 3:
                            crewMemberStats.Sailing++;
                            break;
                        case 4:
                            crewMemberStats.Repair++;
                            break;
                        case 5:
                            crewMemberStats.Medicine++;
                            break;
                        case 6:
                            crewMemberStats.Leadership++;
                            break;
                        case 7:
                            crewMemberStats.Navigation++;
                            break;
                        case 8:
                            crewMemberStats.Cooking++;
                            break;
                    }
                }
            }

            return crewMemberStats;
        }
    }
}
