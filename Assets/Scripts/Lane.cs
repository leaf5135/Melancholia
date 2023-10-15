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

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
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
    // Update is called once per frame
    void Update()
    {
        HandleNoteSpawning();
        HandleNoteInput();
    }

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

    void SpawnNote()
    {
        var note = Instantiate(notePrefab, transform);
        notes.Add(note.GetComponent<Note>());
        Note noteComponent = note.GetComponent<Note>();
        noteComponent.assignedTime = (float)timeStamps[spawnIndex];
        spawnIndex++;
    }

    void HandleNoteInput()
    {
        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            bool hitProcessed = false; // Flag to track if a hit has been processed

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

    void HandleNoteHit(double timeStamp, double marginOfError, double audioTime)
    {
        double delay = Math.Abs(audioTime - timeStamp);
        if (delay <= marginOfError)
        {
            // Create a list to store indices of notes to be destroyed
            List<int> notesToDestroy = new List<int>();

            for (int i = inputIndex; i < notes.Count; i++)
            {
                double noteTime = notes[i].assignedTime;

                // Check if the note's time is within the specified range
                if (Math.Abs(audioTime - noteTime) <= 0.25)
                {
                    notesToDestroy.Add(i);
                }
                else
                {
                    break; // Exit the loop if we're outside the timestamp range
                }
            }

            // Destroy notes within the range
            foreach (int index in notesToDestroy)
            {
                try {
                    Destroy(notes[index].gameObject);
                }
                catch (MissingReferenceException ex)
                {
                    print($"Skipped a deleted note: {ex.Message}");
                }
            }

            // Update the inputIndex to the next unprocessed note
            inputIndex = Mathf.Max(inputIndex, notesToDestroy.Count);
            Hit();
            print($"Hit on {inputIndex} note");
        }
        else
        {
            if (delay <= 2 * marginOfError) // Register as an inaccurate hit
            {
                InaccurateHit();
                print($"Hit inaccurate on {inputIndex} note with {delay} delay");
            }
            else // Anything outside the margin of error range is a miss
            {
                HandleNoteMiss(timeStamp, marginOfError, audioTime);
            }
        }
    }
    void HandleNoteMiss(double timeStamp, double marginOfError, double audioTime)
    {
        try
        {
            if (timeStamp + marginOfError <= audioTime) {
                Destroy(notes[inputIndex++].gameObject);
            }
            Miss();
            print($"Missed {inputIndex} note");
        }
        catch (MissingReferenceException ex)
        {
            print($"Skipped a deleted note: {ex.Message}");
        }

    }
    private void Hit()
    {
        ScoreManager.Hit();
    }
    private void InaccurateHit() {
        ScoreManager.InaccurateHit();
    }
    private void Miss()
    {
        ScoreManager.Miss();
    }
}
