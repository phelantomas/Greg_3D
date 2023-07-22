using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // Start is called before the first frame update
    //public BoxCollider3D gridArea;

    //public LogicManager logic;


    private void Start() {
        RandomizePosition();
        //logic = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicManager>();
    }

    private void RandomizePosition(){
        //Bounds bounds =   //t//his.gridArea.bounds;

        float x = Random.Range(-9, 9);
        float z = Random.Range(-9, 9);

        this.transform.position = new Vector3(Mathf.Round(x), 0.5f, Mathf.Round(z));
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player") {
            RandomizePosition();
            //logic.AddScore();
        }
    }

    // private void OnTriggerEnter3D(Collider3D other) {

    //     if(other.tag == "Player") {
    //         RandomizePosition();
    //         //logic.AddScore();
    //     }
    // }
}
