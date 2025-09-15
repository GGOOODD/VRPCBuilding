using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Unity.VRTemplate
{
    /// <summary>
    /// Controls the steps in the in coaching card.
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        [Serializable]
        class Window
        {
            [SerializeField]
            public GameObject windowObject;

            [SerializeField]
            public string buttonText;
        }

        [SerializeField]
        public TextMeshProUGUI m_WindowButtonTextField;

        [SerializeField]
        List<Window> m_WindowList = new List<Window>();

        int m_CurrentWindowIndex = 0;

        public void Next()
        {
            m_WindowList[m_CurrentWindowIndex].windowObject.SetActive(false);
            m_CurrentWindowIndex = (m_CurrentWindowIndex + 1) % m_WindowList.Count;
            m_WindowList[m_CurrentWindowIndex].windowObject.SetActive(true);
            m_WindowButtonTextField.text = m_WindowList[m_CurrentWindowIndex].buttonText;
        }

        public void Previous()
        {
            m_WindowList[m_CurrentWindowIndex].windowObject.SetActive(false);
            if (m_CurrentWindowIndex == 0)
                m_CurrentWindowIndex = m_WindowList.Count - 1;
            else
                m_CurrentWindowIndex--;
            m_WindowList[m_CurrentWindowIndex].windowObject.SetActive(true);
            m_WindowButtonTextField.text = m_WindowList[m_CurrentWindowIndex].buttonText;
        }
    }
}
