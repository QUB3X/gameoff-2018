using UnityEngine;

public class Music : MonoBehaviour {

    public AudioSource audioSource { get; private set; }
    public float musicBPM { get; private set; }
    public float musicBeatStart { get; private set; }     //milliseconds

    private float musicStartTime;
    private float timeBetweenBeats;                       //milliseconds
    private float previousOffset = 0;

    public Animator UIAnimator;
    public float ACCEPTABLE_TIME_OFFSET;                  //milliseconds
    public float UNITY_AUDIO_LATENCY;                     //milliseconds
	
	void Update () {
		if(audioSource != null) {
            float currentTime = GetTrackTime() - musicBeatStart;
            float offset = ((int)(currentTime * 1000) % (int)(timeBetweenBeats * 1000)) / 1000f;
            if (offset < previousOffset) {
                // Beat!
                UIAnimator.SetTrigger("Beat");
            }
            previousOffset = offset;
        }
	}

    public void LoadMusic(AudioSource audioSource) {
        this.audioSource = audioSource;
        if (audioSource == null) return;
        float next = (float)AudioSettings.dspTime + 1;
        this.musicStartTime = next * 1000f;
        audioSource.PlayScheduled(next);
        // TODO: Fetch from database:
        this.musicBPM = 66;
        this.musicBeatStart = 800;
        this.timeBetweenBeats = 1000 * 60 / musicBPM;
        Debug.Log("Time between beats: " + timeBetweenBeats);
    }

    /// <summary>
    /// Check if this istant is on time with the beat.
    /// Returns -1 if too early, +1 if too late, 0 if on time
    /// </summary>
    /// <param name="correctLatency">
    /// Should apply audio latency correction. Defaults to true.
    /// Set this to false if the input does not come from user but from a calculation.
    /// </param>
    /// <returns>
    /// -1 if too early, +1 if too late, 0 if on time
    /// </returns>
    public int IsOnTime(bool correctLatency = true) {
        if(audioSource == null)
            return 0;

        float currentTime = GetTrackTime() - musicBeatStart;
        if (correctLatency)
            currentTime -= UNITY_AUDIO_LATENCY;

        float offset = ((int)(currentTime*1000) % (int)(timeBetweenBeats*1000)) / 1000f; // Do division as microseconds for more precision
        if (offset >= timeBetweenBeats / 2 && timeBetweenBeats - offset > ACCEPTABLE_TIME_OFFSET) {
            // Too early
            Debug.Log("Too early! " + (offset - timeBetweenBeats));
            return -1;
        }

        if (offset < timeBetweenBeats / 2 && offset > ACCEPTABLE_TIME_OFFSET) {
            // Too early
            Debug.Log("Too late! " + offset);
            return +1;
        }
        
        return 0;
    }

    /// <summary>
    /// Get current track time in milliseconds
    /// </summary>
    float GetTrackTime() {
        return (float)(AudioSettings.dspTime * 1000 - this.musicStartTime);
    }
}
