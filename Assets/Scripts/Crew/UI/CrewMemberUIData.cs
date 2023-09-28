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

            moraleSlider.value = crewMemberStats.Morale;

            SetDropdownValues();

            roleDropdown.onValueChanged.AddListener(RoleDropdownValueChanged);
        }

        public void SetMainStats(List<CrewStats> mainStats)
        {

        }

        private void RoleDropdownValueChanged(int value)
        {
            var nonCombatRole = (NonCombatRole) value;

            CrewMemberStats.AssignedNonCombatRole = nonCombatRole;

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
            roleDropdown.value = (int) CrewMemberStats.AssignedNonCombatRole;
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
