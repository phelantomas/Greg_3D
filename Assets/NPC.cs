using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    private int previousScore = 0;

    public LogicManager logic;
    public AudioClip dianne;

    public AudioClip jake;

    public List<AudioClip> randomClips = new List<AudioClip>();
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        int currentScore = logic.GetScore();

        if (previousScore != currentScore && !audioSource.isPlaying) {
            previousScore = currentScore;
            audioSource.PlayOneShot(randomClips[Random.Range (0, randomClips.Count)], 0.7F);
        }
    }
}
