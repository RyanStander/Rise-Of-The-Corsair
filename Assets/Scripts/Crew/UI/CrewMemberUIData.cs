using System;
using System.Collections.Generic;
using Crew.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Crew
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

            //TODO: Set role dropdown
        }

        public void SetMainStats(List<CrewStats> mainStats)
        {

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
