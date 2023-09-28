using System;
using System.Collections.Generic;
using Crew.Enums;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Crew
{
    public abstract class CrewMemberCreator
    {
        private const int MaxStat = 40;
        private const int SpecialityBonus = 15;

        public static CrewMemberStats GenerateCrewMemberStats(int currentLevel, CrewLevelData crewLevelData,
            TextAsset crewMemberNamesAsset, TextAsset crewMemberNicknamesAsset)
        {
            var crewMemberStats = new CrewMemberStats();

            crewMemberStats.Speciality = RandomSpeciality();

            crewMemberStats.Level = currentLevel;

            //TODO: change this to faction based on location or ship obtained from
            crewMemberStats.Name = GetRandomName(Faction.English, crewMemberNamesAsset);
            crewMemberStats.Nickname = GetRandomNickname(Faction.English, crewMemberNicknamesAsset);

            crewMemberStats.AssignedNonCombatRole = (NonCombatRole) Random.Range(0, 8);
            crewMemberStats.AssignedNavalCombatRole = (NavalCombatRole) Random.Range(0, 8);
            crewMemberStats.AssignedBoardingRole = (BoardingRole) Random.Range(0, 3);

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

        private static CrewStats RandomSpeciality()
        {
            //Select a speciality at random
            var randomSpeciality = Random.Range(0, 9);

            return randomSpeciality switch
            {
                0 => CrewStats.Strength,
                1 => CrewStats.Agility,
                2 => CrewStats.Marksmanship,
                3 => CrewStats.Sailing,
                4 => CrewStats.Repair,
                5 => CrewStats.Medicine,
                6 => CrewStats.Leadership,
                7 => CrewStats.Navigation,
                8 => CrewStats.Cooking,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

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
            //Give a bonus to the speciality
            switch (crewMemberStats.Speciality)
            {
                case CrewStats.Strength:
                    crewMemberStats.Strength += SpecialityBonus;
                    break;
                case CrewStats.Agility:
                    crewMemberStats.Agility += SpecialityBonus;
                    break;
                case CrewStats.Marksmanship:
                    crewMemberStats.Marksmanship += SpecialityBonus;
                    break;
                case CrewStats.Sailing:
                    crewMemberStats.Sailing += SpecialityBonus;
                    break;
                case CrewStats.Repair:
                    crewMemberStats.Repair += SpecialityBonus;
                    break;
                case CrewStats.Medicine:
                    crewMemberStats.Medicine += SpecialityBonus;
                    break;
                case CrewStats.Leadership:
                    crewMemberStats.Leadership += SpecialityBonus;
                    break;
                case CrewStats.Navigation:
                    crewMemberStats.Navigation += SpecialityBonus;
                    break;
                case CrewStats.Cooking:
                    crewMemberStats.Cooking += SpecialityBonus;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            for (var i = 0; i < crewMemberStats.Level; i++)
            {
                var unMaxedStats = GetUnMaxedStats(crewMemberStats);

                if (unMaxedStats.Contains(CrewStats.Strength))
                    crewMemberStats.Strength++;
                if (unMaxedStats.Contains(CrewStats.Agility))
                    crewMemberStats.Agility++;
                if (unMaxedStats.Contains(CrewStats.Marksmanship))
                    crewMemberStats.Marksmanship++;
                if (unMaxedStats.Contains(CrewStats.Sailing))
                    crewMemberStats.Sailing++;
                if (unMaxedStats.Contains(CrewStats.Repair))
                    crewMemberStats.Repair++;
                if (unMaxedStats.Contains(CrewStats.Medicine))
                    crewMemberStats.Medicine++;
                if (unMaxedStats.Contains(CrewStats.Leadership))
                    crewMemberStats.Leadership++;
                if (unMaxedStats.Contains(CrewStats.Navigation))
                    crewMemberStats.Navigation++;
                if (unMaxedStats.Contains(CrewStats.Cooking))
                    crewMemberStats.Cooking++;

                for (var j = 0; j < 2; j++)
                {
                    unMaxedStats = GetUnMaxedStats(crewMemberStats);

                    //randomly select a stat to increase
                    var randomStat = Random.Range(0, unMaxedStats.Count);

                    var selectedStat = unMaxedStats[randomStat];

                    switch (selectedStat)
                    {
                        case CrewStats.Strength:
                            crewMemberStats.Strength++;
                            break;
                        case CrewStats.Agility:
                            crewMemberStats.Agility++;
                            break;
                        case CrewStats.Marksmanship:
                            crewMemberStats.Marksmanship++;
                            break;
                        case CrewStats.Sailing:
                            crewMemberStats.Sailing++;
                            break;
                        case CrewStats.Repair:
                            crewMemberStats.Repair++;
                            break;
                        case CrewStats.Medicine:
                            crewMemberStats.Medicine++;
                            break;
                        case CrewStats.Leadership:
                            crewMemberStats.Leadership++;
                            break;
                        case CrewStats.Navigation:
                            crewMemberStats.Navigation++;
                            break;
                        case CrewStats.Cooking:
                            crewMemberStats.Cooking++;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            return crewMemberStats;
        }

        private static List<CrewStats> GetUnMaxedStats(CrewMemberStats crewMemberStats)
        {
            //Any stat above 40 is considered maxed out
            var unMaxedStats = new List<CrewStats>();

            //check if any stats are not max level
            if (crewMemberStats.Strength < MaxStat)
                unMaxedStats.Add(CrewStats.Strength);
            if (crewMemberStats.Agility < MaxStat)
                unMaxedStats.Add(CrewStats.Agility);
            if (crewMemberStats.Marksmanship < MaxStat)
                unMaxedStats.Add(CrewStats.Marksmanship);
            if (crewMemberStats.Sailing < MaxStat)
                unMaxedStats.Add(CrewStats.Sailing);
            if (crewMemberStats.Repair < MaxStat)
                unMaxedStats.Add(CrewStats.Repair);
            if (crewMemberStats.Medicine < MaxStat)
                unMaxedStats.Add(CrewStats.Medicine);
            if (crewMemberStats.Leadership < MaxStat)
                unMaxedStats.Add(CrewStats.Leadership);
            if (crewMemberStats.Navigation < MaxStat)
                unMaxedStats.Add(CrewStats.Navigation);
            if (crewMemberStats.Cooking < MaxStat)
                unMaxedStats.Add(CrewStats.Cooking);

            return unMaxedStats;
        }
    }
}
