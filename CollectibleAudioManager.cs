using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleAudioManager : MonoBehaviour {

	public AudioClip collectBoneSound1;
	public AudioClip collectBoneSound2;
	public AudioClip collectBoneSound3;
	public AudioClip spawnBoneSound1;
	public AudioClip spawnBoneSound2;
	public AudioClip spawnBoneSound3;

	private AudioSource source;
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayCollectBoneSound(){

		int awnser = Random.Range (1, 3);

		if (awnser == 1) {
			source.PlayOneShot (collectBoneSound1, 1.0f);
			//AudioSource.PlayClipAtPoint (collectSound1, transform.position,500);
		} else if (awnser == 2) {
			source.PlayOneShot (collectBoneSound2, 1.0f);
			//AudioSource.PlayClipAtPoint (collectSound2, transform.position,500);
		} else {
			source.PlayOneShot (collectBoneSound3, 1.0f);
			//AudioSource.PlayClipAtPoint (collectSound3, transform.position,500);
		}
	}

	public void PlayBoneSpawnSound(){

		int random = Random.Range (1, 4);

		if (random >= 1 && random < 2) {
			source.PlayOneShot (spawnBoneSound1, 1.0f);
		} else if (random >= 2 && random < 3) {
			source.PlayOneShot (spawnBoneSound2, 1.0f);
		} else {
			source.PlayOneShot (spawnBoneSound3, 1.0f);
		}

	}

}
