using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOns : MonoBehaviour {

    public bool healSplinter;
    [Header("Dash")]
    public bool lifeStealSplinter;
    public bool additionalStunConcussionSplinter;
    [Header("Slam")]
    public bool stunSplinter;
    public bool lifeForceSplinter;
    [Header("Air Jump")]
    public bool unleashConcussionSplinter;
    public bool additionalMomentumSplinter;
    [Header("Blast")]
    public bool releaseConcussionSplinter;
    public bool additionalAirJumpSplinter;
    [Header("Concussion")]
    public bool additionalConcussionLifeStealSplinter;
    public bool damageIncreaseSplinter;

    HealthComponent _healthComponent;
    PlayerMain _playerMain;
    PlayerShoot _playerShoot;
    UIManager _uiManager;
    // Use this for initialization

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _playerMain = GetComponent<PlayerMain>();
        _playerShoot = GetComponent<PlayerShoot>();
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
    }

    void Start ()
    {

        lifeStealSplinter = false;
        additionalStunConcussionSplinter = false;
        stunSplinter = false;
        lifeForceSplinter = false;
        unleashConcussionSplinter = false;
        additionalMomentumSplinter = false;
        releaseConcussionSplinter = false;
        additionalAirJumpSplinter = false;
        additionalConcussionLifeStealSplinter = false;
        damageIncreaseSplinter = false;

    }

    public void LoadData()
    {
        print("Add Ons Data Loaded");
        healSplinter = JsonData.gameData.healSplinter;
        lifeStealSplinter = JsonData.gameData.lifeStealSplinter;
        additionalStunConcussionSplinter = JsonData.gameData.additionalStunConcussionSplinter;
        stunSplinter = JsonData.gameData.stunSplinter;
        lifeForceSplinter = JsonData.gameData.lifeForceSplinter;
        unleashConcussionSplinter = JsonData.gameData.unleaseConcussionSplinter;
        additionalMomentumSplinter = JsonData.gameData.additionalMomentumSplinter;
        releaseConcussionSplinter = JsonData.gameData.releaseConcussionSplinter;
        additionalAirJumpSplinter = JsonData.gameData.additionalAirJumpSplinter;
        additionalConcussionLifeStealSplinter = JsonData.gameData.additionalLifeStealSplinter;
        damageIncreaseSplinter = JsonData.gameData.damageIncreaseSplinter;

    }

    public void DashLifeSteal()
    {
        //Life steal per concussion hit(+2)
        //Need to check:
        //when is player dashing
        //if addon true, add life steal per hit from concussion
    }

    public void DashAdditionalStunConcussion()
    {
        //+5 additional concussion damage to stunned enemies
        //Need to check:
        //when is player dashing
        //if hit enemy is stunned
        //if addon true and enemy stunned, add 5 extra  damage
    }

    public void SlamStun()
    {
        //Stun enemies and increase damage based on momentum(+1 for every 10 % over 50 %)
        //check when player is slamming
        //each enemy will check if concussion stun is true
        //if true, activate stun behaviour
    }
    
    public void SlamLifeForce()
    {
        //Life Force: for every life steal, add + 2 damage to this attack(resets after attack)
        //Check when any life steal occurs
        //if life steal occuring, add + 2 damage to new int lifeForceAddedDMG
        //when slam, add lifeForceAddedDMG on top of damage, then set lifeForceAddedDMG back to 0
    }
    
    public void AirJumpConcussion()
    {
        //Unleash Concussion
        //check for when in air and if air jumping
        //if air jumping, release concussion
    }

    public void AirJumpMomentum()
    {
        //Gain 1 additional Air Jump if fired in air
        //check for when in air and air jumping
        //if air jumping, add an extra + 1 to momentum
    }
    
    public void BlastReleaseConcussion()
    {
        //Release Concussion on hit for large AOE and stun enemies
        //if add on true, allow blast to unleash concussion on hit before destroying itself
    }
	
    public void BlastAdditionalAirJump()
    {
        //Gain 1 additional Air Jump if fired in air
        //check if fired and in air (same way for how airbehaviour is added)
        //if fired and in air, give additional air jump
    }

    public void ConcussionAdditionalLifeSteal()
    {
        if (additionalConcussionLifeStealSplinter)
        {
            _healthComponent.LifeSteal(1);
            _uiManager.UpdateHealth();
            print("lifesteal");
        }
        //if (additionalLifeStealSplinter)
        //{

        //    if (_healthComponent.health < _healthComponent.maxHealth)
        //        _healthComponent.health += 1;
        //}
        //Addition + 1 life steal for every concussion hit on any move
        //concussion checks if this addon is true
        //if true, add +1 life to player health component
    }

    public int ConcussionIncreaseDamage()
    {
        return 2;
        //Additional + 2 damage to all concussions
        //concussion check if addon is true
        //if true, increase base damage to 2 (damage by concussion should all be set with a +=)
    }

}
