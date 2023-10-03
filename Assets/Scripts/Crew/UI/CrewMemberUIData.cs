using System;
using System.Collections.Generic;
using Crew.Enums;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Crew.UI
{
    public class CrewMemberUIData : MonoBehaviour
    {
        [SerializeField] private CrewSprites crewSprites;
        [SerializeField] private Image crewSpecializationImage;
        [SerializeField] private TextMeshProUGUI nicknameText;

        [Header("Stats")] [SerializeField] private Image strengthImage;
        [SerializeField] private TextMeshProUGUI strengthText;
        [SerializeField] private Image agilityImage;
        [SerializeField] private TextMeshProUGUI agilityText;
        [SerializeField] private Image marksmanshipImage;
        [SerializeField] private TextMeshProUGUI marksmanshipText;
        [SerializeField] private Image sailingImage;
        [SerializeField] private TextMeshProUGUI sailingText;
        [SerializeField] private Image repairImage;
        [SerializeField] private TextMeshProUGUI repairText;
        [SerializeField] private Image medicineImage;
        [SerializeField] private TextMeshProUGUI medicineText;
        [SerializeField] private Image leadershipImage;
        [SerializeField] private TextMeshProUGUI leadershipText;
        [SerializeField] private Image navigationImage;
        [SerializeField] private TextMeshProUGUI navigationText;
        [SerializeField] private Image cookingImage;
        [SerializeField] private TextMeshProUGUI cookingText;

        [Header("Misc")] [SerializeField] private Slider moraleSlider;
        [SerializeField] private TMP_Dropdown roleDropdown;
        [SerializeField] private Color mainStatHighlightColor = Color.green;
        [SerializeField] private Color nonHighlightedColor = Color.white;

        public CrewMemberStats CrewMemberStats;

        private bool isInitialized;

        private void OnValidate()
        {
            if (crewSprites == null)
                return;

            if (strengthImage != null)
                strengthImage.sprite = crewSprites.StrengthSprite;
            if (agilityImage != null)
                agilityImage.sprite = crewSprites.AgilitySprite;
            if (marksmanshipImage != null)
                marksmanshipImage.sprite = crewSprites.MarksmanshipSprite;
            if (sailingImage != null)
                sailingImage.sprite = crewSprites.SailingSprite;
            if (repairImage != null)
                repairImage.sprite = crewSprites.RepairSprite;
            if (medicineImage != null)
                medicineImage.sprite = crewSprites.MedicineSprite;
            if (leadershipImage != null)
                leadershipImage.sprite = crewSprites.LeadershipSprite;
            if (navigationImage != null)
                navigationImage.sprite = crewSprites.NavigationSprite;
            if (cookingImage != null)
                cookingImage.sprite = crewSprites.CookingSprite;
        }

        public void SetCrewUI(CrewMemberStats crewMemberStats, CrewRoleType crewRoleType)
        {
            CrewMemberStats = crewMemberStats;

            crewSpecializationImage.sprite = crewSprites.GetSprite(crewMemberStats.Speciality);

            nicknameText.text = crewMemberStats.Nickname;

            strengthText.text = crewMemberStats.Strength.ToString();
            agilityText.text = crewMemberStats.Agility.ToString();
            marksmanshipText.text = crewMemberStats.Marksmanship.ToString();
            sailingText.text = crewMemberStats.Sailing.ToString();
            repairText.text = crewMemberStats.Repair.ToString();
            medicineText.text = crewMemberStats.Medicine.ToString();
            leadershipText.text = crewMemberStats.Leadership.ToString();
            navigationText.text = crewMemberStats.Navigation.ToString();
            cookingText.text = crewMemberStats.Cooking.ToString();

            var mainStats = new List<CrewStats>();

            switch (crewRoleType)
            {
                case CrewRoleType.NonCombatRole:
                    mainStats.Add(CrewMainStats.MainNonCombatStat[crewMemberStats.AssignedNonCombatRole]);
                    roleDropdown.onValueChanged.AddListener(NonCombatRoleDropdownValueChanged);
                    break;
                case CrewRoleType.NavalCombatRole:
                    mainStats.Add(CrewMainStats.MainNavalCombatStat[crewMemberStats.AssignedNavalCombatRole]);
                    roleDropdown.onValueChanged.AddListener(NavalCombatRoleDropdownValueChanged);
                    break;
                case CrewRoleType.BoardingRole:
                    mainStats.Add(CrewMainStats.MainBoardingStat[crewMemberStats.AssignedBoardingRole]);
                    roleDropdown.onValueChanged.AddListener(BoardingRoleDropdownValueChanged);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(crewRoleType), crewRoleType, null);
            }

            SetMainStats(mainStats);

            moraleSlider.value = crewMemberStats.Morale;

            SetDropdownValues(crewRoleType);
        }

        private void SetMainStats(ICollection<CrewStats> mainStats)
        {
            strengthImage.color = mainStats.Contains(CrewStats.Strength) ? mainStatHighlightColor : nonHighlightedColor;
            strengthText.color = mainStats.Contains(CrewStats.Strength) ? mainStatHighlightColor : nonHighlightedColor;

            agilityImage.color = mainStats.Contains(CrewStats.Agility) ? mainStatHighlightColor : nonHighlightedColor;
            agilityText.color = mainStats.Contains(CrewStats.Agility) ? mainStatHighlightColor : nonHighlightedColor;

            marksmanshipImage.color = mainStats.Contains(CrewStats.Marksmanship)
                ? mainStatHighlightColor
                : nonHighlightedColor;
            marksmanshipText.color =
                mainStats.Contains(CrewStats.Marksmanship) ? mainStatHighlightColor : nonHighlightedColor;

            sailingImage.color = mainStats.Contains(CrewStats.Sailing) ? mainStatHighlightColor : nonHighlightedColor;
            sailingText.color = mainStats.Contains(CrewStats.Sailing) ? mainStatHighlightColor : nonHighlightedColor;

            repairImage.color = mainStats.Contains(CrewStats.Repair) ? mainStatHighlightColor : nonHighlightedColor;
            repairText.color = mainStats.Contains(CrewStats.Repair) ? mainStatHighlightColor : nonHighlightedColor;

            medicineImage.color = mainStats.Contains(CrewStats.Medicine) ? mainStatHighlightColor : nonHighlightedColor;
            medicineText.color = mainStats.Contains(CrewStats.Medicine) ? mainStatHighlightColor : nonHighlightedColor;

            leadershipImage.color =
                mainStats.Contains(CrewStats.Leadership) ? mainStatHighlightColor : nonHighlightedColor;
            leadershipText.color =
                mainStats.Contains(CrewStats.Leadership) ? mainStatHighlightColor : nonHighlightedColor;

            navigationImage.color =
                mainStats.Contains(CrewStats.Navigation) ? mainStatHighlightColor : nonHighlightedColor;
            navigationText.color =
                mainStats.Contains(CrewStats.Navigation) ? mainStatHighlightColor : nonHighlightedColor;

            cookingImage.color = mainStats.Contains(CrewStats.Cooking) ? mainStatHighlightColor : nonHighlightedColor;
            cookingText.color = mainStats.Contains(CrewStats.Cooking) ? mainStatHighlightColor : nonHighlightedColor;
        }

        private void NonCombatRoleDropdownValueChanged(int value)
        {
            if (!isInitialized)
                return;

            var nonCombatRole = (NonCombatRole)value;

            CrewMemberStats.AssignedNonCombatRole = nonCombatRole;

            var mainStats = new List<CrewStats> { CrewMainStats.MainNonCombatStat[nonCombatRole] };

            SetMainStats(mainStats);

            EventManager.currentManager.AddEvent(new SortCrewMember(CrewMemberStats));
            EventManager.currentManager.AddEvent(new RecalculatePlayerCrewModifiers());
        }

        private void NavalCombatRoleDropdownValueChanged(int value)
        {
            if (!isInitialized)
                return;

            var navalCombatRole = (NavalCombatRole)value;

            CrewMemberStats.AssignedNavalCombatRole = navalCombatRole;

            var mainStats = new List<CrewStats> { CrewMainStats.MainNavalCombatStat[navalCombatRole] };

            SetMainStats(mainStats);

            EventManager.currentManager.AddEvent(new SortCrewMember(CrewMemberStats));
            EventManager.currentManager.AddEvent(new RecalculatePlayerCrewModifiers());
        }

        private void BoardingRoleDropdownValueChanged(int value)
        {
            if (!isInitialized)
                return;

            var boardingRole = (BoardingRole)value;

            CrewMemberStats.AssignedBoardingRole = boardingRole;

            var mainStats = new List<CrewStats> { CrewMainStats.MainBoardingStat[boardingRole] };

            SetMainStats(mainStats);

            EventManager.currentManager.AddEvent(new SortCrewMember(CrewMemberStats));
            EventManager.currentManager.AddEvent(new RecalculatePlayerCrewModifiers());
        }

        private void SetDropdownValues(CrewRoleType crewRoleType)
        {
            roleDropdown.ClearOptions();

            //set the options of the dropdown to all non combat roles
            var options = new List<TMP_Dropdown.OptionData>();

            var count = 8;
            if (crewRoleType == CrewRoleType.BoardingRole)
                count = 3;

            for (var i = 0; i < count; i++)
            {
                switch (crewRoleType)
                {
                    case CrewRoleType.NonCombatRole:
                        options.Add(new TMP_Dropdown.OptionData(RoleEnumToString.GetRoleString((NonCombatRole)i)));
                        break;
                    case CrewRoleType.NavalCombatRole:
                        options.Add(new TMP_Dropdown.OptionData(RoleEnumToString.GetRoleString((NavalCombatRole)i)));
                        break;
                    case CrewRoleType.BoardingRole:
                        options.Add(new TMP_Dropdown.OptionData(RoleEnumToString.GetRoleString((BoardingRole)i)));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(crewRoleType), crewRoleType, null);
                }
            }

            roleDropdown.AddOptions(options);

            //set the value of the dropdown to the index of the assigned non combat role
            roleDropdown.value = crewRoleType switch
            {
                CrewRoleType.NonCombatRole => (int)CrewMemberStats.AssignedNonCombatRole,
                CrewRoleType.NavalCombatRole => (int)CrewMemberStats.AssignedNavalCombatRole,
                CrewRoleType.BoardingRole => (int)CrewMemberStats.AssignedBoardingRole,
                _ => throw new ArgumentOutOfRangeException(nameof(crewRoleType), crewRoleType, null)
            };

            isInitialized = true;
        }
    }
}
/*
roleDropdown.ClearOptions();
            var options = new List<TMP_Dropdown.OptionData>();
            foreach (var stat in mainStats)
            {
                options.Add(new TMP_Dropdown.OptionData(stat.ToString()));
            }
            roleDropdown.AddOptions(options);
            */
