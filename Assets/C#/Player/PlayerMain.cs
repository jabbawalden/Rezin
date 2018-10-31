using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour {


    [Header("Player Stats")]
    public int essence;
    private MomentumComponent _momentumComponent;

    [Space(5)]

    [Header("Player Setup ")]
    public Rigidbody2D rb;
    public Vector2 startPosition;
    GameData gameData = new GameData();
    public Camera playerCamera;
    public bool invulnerable;

    [Space(5)]

    [Header("Player Dash")]
    public float dashSpeed;
    public GameObject rayOrigin;
    public bool isDashing;

    [Space(5)]

    [Header("Player Movement")]
    public float movementSpeed;
    public float climbSpeed;
    public float springForce;
    public float fallMultiplier, lowJumpMultiplier;
    public bool grounded;
    //TODO Diagonal shooting via bool and W & D or W & A checks
    //if diagonal, velocity always equals zero and projectiles fire at 45 degree angles
    //creates a slider for between values 1 and 10 to control jump velocity
    [Range(1, 10)]
    public float jumpVelocity;
    BoxCollider2D boxCol;

    [Space(5)]
    private HealthComponent _healthComponent;
    public bool dead;
    [Space(5)]

    [Header("Player GroundCheck")]
    public bool onGroundCheckRepresentation;
    public Material pMat;
    private UIManager _uiManager;

    [Space(5)]

    [Header("Player Jump")]
    public int jumpCount;
    public int jumpMaxCount;
    public bool haveJumped;
    public bool inAir;
    public int collisionCount;
    [Space(5)]

    [Header("Player Energy")]
    public int currentEnergy;
    public int maxEnergy;
    private float _nextEnergyRegen;
    public float energyRegenerate;
    public bool facingPositive;
    public float distanceTravelled;
    private Vector2 _lastPosition;

    public Transform shotOrigin;
    float distance;
    [Space(5)]
    [Header("Player Wall Slide Behaviour")]
    public float wallSlideSpeedMax;

    //private Laser _laser;
    //public int projectileLife;

    public bool isWallSliding;
    [Space(5)]

    [Header("Player Concussion Behaviour")]
    public GameObject concussionObj;
    public float stunTime;
    ConcussionObject _concussionObject;

    [Space(5)]

    [Header("Player Slam Behaviour")]
    public bool slamConcussion;
    public bool stopVelocity; 
    public bool isSlamming;

    [Space(5)]

    [Header("Player Uprades")]
    public bool airJumpUpgrade;
    public bool wallClimbUpgrade;
    public bool dashUpgrade;
    public bool concussionUpgrade;
    public bool slamUpgrade;
    public bool doubleAirJumpUpgrade;

    [Header("Player Add-ons")]
    private AddOns _addOns;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        _healthComponent = GetComponent<HealthComponent>();
        boxCol = GetComponent<BoxCollider2D>();
        _concussionObject = concussionObj.GetComponent<ConcussionObject>();
        _momentumComponent = GetComponent<MomentumComponent>();
        _addOns = GetComponent<AddOns>();
    }

    // Use this for initialization
    void Start ()
    {
        pMat.color = new Color(0, 191, 156);
        //always default
        inAir = false;
        slamConcussion = false;
        facingPositive = true;
        dead = false;
        _lastPosition = transform.position;
        haveJumped = false;
        invulnerable = false;
        stopVelocity = false;
    }

    public void LoadData()
    {
        //Load data
        startPosition = JsonData.gameData.startPosition;
        maxEnergy = JsonData.gameData.maxEnergy;
        dashUpgrade = JsonData.gameData.dashUpgrade;
        airJumpUpgrade = JsonData.gameData.airJumpUpgrade;
        wallClimbUpgrade = JsonData.gameData.wallClimbUpgrade;
        concussionUpgrade = JsonData.gameData.concussionUpgrade;
        slamUpgrade = JsonData.gameData.slamUpgrade;
        doubleAirJumpUpgrade = JsonData.gameData.doubleAirJumpUpgrade;
        essence = JsonData.gameData.essence;
        //set variables
        transform.position = startPosition;
        playerCamera.transform.position = new Vector3(startPosition.x, startPosition.y, -10);
        
    }

    // Update is called once per frame
    void Update ()
    {
        if (_healthComponent.IsAlive())
        {
            float deltaPosition = movementSpeed * Time.deltaTime;
            PlayerMovementBehaviour(deltaPosition);
            PlayerJump();
            PlayerDash();
            Slam();
            PlayerDirectionFace();
            EnergyRegenerate();
            WallSlide();

            //if healSplinter upgraded
            if (Input.GetKeyDown(KeyCode.I) && _addOns.healSplinter)
            {
                Heal();
                _uiManager.UpdateHealth();
            }


        }
        else
        {
            Debug.Log("Dead");
            _uiManager.ShowRestartPanel();
            dead = true;
            rb.velocity = new Vector2(0, 0);
            //Death UI
            //No longer moving
            //Death anim
            //Game State Change
        }
        //onGroundCheckRepresentation = GetGround();
        if (stopVelocity)
        {
            rb.velocity = new Vector2(0, 0);
        }

    }

    public Vector2 GetMouseDirection()
    {
        Vector2 mousePos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = new Vector2(mousePos2.x - shotOrigin.position.x, mousePos2.y - shotOrigin.position.y).normalized;

        return direction;
    }

    void PlayerDirectionFace()
    {
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingPositive = true;
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingPositive = false;
        }
    }

    void PlayerMovementBehaviour(float s) 
    {
        float x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(x * s, rb.velocity.y);
    }

    void PlayerJump()
    {
        if (collisionCount > 0)
            inAir = false;
        else
            inAir = true;

        if (airJumpUpgrade)
            jumpMaxCount = 2;
        
        //check if grounded
        if (Input.GetKeyDown(KeyCode.J) && jumpCount >= 1)
        {
            rb.velocity = Vector2.up * jumpVelocity;
            jumpCount--;
            haveJumped = true;
        }

        //fall multiplier
        if (rb.velocity.y < -0.05f)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            if (!haveJumped)
            {
                if (airJumpUpgrade && !doubleAirJumpUpgrade)
                    jumpCount = 1;
                else if (doubleAirJumpUpgrade)
                    jumpCount = 2;
                else if (!airJumpUpgrade && !doubleAirJumpUpgrade)
                    jumpCount = 0;
            }

        }
    }

    void PlayerDash()
    {
        float transformOffset = 0.2f;

        if (Input.GetKeyDown(KeyCode.K) && currentEnergy >= 50 && dashUpgrade)
        {
            StartCoroutine(DashBehaviour(transformOffset));
            isDashing = true;
        }
            
    }

    //set velocity to 0, then dash
    IEnumerator DashBehaviour(float transformOffset)
    {
        if (concussionUpgrade)
        {
            concussionObj.transform.localScale = new Vector3(3, 3, 3);
            _concussionObject.concussionDamage = 10;
            Instantiate(concussionObj, transform.position, transform.rotation);
        }
           

        invulnerable = true;
        int direction;
        float offsetCalc;
        currentEnergy -= 50;

        if (facingPositive)
        {
            offsetCalc = -transformOffset;
            direction = 1;
        }
        else
        {
            offsetCalc = transformOffset;
            direction = -1;
        }
        stopVelocity = true;
        yield return new WaitForSeconds(0.05F);
        stopVelocity = false;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin.transform.position, Vector2.right * direction, 3, 1 << LayerMask.NameToLayer("GroundLayer"));
        Debug.DrawRay(rayOrigin.transform.position, Vector2.right * direction * 3, Color.red, 0.7f);

        if (hit.collider != null)
        {
            Vector2 newLoc = new Vector2(hit.point.x + offsetCalc, transform.position.y);
            Debug.Log(hit.collider.name);
            transform.position = newLoc;
        }
        else
        {
            transform.Translate(Vector2.right * dashSpeed * direction);
        }

        yield return new WaitForSeconds(0.01F);

        if (concussionUpgrade)
        {
            concussionObj.transform.localScale = new Vector3(3, 3, 3);
            _concussionObject.concussionDamage = 10;
            Instantiate(concussionObj, transform.position, transform.rotation);
        }
        
        yield return new WaitForSeconds(0.45F);
        invulnerable = false;
    }

    void WallSlide()
    {
        if (wallClimbUpgrade)
        {
            if (isWallSliding && rb.velocity.y < 0)
            {
                //float deltaSpeed = climbSpeed * Time.fixedDeltaTime;
                //float v = Input.GetAxis("Vertical");
                //rb.velocity = new Vector2(rb.velocity.x, v * climbSpeed);
                //transform.Translate(Vector2.up * v * deltaSpeed);
                //rb.AddForce(new Vector2(rb.velocity.x, v * deltaSpeed));
                if (rb.velocity.y < -wallSlideSpeedMax)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeedMax);
                }
                jumpCount = jumpMaxCount;
                //if on wall, slamConcussion won't fire even when S is pressed down
                slamConcussion = false;
            }

        }
    }


    public void PlayerDamageBehaviour()
    {
        StartCoroutine(MaterialShift());
        rb.AddForce(Vector2.up * 30);
        _uiManager.UpdateHealth();
    }

    IEnumerator MaterialShift()
    {
        pMat.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(0.34f);
        pMat.color = new Color(0, 191, 156);
    }

    void EnergyRegenerate()
    {
        if (currentEnergy < maxEnergy)
        {
            if (_nextEnergyRegen < Time.time)
            {
                _nextEnergyRegen = Time.time + energyRegenerate;
                currentEnergy++;
            }
        }
    }

    //player slam spring behaviour midAir
    public void SpringBehaviour()
    {
        StartCoroutine(SpringBehaviourCo());
        concussionObj.transform.localScale = new Vector3(4.3f, 4.3f, 4.3f);
        _concussionObject.concussionDamage = 20;
        Instantiate(concussionObj, transform.position, transform.rotation);
        slamConcussion = false;
        jumpCount = jumpMaxCount;
    }

    IEnumerator SpringBehaviourCo()
    {
        yield return new WaitForSeconds(0.01f);
        rb.velocity = (new Vector2(0, springForce));
    }

    void Slam()
    {
        if (rb.velocity.y < 0 || haveJumped)
        {
            if (Input.GetKeyDown(KeyCode.S) && !isWallSliding && slamUpgrade) 
            {
                slamConcussion = true;
                isSlamming = true;
                StartCoroutine(SlamBehaviour());
            }
        }
    }

    IEnumerator SlamBehaviour()
    {
        invulnerable = true;
        stopVelocity = true;
        yield return new WaitForSeconds(0.15f);
        stopVelocity = false;
        rb.AddForce(Vector2.down * 725);
        yield return new WaitForSeconds(0.5f);
        invulnerable = false;

    }

    public void RecoilBehaviour(float direction)
    {
        //rb.velocity = (Vector2.right * 10 * direction);
        rb.AddForce(new Vector2(260 * direction, 0), ForceMode2D.Force);
    }

    void Heal()
    {
        if (_momentumComponent.momentum >= 50)
            _healthComponent.health += 5;
        if (_momentumComponent.momentum >= 60)
            _healthComponent.health += 5;
        if (_momentumComponent.momentum >= 70)
            _healthComponent.health += 5;
        if (_momentumComponent.momentum >= 80)
            _healthComponent.health += 5;
        if (_momentumComponent.momentum >= 90)
            _healthComponent.health += 5;
        if (_momentumComponent.momentum >= 90)
            _healthComponent.health += 5;

        if (_healthComponent.health > _healthComponent.maxHealth)
            _healthComponent.health = _healthComponent.maxHealth;

        _momentumComponent.momentum = 0;
    }

    void PlayerDeath()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionCount++;

        foreach (ContactPoint2D hitPos in collision.contacts)
        {
            if (collision.collider.CompareTag("Ground") && hitPos.normal.y > 0)
            {
                jumpCount = jumpMaxCount;
                haveJumped = false;

                if (concussionUpgrade && slamConcussion)
                {
                    concussionObj.transform.localScale = new Vector3(4.3f, 4.3f, 4.3f);
                    _concussionObject.concussionDamage = 20;
                    Instantiate(concussionObj, transform.position, transform.rotation);
                    slamConcussion = false;
                }
            }
        }
    }

   

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            foreach (ContactPoint2D hitPos in collision.contacts)
            {
                if (hitPos.normal.x > 0 || hitPos.normal.x < 0)
                {
                    isWallSliding = true;

                }
                else
                {
                    isWallSliding = false;
                }

            }
        }
        
    }

    //check off isWallSliding
    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionCount--;

        if (collision.collider.CompareTag("Ground"))
        {
            isWallSliding = false;
        }
    }
    





}
