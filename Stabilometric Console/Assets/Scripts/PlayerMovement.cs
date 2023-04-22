// Keyboard controls
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    bool alive = true;

    public float speed = 5;
    [SerializeField] Rigidbody rb;

    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;

    public float speedIncreasePerPoint = 0.1f;

    private void FixedUpdate ()
    {
        if (!alive) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }

    private void Update () {
        horizontalInput = Input.GetAxis("Horizontal");

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

    // TODO: apply the changes above to the code below. or better yet, just have some conditional above that switches between the joystick and keyboard control.
    // One approach is to put if statements in each of the functions that change, and then put both versions of the code in. Or another approach is two have two versions
    // of each of those functions so the functions stay a bit cleaner, but would have to change code in GameManager.cs or whatever. [delete this comment later]
}

// TOGGLE BTWN KEYBOARD AND JOYSTICK

// // Joystick controls
// using UnityEngine;
// using UnityEngine.SceneManagement;

// using System.Collections;
// using System.IO.Ports;

// public class PlayerMovement : MonoBehaviour {

//     bool alive = true;

//     public float speed = 5;
//     [SerializeField] Rigidbody rb;

//     float horizontalInput;
//     [SerializeField] float horizontalMultiplier = 2;

//     public float speedIncreasePerPoint = 0.1f;

//     SerialPort data_stream = new SerialPort("COM5", 9600);
//     public string receivedstring;
//     public GameObject test_data;
//     //public Rigidbody rb;
//     //rb = getcomponent<Rigidbody>();
//     public float sensitivity = 0.01f;

//     public string[] datas;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();   //added this line
//         Debug.Log("hello world");
//         data_stream.Open();
//         Debug.Log("hello world again");
//     }


//     private void FixedUpdate ()
//     {
//         if (!alive) return;

//         Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
//         Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
//         rb.MovePosition(rb.position + forwardMove + horizontalMove);        
//     }

//     private void Update () {
//         receivedstring = data_stream.ReadLine();
//         //Debug.Log(receivedstring);
//         string[] datas = receivedstring.Split(',');  
//         Debug.Log(datas[0]);

//         horizontalInput = float.Parse(datas[0])/2;

//         if (transform.position.y < -5) {
//             Die();
//         }
// 	}

//     public void Die ()
//     {
//         alive = false;
//         // Restart the game
//         Invoke("Restart", 2);
//     }

//     void Restart ()
//     {
//         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//     }
// }