using UnityEngine;


namespace Effects
{
    public class EquipmentSway : MonoBehaviour
    {

        public float sway_speed = 0.5f;
        public float maxSway = 20.0f;

        void Update()
        {

            float rotCamX = Input.GetAxis("Mouse X") * sway_speed;
            float rotCamY = Input.GetAxis("Mouse Y") * sway_speed;
            if (Mathf.Abs(rotCamX) > 20.0f)
            {
                rotCamX = 20.0f * Mathf.Sign(rotCamX);
            }
            if (Mathf.Abs(rotCamY) > 20.0f)
            {
                rotCamY = 20.0f * Mathf.Sign(rotCamY);
            }
            transform.Rotate(transform.up * rotCamX * -1);
            transform.Rotate(transform.right * rotCamY * -1);
            transform.rotation = Quaternion.Lerp(transform.rotation, Camera.main.transform.rotation, sway_speed/2.0f);

        }
    }
}