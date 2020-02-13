using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{

    public GameObject levelUpEffectPrefab;
    public Transform levelUpEffectSpawner;
    public SpriteRenderer levelDisplayer;

    public Sprite lvl2Sprite;
    public Sprite lvl3Sprite;
    public Sprite lvl4Sprite;
    public Sprite lvl5Sprite;

    private int maxLevel;
    private int currentLevel;

    private float expNeededToNextLevel;
    private float currentExp;

    private float baseRangedDamageModifier;
    private float baseMeleeDamageModifier;
    private float baseMaxLifeModifier;


    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 1;
        currentExp = 0;
        expNeededToNextLevel = 200;

        CharacterBaseStats playerBaseStats = GetComponent<Player>().baseStats;

        maxLevel = playerBaseStats.maxLevel;
        baseRangedDamageModifier = playerBaseStats.baseRangedDamageModifier;
        baseMeleeDamageModifier = playerBaseStats.baseMeleeDamageModifier;
        baseMaxLifeModifier = playerBaseStats.baseMaxLifeModifier;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrantExp(float exp) {
        currentExp += exp;
        CheckLevelUp();
    }

    public void LevelUp() {

        Player player = GetComponent<Player>();

        Instantiate(levelUpEffectPrefab, levelUpEffectSpawner.transform.position, Quaternion.identity);

        currentLevel += 1;

        player.IncreaseMeleeDamage(baseMeleeDamageModifier);
        player.IncreaseRangedDamage(baseRangedDamageModifier);

        player.IncreasePlayerMaxHealth(baseMaxLifeModifier);
        player.HealPlayerCompletely();

        currentExp -= expNeededToNextLevel;
        expNeededToNextLevel = expNeededToNextLevel * 2;

        if (currentLevel == 2) {
            levelDisplayer.sprite = lvl2Sprite;
        } else if (currentLevel == 3) {
            levelDisplayer.sprite = lvl3Sprite;
        } else if (currentLevel == 4) {
            levelDisplayer.sprite = lvl4Sprite;
        } else if (currentLevel == 5) {
            levelDisplayer.sprite = lvl5Sprite;
        }

        CheckLevelUp();
    }

    private void CheckLevelUp() {

        if (currentLevel < maxLevel) {
         
            if (currentExp >= expNeededToNextLevel) {
                LevelUp();
            }
        }

    }

    public float GetCurrentExp() {
        return currentExp;
    }

    public float GetExpNeededForNextLevel() {
        return expNeededToNextLevel;
    }

    public bool IsMaxLevel() {
        if(currentLevel < maxLevel) {
            return true;
        } else {
            return false;
        }
    }

}
