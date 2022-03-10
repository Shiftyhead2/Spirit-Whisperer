using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FPS_models
{

    #region - Player
    [Serializable]
    public class PlayerSettingsModel
    {
        [Header("View Settings")]
        public float ViewXSensitivity;
        public float ViewYSensitivity;

        public bool ViewXInverted;
        public bool ViewYInverted;


        [Header("Movement Settings")]
        public bool sprintHold;
        public float MovementSmoothing;



        [Header("Movement Running Settings")]
        public float RunningForwardSpeed;
        public float RunningStrafeSpeed;

        [Header("Movement Walking Settings")]
        public float WalkingForwardSpeed;
        public float WalkingStrafeSpeed;
    }
    #endregion
}
