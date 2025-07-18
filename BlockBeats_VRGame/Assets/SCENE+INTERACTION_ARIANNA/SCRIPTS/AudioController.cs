using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    private AudioSource audioSource_1;
    private AudioSource audioSource_2;
    private AudioSource audioSource_3;
    private AudioSource audioSource_4;


    [SerializeField]
    private AudioClip[] _audioClips;

    public GameObject myself;
    public GameObject block;

    public GameObject audioTrack_2;
    public GameObject audioTrack_3;
    public GameObject audioTrack_4;
  

    float _volume;
    float dist;

    void Start()
    {
        audioSource_1 = gameObject.GetComponent<AudioSource>();
        audioSource_1.spatialize = true;

        audioSource_2 = audioTrack_2.GetComponent<AudioSource>();
        audioSource_2.spatialize = true;

        audioSource_3 = audioTrack_3.GetComponent<AudioSource>();
        audioSource_3.spatialize = true;

        audioSource_4 = audioTrack_4.GetComponent<AudioSource>();
        audioSource_4.spatialize = true;
    }

    void Update()
    {
        dist = Vector3.Distance(myself.transform.position, block.transform.position);
        float t = Mathf.InverseLerp(0, 1.5f, dist);
        _volume = Mathf.InverseLerp(0, 0.75f, t);

        audioSource_1.volume = 0.75f-dist;

        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("R"))
        {
            //audioSource.clip = _audioClips[1];
            Debug.Log('R');
            
            audioSource_1.mute = true;
            audioSource_2.mute = false;
            audioSource_3.mute = true;
            audioSource_4.mute = true;

        }

        if (other.gameObject.CompareTag("G"))
        {
            //audioSource.clip = _audioClips[2];
            Debug.Log('G');

            audioSource_1.mute = true;
            audioSource_2.mute = true;
            audioSource_3.mute = false;
            audioSource_4.mute = true;
        }

        if (other.gameObject.CompareTag("B"))
        {
           // audioSource.clip = _audioClips[3];
            Debug.Log('B');

            audioSource_1.mute = true;
            audioSource_2.mute = true;
            audioSource_3.mute = true;
            audioSource_4.mute = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("R") || other.gameObject.CompareTag("G") || other.gameObject.CompareTag("B"))
        {
            audioSource_1.mute = false;
            audioSource_2.mute = true;
            audioSource_3.mute = true;
            audioSource_4.mute = true;
        }
    }


}
