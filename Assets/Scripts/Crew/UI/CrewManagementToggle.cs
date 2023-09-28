using UnityEngine;

namespace Crew.UI
{
    public class CrewManagementToggle : MonoBehaviour
    {
        [SerializeField] private GameObject crewManagementPanel;

        // Update is called once per frame
        private void Update()
        {
            //when c is pressed, toggle the crew management panel
            if (!Input.GetKeyUp(KeyCode.C))
                return;
            
            crewManagementPanel.SetActive(!crewManagementPanel.activeSelf);

            //if the crew management panel is active, pause the game
            Time.timeScale = crewManagementPanel.activeSelf ? 0 : 1;
        }
    }
}
