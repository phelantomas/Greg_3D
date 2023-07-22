
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    public float moveSpeed = 20; 
    public int Gap = 10;
    // Start is called before the first frame update
    private Vector3 _direction = Vector3.right;

    public GameObject initSegmentPrefab;
    public GameObject segmentPrefab;
    private List<GameObject> _segements = new List<GameObject>();
    private List<Vector3> positionHistory = new List<Vector3>();

    private bool snakeAlive = true;

     public LogicManager logic;

    void Start()
    {
        Grow(true);
        logic = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicManager>();
    }

    private void Grow(bool initial){
        if (initial) {
          GameObject body = Instantiate(initSegmentPrefab);
          _segements.Add(body);
        } else {
          GameObject body = Instantiate(segmentPrefab);
          _segements.Add(body);
          logic.AddScore(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (snakeAlive) {
          if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
              _direction = Vector3.forward; //
          } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
              _direction = Vector3.back; //
          } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
              _direction = Vector3.left;
          } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
              _direction = Vector3.right;
          }
        }

        transform.position += _direction * moveSpeed * Time.deltaTime;
        positionHistory.Insert(0, transform.position);


        int index = 0;
        foreach(var body in _segements) {
            Vector3 point = positionHistory[Mathf.Min(index * Gap, positionHistory.Count - 1)];
            body.transform.position = point;
            index++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Food") {
            Grow(false);
        }

        if(other.tag == "Obstacle") {
            snakeAlive = false;
            logic.GameOver();
        }
    }
}
