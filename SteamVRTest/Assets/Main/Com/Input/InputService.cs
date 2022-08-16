using UnityEngine;

namespace com
{
    public class InputService : MonoBehaviour
    {
        public static InputService instance { get; private set; }

        public KeyCode move_right_key = KeyCode.D;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(move_right_key))
            {

            }
        }

        public bool moveRight { get { return Input.GetKey(move_right_key); } }
    }
}
