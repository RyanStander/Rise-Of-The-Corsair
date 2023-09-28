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
            if (Input.GetKeyUp(KeyCode.C))
            {
                crewManagementPanel.SetActive(!crewManagementPanel.activeSelf);
            }
        }
    }
}
