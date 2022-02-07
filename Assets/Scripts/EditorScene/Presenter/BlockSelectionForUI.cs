using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using EAR.Selection;

namespace EAR.Editor.Presenter
{
    public class BlockSelectionForUI : MonoBehaviour
    {
        [SerializeField]
        private SelectionManager selectionManager;
        [SerializeField]
        private GraphicRaycaster raycaster;

        private EventSystem eventSystem;


        void Awake()
        {
            eventSystem = FindObjectOfType<EventSystem>();
            if (eventSystem == null)
            {
                Debug.LogError("No Event System!");
            }
        }

        void Start()
        {
            if (selectionManager != null && raycaster != null)
            {
                selectionManager.CheckMouseRaycastBlocked += CheckIfRaycastBlockedByUI;
            }
        }

        private void CheckIfRaycastBlockedByUI(ref bool isBlocked)
        {
            if (eventSystem == null)
            {
                return;
            }

            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);
            if (results.Count > 0)
            {
                isBlocked = true;
            }
        }
    }
}

