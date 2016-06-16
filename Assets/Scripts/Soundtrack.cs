using UnityEngine;
using System.Collections;


public class CSoundtrack
{
	public struct Tune {
		
		public string band,
			song,
			album,
			year,
			path;

		public AudioClip clip;

		public Tune(string band_title,
			string song_title,
			string album_title,
			string year_release) : this() {

			band = band_title;
			song = song_title;
			album = album_title;
			year = year_release;
			path = (string)("sounds/music/" + band_title + "__" + song_title).ToLower().Replace(' ', '_');

			clip = Resources.Load (path, typeof(AudioClip)) as AudioClip;
		}
	}

	// Class providing shuffle right index
	protected class Shuffle {

		private int shuffled_index = -1;
		private Tune[] playlist;


		public Shuffle(ref Tune[] music) {
			
			playlist = music;
			DoShuffle();

		}


		public Tune PlayNext() {
			
			if (shuffled_index == playlist.Length - 1) {
				DoShuffle ();
				shuffled_index = -1;
			}

			shuffled_index += 1;
			Debug.Log ("CURRENT SONG BY " + playlist [shuffled_index].band);
			return playlist[shuffled_index];
		}


		private void DoShuffle() {
			
			int rnd_index;
			Tune song_to_shuffle;

			for (int i = 0; i < playlist.Length; i++) {
				rnd_index = Random.Range (0, playlist.Length);

				if (i != rnd_index) {
					song_to_shuffle = playlist [i];
					playlist [i] = playlist [rnd_index];
					playlist [rnd_index] = song_to_shuffle;
				}

			}

		}
	}

	private Tune[] playlist = new Tune[5];
	private Tune current_tune;
	private Shuffle shuffling;

	private AudioSource MusicSource;

	// Construct. object
	public CSoundtrack() {
		// temp. not in a file
		playlist [0] = new Tune ("Youth Decay", "Little Winnipeg", "The Party's Over | LP", "2016");
		playlist [1] = new Tune ("Get Cold", "Missed", "Before I Fall Asleep | EP", "2016");
		playlist [2] = new Tune ("The Lunch Club", "Strayed", "Basements of Truth | EP", "2016");
		playlist [3] = new Tune ("Paint the Morning", "Аэроплан", "Мемория | LP", "2015");
		playlist [4] = new Tune ("Sløtface", "Sponge State", "Sponge State | Single", "2016");

		shuffling = new Shuffle (ref playlist);

		MusicSource = DefaultAudioSource ();
		MusicSource.spatialBlend = 0.1f;
	}


	// Play tune by index in array
	public void PlayTune (Tune song) {
		current_tune = song;
		MusicSource.PlayOneShot (current_tune.clip, 0.08f);
	}

	public void Mute (bool off) {
		
		MusicSource.volume = (off) ? 0.0f : 0.8f;

	}

	public bool IsMuted() {

		return (MusicSource.volume == 0.0f);

	}

	// Set - Set custom audio source for playing tracks
	public void SetCustomAudioSource(ref AudioSource custom_audio_source) {
		MusicSource = custom_audio_source;
	}

	// Get - Default audio source by CORE object
	public AudioSource DefaultAudioSource() {
		return GameObject.Find("Core").AddComponent<AudioSource> ();
	}

	// Update process
	public void Process() {
		if (!MusicSource.isPlaying) {
			PlayTune (shuffling.PlayNext ());
		} else {
			// Temp. 
			// Alt + N - next shuffled track
			// Alt + M - mute/unmute music

			if ((Input.GetKey (KeyCode.LeftAlt) || Input.GetKey (KeyCode.RightAlt))
				&& Input.GetKeyUp (KeyCode.N)) {

				MusicSource.Stop ();
			}

			if ((Input.GetKey (KeyCode.LeftAlt) || Input.GetKey (KeyCode.RightAlt))
				&& Input.GetKeyUp (KeyCode.M)) {

				Mute (!IsMuted ());
			}
		}

	}

	// Widget
	public void OnGUI() {
		GUIStyle label_style = new GUIStyle();
		label_style.fontSize = 10;

		float w_band = label_style.CalcSize (new GUIContent (current_tune.band.ToUpper())).x;
		float w_song = label_style.CalcSize (new GUIContent (current_tune.song.ToUpper())).x;
		float w = (w_band > w_song) ? w_band + 10 : w_song + 10;

		label_style.normal.textColor = Color.black;

		GUI.color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
		GUI.TextArea (new Rect (5, Screen.height - 52, w, 36), "");
		GUI.Label (new Rect (9, Screen.height - 44, w, 50), current_tune.band.ToUpper(), label_style);

		label_style.normal.textColor = Color.gray;

		GUI.Label (new Rect (9, Screen.height - 36, w, 50), current_tune.song.ToUpper(), label_style);
	}

}

