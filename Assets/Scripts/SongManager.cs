using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public AudioSource audioSource;
    public Lane[] lanes;
    public float songDelayInSeconds;
    public double marginOfError; // in seconds
    public int inputDelayInMilliseconds;
    public string fileLocation;
    public float noteTime;
    public static bool songFinished;
    public static MidiFile midiFile;

    /// <summary>
    /// Initializes the SongManager, reads MIDI file from either local or remote location.
    /// </summary>
    void Start()
    {
        Instance = this;
        songFinished = false;
        if (Application.streamingAssetsPath.StartsWith("http://") || Application.streamingAssetsPath.StartsWith("https://"))
        {
            StartCoroutine(ReadFromWebsite());
        }
        else
        {
            ReadFromFile();
        }
    }

    /// <summary>
    /// Reads a MIDI file from a URL and initializes note data.
    /// </summary>
    private IEnumerator ReadFromWebsite()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + fileLocation))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                byte[] results = www.downloadHandler.data;
                using (var stream = new MemoryStream(results))
                {
                    midiFile = MidiFile.Read(stream);
                    GetDataFromMidi();
                }
            }
        }
    }

    /// <summary>
    /// Reads a MIDI file from the local file system and initializes note data.
    /// </summary>
    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }

    /// <summary>
    /// Extracts note data from the MIDI file and distributes it to lanes.
    /// </summary>
    public void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);

        foreach (var lane in lanes) lane.SetTimeStamps(array);

        Invoke(nameof(StartSong), songDelayInSeconds);
    }

    /// <summary>
    /// Begins audio playback after MIDI data is prepared.
    /// </summary>
    public void StartSong()
    {
        audioSource.Play();
        print("song length: " + audioSource.clip.length);
    }

    /// <summary>
    /// Returns the current time of the audio source in seconds.
    /// </summary>
    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }

    /// <summary>
    /// Pauses or resumes audio playback depending on game pause state.
    /// Ends the song when the clip finishes playing.
    /// </summary>
    void Update()
    {
        if (Pause.isPaused)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }

        // print("difference: " + (audioSource.clip.length - audioSource.time));
        if (audioSource.time >= audioSource.clip.length)
        {
            songFinished = true;
            audioSource.Stop();
        }
    }
}
