using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehavior : MonoBehaviour
{
    private AudioSource baseTrack;
    private AudioSource trackFX_1;
    private AudioSource trackFX_2;
    private AudioSource trackFX_3;

    private float volume;
    private float distance;

    private bool shouldPlayBase = false;

    public GameObject centerEyeAnchor;

    public GameObject baseTrackGO;
    public GameObject trackFX_1GO;
    public GameObject trackFX_2GO;
    public GameObject trackFX_3GO;

    private BlockManager blockManager;

    // Start is called before the first frame update
    void Start()
    {
        blockManager = GetComponent<BlockManager>();
        shouldPlayBase = true;
        InitializeAudio();
    }

    // Update is called once per frame
    void Update()
    {
        if (blockManager.IsGrabbing && shouldPlayBase)
        {
            PlayBase();
            UpdateBaseVolume();
        }

        if (!blockManager.IsGrabbing && shouldPlayBase)
        {
            MuteAll();
        }
    }

    private void InitializeAudio()
    {
        baseTrack = baseTrackGO.GetComponent<AudioSource>();
        baseTrack.spatialize = true;

        trackFX_1 = trackFX_1GO.GetComponent<AudioSource>();
        trackFX_1.spatialize = true;

        trackFX_2 = trackFX_2GO.GetComponent<AudioSource>();
        trackFX_2.spatialize = true;

        trackFX_3 = trackFX_3GO.GetComponent<AudioSource>();
        trackFX_3.spatialize = true;
    }

    private void UpdateBaseVolume()
    {
        distance = Vector3.Distance(centerEyeAnchor.transform.position, transform.position);
        float t = Mathf.InverseLerp(0, 1.5f, distance);
        volume = Mathf.InverseLerp(0, 0.75f, t);

        baseTrack.volume = 0.75f - distance;
    }

    public void MuteAll()
    {
        baseTrack.mute = true;
        trackFX_1.mute = true;
        trackFX_2.mute = true;
        trackFX_3.mute = true;
    }

    public void PlayBase()
    {
        shouldPlayBase = true;

        baseTrack.mute = false;
        trackFX_1.mute = true;
        trackFX_2.mute = true;
        trackFX_3.mute = true;
    }

    public void PlayFX1()
    {
        shouldPlayBase = false;

        baseTrack.mute = true;
        trackFX_1.mute = false;
        trackFX_2.mute = true;
        trackFX_3.mute = true;
    }

    public void PlayFX2()
    {
        shouldPlayBase = false;

        baseTrack.mute = true;
        trackFX_1.mute = true;
        trackFX_2.mute = false;
        trackFX_3.mute = true;
    }
    public void PlayFX3()
    {
        shouldPlayBase = false;

        baseTrack.mute = true;
        trackFX_1.mute = true;
        trackFX_2.mute = true;
        trackFX_3.mute = false;
    }

}
