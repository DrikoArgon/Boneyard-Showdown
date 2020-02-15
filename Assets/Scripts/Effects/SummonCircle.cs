using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonCircle : MonoBehaviour {

	public Transform skeletonSpawnPointTransform;
	public Transform towerSkeletonSpawnPoint;
	public GameObject scrollOn;
    public GameObject scrollOff;
	public SpriteRenderer backSummonEffect;
	public SpriteRenderer frontSummonEffect;
	public int boneAmountNeeded;
	public GameObject skeletonToSummonPrefab;
	public int levelOfSkeletonCreated;
	public KeyCode summonKey;
	public string summonGamepadButton;
	public AudioClip summoningSound;

	private bool insideCircle;
	private Player playerInsideCircle;

	private AudioSource source;
	// Use this for initialization
	void Start () {
		insideCircle = false;
		source = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (insideCircle) {
			if (scrollOn.activeSelf) {
				if(Input.GetKeyDown(summonKey) || Input.GetButtonDown(summonGamepadButton)){
					source.PlayOneShot (summoningSound, 1.0f);
					SummonSkeleton ();
				}
			}
		}

	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Player1" || other.tag == "Player2") {
			playerInsideCircle = other.GetComponent<Player> ();

			insideCircle = true;

			if (playerInsideCircle.GetBoneAmount() >= boneAmountNeeded && !playerInsideCircle.dead) {
				scrollOn.SetActive (true);
				backSummonEffect.enabled = true;
				frontSummonEffect.enabled = true;
			}

		}

	}

	void OnTriggerExit2D(Collider2D other){

		if (other.tag == "Player1" || other.tag == "Player2") {
			scrollOn.SetActive (false);
			insideCircle = false;
			backSummonEffect.enabled = false;
			frontSummonEffect.enabled = false;
		}

	}

    public void SetupScrollSprites(Sprite offSprite, Sprite onSprite) {
        scrollOff.GetComponent<SpriteRenderer>().sprite = offSprite;
        scrollOn.GetComponent<SpriteRenderer>().sprite = onSprite;
    }

	public void SummonSkeleton(){
		playerInsideCircle.DecreaseBoneAmount(boneAmountNeeded);
		playerInsideCircle.GrantExp(levelOfSkeletonCreated * 50);

		if (playerInsideCircle.GetBoneAmount() < boneAmountNeeded) {
			scrollOn.SetActive (false);
			backSummonEffect.enabled = false;
			frontSummonEffect.enabled = false;
		}

		StartCoroutine (RiseSkeleton ());
	}

	IEnumerator RiseSkeleton()
	{
		
		yield return new WaitForSeconds(0.6f);
		Instantiate(skeletonToSummonPrefab,towerSkeletonSpawnPoint.position,Quaternion.identity);
	}
}
