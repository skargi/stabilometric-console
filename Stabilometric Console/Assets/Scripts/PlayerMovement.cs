﻿//#define KEYBOARD   //modes: KEYBOARD, JOYSTICK, LOADCELL (only toggle one at a time)
//#define JOYSTICK
#define LOADCELL

#if KEYBOARD
    // Keyboard controls
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class PlayerMovement : MonoBehaviour {

        bool alive = true;

        public float speed = 5;
        [SerializeField] Rigidbody rb;

        float horizontalInput;
        [SerializeField] float horizontalMultiplier = 2;

        float verticalInput; //forward/backward input

        public float speedIncreasePerPoint = 0.1f;

        private void FixedUpdate ()
        {
            if (!alive) return;

            //Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
            Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime * (1 + verticalInput);
            Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
            rb.MovePosition(rb.position + forwardMove + horizontalMove);
        }

        private void Update () {
            horizontalInput = Input.GetAxis("Horizontal");

            float scaleSpeed = 1.5f;
            verticalInput = scaleSpeed * Input.GetAxis("Vertical"); //forward/backward input

            if (transform.position.y < -5) {
                Die();
            }
        }

        public void Die ()
        {
            alive = false;
            // Restart the game
            //Invoke("Restart", 2);  // Replay button will restart the game; the Restart() function is no longer needed.
            SceneManager.LoadScene("Death Menu");
        }

        /* void Restart ()
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // this line just restarts the game. instead we want to load the death menu.
            SceneManager.LoadScene("Death Menu");
        } */
    }
#endif



////// SWITCH TO JOYSTICK



#if JOYSTICK
    // Joystick controls
    using UnityEngine;
    using UnityEngine.SceneManagement;

    using System.Collections;
    using System.IO.Ports;

    public class PlayerMovement : MonoBehaviour {

        bool alive = true;

        public float speed = 5;
        [SerializeField] Rigidbody rb;

        float horizontalInput;
        [SerializeField] float horizontalMultiplier = 2;

        public float verticalInput; //forward/backward input

        public float speedIncreasePerPoint = 0.1f;

        SerialPort data_stream = new SerialPort("COM3", 9600);
        public string receivedstring;
        public GameObject test_data;
        //public Rigidbody rb;
        //rb = getcomponent<Rigidbody>();
        public float sensitivity = 0.01f;

        public string[] datas;

        void Start()
        {
            rb = GetComponent<Rigidbody>();   //added this line
            Debug.Log("hello world");
            data_stream.Open();
            Debug.Log("hello world again");
        }


        private void FixedUpdate ()
        {
            if (!alive) return;

            Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime * (1 + verticalInput);
            Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;  //dont need horizontal multiplier and manually scaling it in update (redundant)
            rb.MovePosition(rb.position + forwardMove + horizontalMove);        
        }

        private void Update () {
            receivedstring = data_stream.ReadLine();
            Debug.Log("receivedstring: " + receivedstring);
            string[] datas = receivedstring.Split(',');  
            Debug.Log("datas[0]: " + datas[0]);

            horizontalInput = float.Parse(datas[1]) * 0.001f * -1;
            Debug.Log("horzintalInput" + horizontalInput);
            verticalInput = float.Parse(datas[0]) * 0.002f * -1;  //forward/backward input

            if (transform.position.y < -5) {
                Die();
            }
        }

        public void Die ()
        {
            alive = false;
            // Restart the game
            //Invoke("Restart", 2);  // Replay button will restart the game; the Restart() function is no longer needed.
            SceneManager.LoadScene("Death Menu");
        }

        /* void Restart ()
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // this line just restarts the game. instead we want to load the death menu.
            SceneManager.LoadScene("Death Menu");
        } */
    }
#endif



////// SWITCH TO LOADCELL



#if LOADCELL
    // Load cell controls
    using UnityEngine;
    using UnityEngine.SceneManagement;

    using System.Collections;
    using System.IO.Ports;

    public class PlayerMovement : MonoBehaviour {

        bool alive = true;

        public float speed = 5;
        [SerializeField] Rigidbody rb;

        float horizontalInput;
        [SerializeField] float horizontalMultiplier = 2;

        public float speedIncreasePerPoint = 0.1f;

        SerialPort data_stream = new SerialPort("COM4", 9600);
        public string receivedstring;
        public GameObject test_data;
        //public Rigidbody rb;
        //rb = getcomponent<Rigidbody>();
        public float sensitivity = 0.01f;

        public string[] datas;

        float basef1, basef2, basef3, basef4;
        // float f1, f2, f3, f4;
        // f1 = 0; f2 = 0; f3 = 0; f4 = 0;

        void Start()
        {
            rb = GetComponent<Rigidbody>();   //added this line
            Debug.Log("hello world");
            data_stream.Open();
            Debug.Log("hello world again");
        }


        private void FixedUpdate ()
        {
            if (!alive) return;

            Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
            Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
            rb.MovePosition(rb.position + forwardMove + horizontalMove);        
        }

        private void Update () {
            float f1, f2, f3, f4;
            float length = 5;  //effectively just a scaling constant
            f1 = 0; f2 = 0; f3 = 0; f4 = 0;

            receivedstring = data_stream.ReadLine();
            Debug.Log(receivedstring);  //comment out
            string[] datas = receivedstring.Split(',');
            f1 = float.Parse(datas[0]);
            if(f1 < 5) {f1 = 0;}
            f2 = float.Parse(datas[1]);
            if(f2 < 5) {f2 = 0;}
            f3 = float.Parse(datas[2]);  //uncomment
            if(f3 < 5) {f3 = 0;}
            f4 = float.Parse(datas[3]);  //uncomment
            if(f4 < 5) {f4 = 0;}
            Debug.Log(f1 + ", " + f2 + ", " + f3 + ", " + f4);  //comment out

            //horizontalInput = f2 / 1000 * -1;   //comment out
            float tempscale = 0.1f;
            horizontalInput = ((f1 + f2 - f3 - f4) / (f1 + f2 + f3 + f4)) * tempscale;     //uncomment, length/2 is just a scaling constant
            horizontalInput = horizontalInput * -1;
            Debug.Log("horiztonalInput: " + horizontalInput);

            if (transform.position.y < -5) {
                Die();
            }
        }

        public void Die ()
        {
            alive = false;
            // Restart the game
            //Invoke("Restart", 2);  // Replay button will restart the game; the Restart() function is no longer needed.
            SceneManager.LoadScene("Death Menu");
        }

        /* void Restart ()
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // this line just restarts the game. instead we want to load the death menu.
            SceneManager.LoadScene("Death Menu");
        } */
    }
#endif