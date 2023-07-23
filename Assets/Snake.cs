using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    public float moveSpeed = 20; 
    public int Gap = 10;
    // Start is called before the first frame update
    private Vector3 currentDirection = Vector3.right;
    public GameObject initSegmentPrefab;
    public GameObject segmentPrefab;
    public AudioSource audioSource;
    private List<GameObject> bodyParts = new List<GameObject>();
    private List<Vector3> positionHistory = new List<Vector3>();
    public float pitchSpeed = 0.01f;
    private bool snakeAlive = true;

    public LogicManager logic;

         Vector3 currentRotation = new Vector3(0.0f, 0.0f, 0.0f);
    void Start()
    {
        Grow(true);
        logic = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicManager>();
    }

    private void Grow(bool initial){
        if (initial) {
          GameObject body = Instantiate(initSegmentPrefab);
          bodyParts.Add(body);
        } else {
          GameObject body = Instantiate(segmentPrefab);
          bodyParts.Add(body);
          logic.AddScore(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool left = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        bool right = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        bool forward = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        bool back = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
   

        if (snakeAlive) {
          if(forward && currentDirection != Vector3.back){
              currentDirection = Vector3.forward;
              currentRotation = new Vector3(0, -90, 0);
          } else if (back && currentDirection != Vector3.forward) {
              currentDirection = Vector3.back;
              currentRotation = new Vector3(0, 90, 0);
          } else if (left && currentDirection != Vector3.right) {
              currentDirection = Vector3.left;
              currentRotation = new Vector3(0, 180, 0);
          } else if (right && currentDirection != Vector3.left) {
              currentDirection = Vector3.right;
              currentRotation = new Vector3(0, 0, 0);
          }
        }

        transform.position += currentDirection * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, currentRotation.z);
        positionHistory.Insert(0, transform.position);


        int index = 0;
        foreach(var body in bodyParts) {
            Vector3 point = positionHistory[Mathf.Min(index * Gap, positionHistory.Count - 1)];
            body.transform.position = point;
            index++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Food") {
            Grow(false);
            audioSource.pitch += pitchSpeed;
        }

        if(other.tag == "Obstacle") {
            snakeAlive = false;
            logic.GameOver();
        }
    }
}
