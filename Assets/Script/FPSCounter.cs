using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Utility
{

    public class FPSCounter : MonoBehaviour
    {
        const float fpsMeasurePeriod = 0.5f;
        private int m_FpsAccumulator = 0;
        private float m_FpsNextPeriod = 0;
        private int m_CurrentFps;
        const string display = "{0} FPS";
        private string m_Text;


        private void Start()
        {
            m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
            //m_Text = GetComponent<Text>();
        }


        private void Update()
        {
            // measure average frames per second
            m_FpsAccumulator++;
            if (Time.realtimeSinceStartup > m_FpsNextPeriod)
            {
                m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
                m_FpsAccumulator = 0;
                m_FpsNextPeriod += fpsMeasurePeriod;
                m_Text = string.Format(display, m_CurrentFps);
            }
        }
        void OnGUI()
        {
            GUIStyle style_tatle = new GUIStyle();
            style_tatle.normal.textColor = Color.yellow;
            style_tatle.fontSize = 25;

            GUI.Label(new Rect(Screen.width / 2, 10, 180, 80), m_Text, style_tatle);
        }
    }



}
