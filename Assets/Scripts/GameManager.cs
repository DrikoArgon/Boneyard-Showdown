using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ChosenCharacter {
    Necromancer,
    NecromancerAlt
}

public class GameManager : MonoBehaviour {

    public static GameManager instance;

	public int player1CastleLife;
	public int player2CastleLife;
	public int inicialCastleLife;
	public bool player1CastleUnderAttack;
	public bool player2CastleUnderAttack;

	public Text player1CastleLifeCounter;
	public Text player2CastleLifeCounter;

	public Text mergedPlayer1CastleLifeCounter;
	public Text mergedPlayer2CastleLifeCounter;

	public Animator player1CastleFlagAnimator;
	public Animator player2CastleFlagAnimator;

	public Animator mergedPlayer1CastleFlagAnimator;
	public Animator mergedPlayer2CastleFlagAnimator;

	private bool paused;

    public ChosenCharacter player1ChosenCharacter;
    public ChosenCharacter player2ChosenCharacter;

    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start () {

		paused = false;
		player1CastleUnderAttack = false;
		player2CastleUnderAttack = false;
		player1CastleLife = inicialCastleLife;
		player2CastleLife = inicialCastleLife;

		player1CastleLifeCounter.text = inicialCastleLife.ToString();
		player2CastleLifeCounter.text = inicialCastleLife.ToString();

		mergedPlayer1CastleLifeCounter.text = inicialCastleLife.ToString();
		mergedPlayer2CastleLifeCounter.text = inicialCastleLife.ToString();
		
	}
    
    public void SetChosenCharacter(bool isPlayer1, ChosenCharacter chosenCharacter) {
        if (isPlayer1) {
            player1ChosenCharacter = chosenCharacter;
        } else {
            player2ChosenCharacter = chosenCharacter;
        }
    }
	
	// Update is called once per frame
	void Update () {

		player1CastleLifeCounter.text = player1CastleLife.ToString();
		player2CastleLifeCounter.text = player2CastleLife.ToString();

		mergedPlayer1CastleLifeCounter.text = player1CastleLife.ToString();
		mergedPlayer2CastleLifeCounter.text = player2CastleLife.ToString();

		if (player1CastleUnderAttack) {
			player1CastleFlagAnimator.SetBool ("underAttack", true);
			mergedPlayer1CastleFlagAnimator.SetBool ("underAttack", true);
		} else {
			player1CastleFlagAnimator.SetBool ("underAttack", false);
			mergedPlayer1CastleFlagAnimator.SetBool ("underAttack", false);
		}

		if (player2CastleUnderAttack) {
			player2CastleFlagAnimator.SetBool ("underAttack", true);
			mergedPlayer2CastleFlagAnimator.SetBool ("underAttack", true);
		} else {
			player2CastleFlagAnimator.SetBool ("underAttack", false);
			mergedPlayer2CastleFlagAnimator.SetBool ("underAttack", false);
		}

		if (player1CastleLife <= 0) {
			SceneManager.LoadScene (4);
		}

		if (player2CastleLife <= 0) {
			SceneManager.LoadScene (3);
		}
			

	}
}
