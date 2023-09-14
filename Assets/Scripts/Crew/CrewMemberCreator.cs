using System;
using System.Collections.Generic;
using Crew.Enums;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Crew
{
    public class CrewMemberCreator
    {
        public static CrewMemberStats GenerateCrewMemberStats(int minLevel, CrewLevelData crewLevelData, TextAsset crewMemberNamesAsset, TextAsset crewMemberNicknamesAsset)
        {
            var crewMemberStats = new CrewMemberStats();

            //TODO: This needs some form of random
            crewMemberStats.Level = minLevel;

            //TODO: change this to faction based on location or ship obtained from
            crewMemberStats.Name = GetRandomName(Faction.Dutch,crewMemberNamesAsset);
            crewMemberStats.Nickname = GetRandomNickname(Faction.Dutch,crewMemberNicknamesAsset);

            crewMemberStats.Health = RandomHealth();

            crewMemberStats.Rank = DetermineRank(crewMemberStats.Level);

            crewMemberStats.Experience = CrewLevelSystem.GetXpForCurrentLevel(crewLevelData, crewMemberStats.Level);

            //TODO: add traits and specialisation functionality

            crewMemberStats = SetupAttributes(crewMemberStats);

            return crewMemberStats;
        }

        #region Name Generation

        private static string GetRandomName(Faction faction, TextAsset crewMemberNamesAsset)
        {
            //convert the json file to a CrewMemberNames object
            var crewMemberNames = CrewMemberNames.CreateFromJson(crewMemberNamesAsset);

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

        private static string GetRandomNickname(Faction faction, TextAsset crewMemberNicknamesAsset)
        {
            //convert the json file to a CrewMemberNicknames object
            var crewMemberNicknames = CrewMemberNicknames.CreateFromJson(crewMemberNicknamesAsset);

            return faction switch
            {
                Faction.English => crewMemberNicknames.EnglishNicknames[
                    Random.Range(0, crewMemberNicknames.EnglishNicknames.Length)],
                Faction.Dutch => crewMemberNicknames.DutchNicknames[
                    Random.Range(0, crewMemberNicknames.DutchNicknames.Length)],
                Faction.Spanish => crewMemberNicknames.SpanishNicknames[
                    Random.Range(0, crewMemberNicknames.SpanishNicknames.Length)],
                Faction.French => crewMemberNicknames.FrenchNicknames[
                    Random.Range(0, crewMemberNicknames.FrenchNicknames.Length)],
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
        private static CrewMemberHealth RandomHealth()
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
        private static CrewMemberStats SetupAttributes(CrewMemberStats crewMemberStats)
        {

            for (var i = 0; i < crewMemberStats.Level; i++)
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
