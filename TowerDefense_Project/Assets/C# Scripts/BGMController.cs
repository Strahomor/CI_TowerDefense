using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BGMController : MonoBehaviour
{
    public AudioSource source;
    public AudioClip BGM;
    public AudioClip NukeCountdown;
    public AudioClip NukeSplosion;
    public List<AudioClip> AllSongs;
    public List<AudioClip> PlayedSongs;
    public int DiskotekaLength;
    public GameManager gameManager;

    public TMP_Text NukaTime;

    public bool SoundAlarm;
    public bool Exploded;

    public double RemainingAlarmTime;

    // Start is called before the first frame update
    void Start()
    {
        Exploded = false;
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
        if ((source.isPlaying == false) && (!gameManager.NukeIncoming))
        {
            SwapBGMTrack();
        }
        else if (gameManager.NukeIncoming)
        {
            Nuke();
        }
        
    }
    private void FixedUpdate()
    {
        if (SoundAlarm)
        {
            RemainingAlarmTime -= Time.deltaTime;
            NukaTime.text = System.Convert.ToInt32(RemainingAlarmTime).ToString();
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

    public void Nuke()
    {
        if ((SoundAlarm == false) && (source.isPlaying == true)) {
            source.Stop();
            source.PlayOneShot(NukeCountdown);
            NukaTime.text = System.Convert.ToInt32(NukeCountdown.length).ToString();
            RemainingAlarmTime = System.Convert.ToInt32(NukeCountdown.length);
            gameManager.MainUIPanel.SetActive(true);
            SoundAlarm = true;
        }
        else
        {
            if (((source.isPlaying == false) && (SoundAlarm) && (!Exploded)))
            {
                source.PlayOneShot(NukeSplosion);
                gameManager.NukePanel.SetActive(true);
                gameManager.ShopUIPanel.SetActive(false);
                gameManager.PauseUI.SetActive(false);
                Exploded = true;

            }
            else if (((source.isPlaying == false) && (SoundAlarm) && (Exploded)))
            {
                gameManager.NukePanel.SetActive(false);
                gameManager.DedUI.SetActive(true);
                gameManager.Score += 50000;
                gameManager.ScoreVal.text = gameManager.Score.ToString();
                gameManager.WaveNum.text = gameManager.WaveCounter.ToString();
                Time.timeScale = 0;
                SoundAlarm = false;
            }
            
        }
        
    }
}
