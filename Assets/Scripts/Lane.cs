using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Lane Instance;
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public GameObject notePrefab;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();
    int spawnIndex = 0;
    int inputIndex = 0;
    public float despawnOffset;
    public float noteSpawnY;
    public float noteTapY;

    public float noteDespawnY
    {
        get
        {
            return noteTapY - (noteSpawnY - noteTapY) + despawnOffset;
        }
    }

    public float noteSpawnX;
    public float noteTapX;

    public float noteDespawnX
    {
        get
        {
            return noteTapX - (noteSpawnX - noteTapX) + despawnOffset;
        }
    }

    public int direction;

    /// <summary>
    /// Initializes the Instance reference when the object starts.
    /// </summary>
    void Start()
    {
        Instance = this;
    }

    /// <summary>
    /// Sets timestamps for notes matching the lane's note restriction from a MIDI note array.
    /// </summary>
    /// <param name="array">Array of MIDI notes.</param>
    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }

    /// <summary>
    /// Called every frame to handle note spawning and input detection.
    /// </summary>
    void Update()
    {
        HandleNoteSpawning();
        HandleNoteInput();
    }

    /// <summary>
    /// Checks if it’s time to spawn a note and spawns it.
    /// </summary>
    void HandleNoteSpawning()
    {
        if (spawnIndex < timeStamps.Count)
        {
            double audioTime = SongManager.GetAudioSourceTime();
            double nextNoteTime = timeStamps[spawnIndex] - SongManager.Instance.noteTime;

            if (audioTime >= nextNoteTime)
            {
                SpawnNote();
            }
        }
    }

    /// <summary>
    /// Instantiates a note prefab and assigns its spawn time.
    /// </summary>
    void SpawnNote()
    {
        var note = Instantiate(notePrefab, transform);
        notes.Add(note.GetComponent<Note>());
        Note noteComponent = note.GetComponent<Note>();
        noteComponent.assignedTime = (float)timeStamps[spawnIndex];
        spawnIndex++;
    }

    /// <summary>
    /// Detects player input and handles hits or misses accordingly.
    /// </summary>
    void HandleNoteInput()
    {
        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            bool hitProcessed = false;

            if (Input.GetKeyDown(input))
            {
                hitProcessed = true;
                HandleNoteHit(timeStamp, marginOfError, audioTime);
            }
            else if (!hitProcessed && timeStamp + marginOfError <= audioTime)
            {
                HandleNoteMiss(timeStamp, marginOfError, audioTime);
            }
        }
    }

    /// <summary>
    /// Handles the logic for a note hit, destroying notes and updating stats.
    /// </summary>
    /// <param name="timeStamp">The note’s timestamp.</param>
    /// <param name="marginOfError">Allowed margin of error.</param>
    /// <param name="audioTime">Current audio playback time.</param>
    void HandleNoteHit(double timeStamp, double marginOfError, double audioTime)
    {
        double delay = Math.Abs(audioTime - timeStamp);
        if (delay <= marginOfError)
        {
            List<int> notesToDestroy = new List<int>();

            for (int i = inputIndex; i < notes.Count; i++)
            {
                double noteTime = notes[i].assignedTime;

                if (Math.Abs(audioTime - noteTime) <= 0.25)
                {
                    notesToDestroy.Add(i);
                }
                else
                {
                    break;
                }
            }

            foreach (int index in notesToDestroy)
            {
                try
                {
                    Destroy(notes[index].gameObject);
                }
                catch (MissingReferenceException)
                {
                    print($"Skipped a deleted note");
                }
            }

            inputIndex = Mathf.Max(inputIndex, notesToDestroy.Count);
            Hit();
        }
        else
        {
            if (delay <= 2 * marginOfError)
            {
                InaccurateHit();
            }
            else
            {
                HandleNoteMiss(timeStamp, marginOfError, audioTime);
            }
        }
    }

    /// <summary>
    /// Handles the logic for a missed note, destroying it and updating stats.
    /// </summary>
    /// <param name="timeStamp">The note’s timestamp.</param>
    /// <param name="marginOfError">Allowed margin of error.</param>
    /// <param name="audioTime">Current audio playback time.</param>
    void HandleNoteMiss(double timeStamp, double marginOfError, double audioTime)
    {
        try
        {
            if (timeStamp + marginOfError <= audioTime)
            {
                Destroy(notes[inputIndex++].gameObject);
            }
            Miss();
        }
        catch (MissingReferenceException ex)
        {
            print($"Skipped a deleted note: {ex.Message}");
        }
    }

    /// <summary>
    /// Registers a successful hit with the stats manager.
    /// </summary>
    private void Hit()
    {
        StatsManager.Hit();
    }

    /// <summary>
    /// Registers an inaccurate hit with the stats manager.
    /// </summary>
    private void InaccurateHit()
    {
        StatsManager.InaccurateHit();
    }

    /// <summary>
    /// Registers a miss with the stats manager.
    /// </summary>
    private void Miss()
    {
        StatsManager.Miss();
    }
}
