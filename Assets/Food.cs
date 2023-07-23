using UnityEngine;

public class Food : MonoBehaviour
{
    private void Start() {
        RandomizePosition();
    }

    //TODO: Take into account snakes current position
    private void RandomizePosition(){
        float x = Random.Range(-9, 9);
        float z = Random.Range(-9, 9);

        this.transform.position = new Vector3(Mathf.Round(x), 0.5f, Mathf.Round(z));
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player") {
            RandomizePosition();
        }
    }
}
