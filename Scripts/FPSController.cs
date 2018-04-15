using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public class FPSController : MonoBehaviour
    {

        public float move_speed = 10.0F;
        [Range(0,3)]
        public float run_speed_multiplier = 2.0F;
        public float turn_speed = 2.0F;
        [Range(-1, 1)]
        public int y_invert = -1;
        public bool isDebug = true;
        public LayerMask object_layer;

        private Camera mainCam;
        private Rigidbody player;
        private bool isRunning = false;

        void Start()
        {

            player = GetComponent<Rigidbody>();
            mainCam = Camera.main;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player.constraints = RigidbodyConstraints.FreezeRotation;

        }

        void Update()
        {

            if (isDebug)
            {

                //Write devolopment build code here
                if (Input.GetKey(KeyCode.Escape) && !Cursor.visible)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    if (Cursor.visible)
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                }

            }

            #region Movement
            float rotX = Input.GetAxis("Mouse Y");
            float rotY = Input.GetAxis("Mouse X");

            float moveX = move_speed * Input.GetAxis("Horizontal");
            float moveY = move_speed * Input.GetAxis("Vertical");

            isRunning = Input.GetButton("Sprint") && moveY > 0.0F;
            if (isRunning) moveY *= run_speed_multiplier;           

            Vector3 velocity = new Vector3(0, 0);
            velocity = transform.forward * moveY + transform.right * moveX;

            player.velocity = velocity;

            player.transform.Rotate(Vector3.up * rotY, Space.World);
            mainCam.transform.Rotate(Vector3.right * rotX * y_invert);
            #endregion

            #region Object Detection

            RaycastHit hit;
            Ray ray = mainCam.ViewportPointToRay(new Vector2(0.5F, 0.5F));
            if (Physics.Raycast(ray, out hit, 100.0f, object_layer))
            {
                GameObject hitObj = hit.collider.gameObject;
                Debug.Log(hitObj.ToString());
            }

            #endregion

        }

    }
}