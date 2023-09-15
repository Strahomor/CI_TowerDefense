using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public AudioSource source;
    public AudioClip BGM;
    public List<AudioClip> AllSongs;
    public List<AudioClip> PlayedSongs;
    public int DiskotekaLength;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        DiskotekaLength = AllSongs.Count;
        source = GetComponent<AudioSource>();
        Object[] Diskoteka = Resources.LoadAll("Music/");
        foreach (AudioClip Song in Diskoteka)
        {
            AudioClip This = (AudioClip)Song;
            AllSongs.Add(Song);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (source.isPlaying == false)
        {
            SwapBGMTrack();
        }
    }

    public void SwapBGMTrack()
    {
        int RandSong = Random.Range(0, AllSongs.Count);
        BGM = AllSongs[RandSong];
        source.PlayOneShot(BGM);
        gameManager.SongName.text = BGM.name;
        PlayedSongs.Add(AllSongs[RandSong]);
        AllSongs.RemoveAt(RandSong);
        if (AllSongs.Count == 0)
        {
            while (AllSongs.Count < DiskotekaLength)
            {
                AllSongs.Add(PlayedSongs[0]);
                PlayedSongs.RemoveAt(0);
            }
        }
    }
}
