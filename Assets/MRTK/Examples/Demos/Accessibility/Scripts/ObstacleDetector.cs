// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Examples.Demos
{
    public class ObstacleDetector : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Time interval of sonars.")]
        private float updateInterval = 1f;

        [SerializeField]
        [Range(1f, 100f)]
        [Tooltip("Radial density of sonar. Higher numbers lead to more precision at the cost of performance.")]
        private float radialDensity = 10f;

        [SerializeField]
        [Range(1f, 100f)]
        [Tooltip("Circular density of sonar. Higher numbers lead to more precision at the cost of performance.")]
        private float circularDensity = 10f;

        [SerializeField]
        [Range(0.0f, 89.9f)]
        [Tooltip("Maximum angle of the sonar in degrees. Higher numbers lead to more precision at the cost of performance.")]
        private float maxAngle = 20f;


        [SerializeField]
        [Tooltip("Whether items located at a certain distance above the user should be ignored.")]
        private bool ignoreCeilings = true;

        [SerializeField]
        [Tooltip("Whether draw debug ray in editor play mode.")]
        private bool drawDebugRay = false;

        [SerializeField]
        [Tooltip("Maximum distance above the camera under which obstacles are still reported.")]
        private float ceilingOffsetFromHead = 0.2f;

        [SerializeField]
        [Tooltip("Required TextToSpeech Script in order to give audio feedback to user commands.")]
        private TextToSpeech textToSpeech = null;

        [SerializeField]
        [Tooltip("Required reference to AudioSource compoent attached to another gameobject in order to give user spatialized audio feedback in sonar.")]
        private AudioSource audioSource = null;

        private float lastUpdateTime;


        #region MonoBehaviour Implementation

        void Start()
        {
            if (textToSpeech == null)
            {
                Debug.LogError("ObstacleDetector requires a TextToSpeech Script in order to give audio feedback to user commands!");
                enabled = false;
            }
            if (audioSource == null)
            {
                Debug.LogError("ObstacleDetector requires a reference to AudioSource compoent attached to another gameobject!");
                enabled = false;
            }
        }

        void Update()
        {
            if (Time.time - lastUpdateTime > updateInterval)
            {
                var obstacles = SendSonar();
                GameObject closestObstacle = null;
                float closestDist = Mathf.Infinity;
                Vector3 hitPoint = Vector3.zero;

                foreach (var obstacle in obstacles)
                {
                    if (obstacle.collider != null)
                    {
                        // Check if need to ignore as the point is on a ceiling
                        if (ignoreCeilings && obstacle.point.y > Camera.main.transform.position.y + ceilingOffsetFromHead)
                        {
                            continue;
                        }
                        // Obtain the closest hit point
                        if (obstacle.distance < closestDist)
                        {
                            closestDist = obstacle.distance;
                            closestObstacle = obstacle.collider.gameObject;
                            hitPoint = obstacle.point;
                        }

                    }
                }

                // Play spatial sound
                if (audioSource != null && closestObstacle != null)
                {
                    audioSource.transform.position = hitPoint;
                    audioSource.Play();
                }
                lastUpdateTime = Time.time;
            }
        }

        #endregion MonoBehaviour Implementation

        #region Private Functions

        private List<RaycastHit> SendSonar()
        {
            List<RaycastHit> hits = new List<RaycastHit>();

            // Perform multiple raycast to achieve a "conecast"
            hits.Add(performRaycast(Vector3.zero));
            for (float i = Mathf.Tan(maxAngle * Mathf.PI / 180f); i > 0; i -= 1 / radialDensity)
            {
                for (float j = 0; j < 2f * Mathf.PI; j += 2 / circularDensity * Mathf.PI)
                {
                    hits.Add(performRaycast((Camera.main.transform.up * Mathf.Sin(j) + Camera.main.transform.right * Mathf.Cos(j)) * i));
                }
            }

            return hits;
        }

        private RaycastHit performRaycast(Vector3 offset)
        {
            RaycastHit hit;
            if (UnityEngine.Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward + offset, out hit, Mathf.Infinity, UnityEngine.Physics.AllLayers))
            {
                if (drawDebugRay)
                {
                    Debug.DrawRay(Camera.main.transform.position, (Camera.main.transform.forward + offset) * hit.distance, Color.yellow);
                }

            }
            else
            {
                if (drawDebugRay)
                {
                    Debug.DrawRay(Camera.main.transform.position, (Camera.main.transform.forward + offset) * 100, Color.white);
                }
            }
            return hit;

        }

        #endregion Private Functions

        #region Public Functions

        public void IncreaseSonarInterval()
        {
            updateInterval += 0.5f;
            textToSpeech.StartSpeaking("Sonar interval increased to " + updateInterval.ToString("F1") + "second");
        }

        public void DecreaseSonarInterval()
        {
            updateInterval -= 0.5f;
            if (updateInterval < 0.1f)
            {
                updateInterval = 0.1f;
            }
            textToSpeech.StartSpeaking("Sonar interval decreased to " + updateInterval.ToString("F1") + "second");
        }

        public void IncreaseRadialDensity()
        {
            radialDensity += 5f;
            if (radialDensity > 100f)
            {
                radialDensity = 100f;
            }
            textToSpeech.StartSpeaking("Radial density increased to " + radialDensity.ToString("F1"));
        }

        public void DecreaseRadialDensity()
        {
            radialDensity -= 5f;
            if (radialDensity < 1f)
            {
                radialDensity = 1f;
            }
            textToSpeech.StartSpeaking("Radial density decreased to " + radialDensity.ToString("F1"));
        }

        public void IncreaseCircularDensity()
        {
            circularDensity += 5f;
            if (circularDensity > 100f)
            {
                circularDensity = 100f;
            }
            textToSpeech.StartSpeaking("Circular density increased to " + circularDensity.ToString("F1"));
        }

        public void DecreaseCircularDensity()
        {
            circularDensity -= 5f;
            if (circularDensity < 1f)
            {
                circularDensity = 1f;
            }
            textToSpeech.StartSpeaking("Circular density decreased to " + circularDensity.ToString("F1"));
        }

        public void IncreaseAngle()
        {
            maxAngle += 5f;
            if (maxAngle > 89.9f)
            {
                maxAngle = 89.9f;
            }
            textToSpeech.StartSpeaking("Sonar angle increased to " + maxAngle.ToString("F1"));
        }

        public void DecreaseAngle()
        {
            maxAngle -= 5f;
            if (maxAngle < 0f)
            {
                maxAngle = 0f;
            }
            textToSpeech.StartSpeaking("Sonar angle decreased to " + maxAngle.ToString("F1"));
        }

        public void AllCommands()
        {
            textToSpeech.StartSpeaking(@"Following commands are available: enable or disable sonar, increase or decrease sonar angle,
            increase or decrease sonar interval, increase or decrease circular density, increase or decrease radial density.");
        }


        #endregion Public Functions
    }
}