using System.Collections.Generic;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Crew.UI
{
    public class CrewRecruitment : MonoBehaviour
    {
        [SerializeField] private Image crewSpecializationImage;

        [Header("Text fields")] [SerializeField]
        private TextMeshProUGUI nameText;

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

        [Header("Slider fields")] [SerializeField]
        private Slider strengthSlider;

        [SerializeField] private Slider agilitySlider;
        [SerializeField] private Slider marksmanshipSlider;
        [SerializeField] private Slider sailingSlider;
        [SerializeField] private Slider repairSlider;
        [SerializeField] private Slider medicineSlider;
        [SerializeField] private Slider leadershipSlider;
        [SerializeField] private Slider navigationSlider;
        [SerializeField] private Slider cookingSlider;
        [SerializeField] private Slider moraleSlider;

        [Header("Button fields")] [SerializeField]
        private Button recruitButton;

        [SerializeField] private Button previousButton;
        [SerializeField] private Button nextButton;

        [Header("Crew member data")] [Range(1, 20), SerializeField]
        private int crewMemberLevel;

        [SerializeField] private TextAsset crewMemberNamesAsset;
        [SerializeField] private TextAsset crewMemberNicknamesAsset;
        [SerializeField] private CrewLevelData crewLevelData;
        [SerializeField] private CrewSprites crewSprites;

        private List<CrewMemberStats> generatedCrewMembers = new List<CrewMemberStats>();
        private int currentCrewMemberIndex = 0;

        private void Start()
        {
            GenerateCrewMember();

            SetCrewUI(generatedCrewMembers[currentCrewMemberIndex]);
        }

        public void NextCrewMember()
        {
            currentCrewMemberIndex++;

            //check if is in range
            if (currentCrewMemberIndex >= generatedCrewMembers.Count)
            {
                GenerateCrewMember();
            }

            SetCrewUI(generatedCrewMembers[currentCrewMemberIndex]);
        }

        public void PreviousCrewMember()
        {
            currentCrewMemberIndex--;

            //check if is in range
            if (currentCrewMemberIndex < 0)
            {
                currentCrewMemberIndex = generatedCrewMembers.Count - 1;
            }

            SetCrewUI(generatedCrewMembers[currentCrewMemberIndex]);
        }

        public void RecruitCrewMember()
        {
            EventManager.currentManager.AddEvent(new RecruitCrewMember(generatedCrewMembers[currentCrewMemberIndex]));

            //remove crew member from list
            generatedCrewMembers.RemoveAt(currentCrewMemberIndex);
            currentCrewMemberIndex--;

            if (generatedCrewMembers.Count == 0)
            {
                GenerateCrewMember();
                currentCrewMemberIndex = 0;
            }
            else
            {
                if (currentCrewMemberIndex < 0)
                    currentCrewMemberIndex = generatedCrewMembers.Count - 1;
            }

            SetCrewUI(generatedCrewMembers[currentCrewMemberIndex]);
        }

        private void GenerateCrewMember()
        {
            generatedCrewMembers.Add(CrewMemberCreator.GenerateCrewMemberStats(crewMemberLevel, crewLevelData,
                crewMemberNamesAsset, crewMemberNicknamesAsset));
        }

        private void SetCrewUI(CrewMemberStats crewMemberStats)
        {
            crewSpecializationImage.sprite = crewSprites.GetSprite(crewMemberStats.Speciality);

            nameText.text = crewMemberStats.Name;
            nicknameText.text = crewMemberStats.Nickname;
            rankText.text = crewMemberStats.Rank.ToString();
            healthText.text = crewMemberStats.Health.ToString();

            strengthText.text = crewMemberStats.Strength.ToString();
            strengthSlider.value = crewMemberStats.Strength;
            agilityText.text = crewMemberStats.Agility.ToString();
            agilitySlider.value = crewMemberStats.Agility;
            marksmanshipText.text = crewMemberStats.Marksmanship.ToString();
            marksmanshipSlider.value = crewMemberStats.Marksmanship;
            sailingText.text = crewMemberStats.Sailing.ToString();
            sailingSlider.value = crewMemberStats.Sailing;
            repairText.text = crewMemberStats.Repair.ToString();
            repairSlider.value = crewMemberStats.Repair;
            medicineText.text = crewMemberStats.Medicine.ToString();
            medicineSlider.value = crewMemberStats.Medicine;
            leadershipText.text = crewMemberStats.Leadership.ToString();
            leadershipSlider.value = crewMemberStats.Leadership;
            navigationText.text = crewMemberStats.Navigation.ToString();
            navigationSlider.value = crewMemberStats.Navigation;
            cookingText.text = crewMemberStats.Cooking.ToString();
            cookingSlider.value = crewMemberStats.Cooking;
            moraleSlider.value = crewMemberStats.Morale;
        }
    }
}
