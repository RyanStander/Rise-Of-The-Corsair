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

        public void SetCrewUI(CrewMemberStats crewMemberStats)
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

            var mainStats = new List<CrewStats>
                { CrewMainStats.MainNonCombatStat[crewMemberStats.AssignedNonCombatRole] };
            SetMainStats(mainStats);

            moraleSlider.value = crewMemberStats.Morale;

            SetDropdownValues();

            roleDropdown.onValueChanged.AddListener(RoleDropdownValueChanged);
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

        private void RoleDropdownValueChanged(int value)
        {
            var nonCombatRole = (NonCombatRole)value;

            CrewMemberStats.AssignedNonCombatRole = nonCombatRole;

            var mainStats = new List<CrewStats> { CrewMainStats.MainNonCombatStat[nonCombatRole] };

            SetMainStats(mainStats);

            EventManager.currentManager.AddEvent(new SortCrewMember(CrewMemberStats));
        }

        private void SetDropdownValues()
        {
            roleDropdown.ClearOptions();

            //set the options of the dropdown to all non combat roles
            var options = new List<TMP_Dropdown.OptionData>();
            foreach (var nonCombatRole in Enum.GetValues(typeof(NonCombatRole)))
            {
                options.Add(new TMP_Dropdown.OptionData(nonCombatRole.ToString()));
            }

            roleDropdown.AddOptions(options);

            //set the value of the dropdown to the index of the assigned non combat role
            roleDropdown.value = (int)CrewMemberStats.AssignedNonCombatRole;
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
