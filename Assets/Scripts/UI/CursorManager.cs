using System;
using UI.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CursorManager : MonoBehaviour
    {
        [SerializeField] private Image cursor;
        private bool customCursorEnabled;
        private Vector3 cursorOffset;

        private void Start()
        {
            SwapToDefaultCursor();
        }

        private void FixedUpdate()
        {
            if (!customCursorEnabled)
                return;

            cursor.transform.position = Input.mousePosition - cursorOffset;
        }

        public void SwapCursor(CursorTypes cursorType)
        {
            switch (cursorType)
            {
                case CursorTypes.Aim:
                    SwapToAimCursor();
                    break;
                case CursorTypes.Default:
                    SwapToDefaultCursor();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cursorType), cursorType, null);
            }
        }

        private void SwapToAimCursor()
        {
            DisableAll();
            customCursorEnabled = true;
            cursor.enabled = true;

            cursorOffset = Vector3.zero;
        }

        private void SwapToDefaultCursor()
        {
            DisableAll();
            Cursor.visible = true;

            cursorOffset = Vector3.zero;
        }

        private void DisableAll()
        {
            customCursorEnabled = false;
            cursor.enabled = false;
            Cursor.visible = false;
        }
    }
}
