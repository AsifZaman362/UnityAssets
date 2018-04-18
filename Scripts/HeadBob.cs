using UnityEngine;
using Controllers;

namespace Effects
{
    [RequireComponent(typeof(FPSController))]
    public class HeadBob : MonoBehaviour
    {

        /// <summary>
        /// Amount to move in respective Axis
        /// </summary>
        public float deltaX, deltaY;
        /// <summary>
        /// The rate at which displacement happens
        /// </summary>
        public float step_length;
        /// <summary>
        /// The movement curve
        /// </summary>
        public AnimationCurve m_curve;

        private Vector3 original_camera_pos;
        private FPSController controller;
        private Transform cameraTransform;
        private float move_x = 0.0F, move_y = 0.0F;
        


        void Start()
        {

            cameraTransform = Camera.main.transform;
            controller = GetComponent<FPSController>();

        }


        void Update()
        {

            if (controller.isWalking || controller.isRunning)
            {
                move_x = Mathf.Lerp(move_x, deltaX, step_length);
                move_y = Mathf.Lerp(move_y, deltaY, step_length);
            }
            else
            {
                move_x = Mathf.Lerp(move_x, cameraTransform.localPosition.x, step_length);
                move_y = Mathf.Lerp(move_y, cameraTransform.localPosition.y, step_length);
            }

            if (Mathf.Abs(move_x - deltaX) <= 0.05f)
            {
                deltaX *= -1;
            }

            if (Mathf.Abs(Mathf.Abs(move_x) - Mathf.Abs(deltaX/2)) <= 0.2f)
            {
                deltaY *= -1;
            }

            Vector3 target = cameraTransform.localPosition;
            target.x = move_x;
            target.y = move_y;
            cameraTransform.localPosition = target;


        }
    }
}