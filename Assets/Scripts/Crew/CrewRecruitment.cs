using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Crew
{
    public class CrewRecruitment : MonoBehaviour
    {
        [Header("Text fields")]
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI nicknameText;
        [SerializeField] private TextMeshProUGUI rankText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI strengthText;
        [SerializeField] private TextMeshProUGUI agilityText;
        [SerializeField] private TextMeshProUGUI marksmanshipText;
        [SerializeField] private TextMeshProUGUI sailingText;
        [SerializeField] private TextMeshProUGUI repairText;
        [SerializeField] private TextMeshProUGUI medicineText;
        [SerializeField] private TextMeshProUGUI leadershipText;
        [SerializeField] private TextMeshProUGUI navigationText;
        [SerializeField] private TextMeshProUGUI cookingText;

        [Header("Slider fields")]
        [SerializeField] private Slider moraleSlider;

        [Header("Button fields")]
        [SerializeField] private Button recruitButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button nextButton;

        [Header("Crew member data")]
        [SerializeField] private TextAsset crewMemberNamesAsset;
        [SerializeField] private TextAsset crewMemberNicknamesAsset;
        [SerializeField] private CrewLevelData crewLevelData;

        private void Start()
        {
            //Generate a new crew member
            var crewMemberStats = CrewMemberCreator.GenerateCrewMemberStats(1, crewLevelData, crewMemberNamesAsset, crewMemberNicknamesAsset);

            SetCrewUI(crewMemberStats);

        }

        private void SetCrewUI(CrewMemberStats crewMemberStats)
        {
            nameText.text = crewMemberStats.Name;
            nicknameText.text = crewMemberStats.Nickname;
            rankText.text = crewMemberStats.Rank.ToString();
            healthText.text = crewMemberStats.Health.ToString();
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
        }
    }
}
