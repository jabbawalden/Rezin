using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    
    public Rigidbody2D rb;

    [Header("Player Movement")]
    public float movementSpeed;
    public float dashSpeed;
    public float fallMultiplier, lowJumpMultiplier;
    public bool grounded;
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
    private bool _haveJumped;
    [Space(5)]

    [Header("Player Jump")]
    public int jumpCount;
    public int jumpMaxCount;
    [Space(5)]

    [Header("Player Energy")]
    public int currentEnergy;
    public int maxEnergy;
    private float _nextEnergyRegen;
    public float energyRegenerate;
    public bool facingPositive;
    public float distanceTravelled;
    private Vector2 _lastPosition;

    [Header("Player Shoot")]
    public int laserDamage;
    public float laserSpeed;
    public GameObject projectile;
    public float fireRateDivider;
    private float _nextFire;
    public Transform shotOrigin;
    float distance;

    private Laser _laser;
    public int projectileLife;

    [Space(5)]

    [Header("Player Uprades")]
    public bool doubleJump;
    public bool rebound;


    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        _healthComponent = GetComponent<HealthComponent>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    // Use this for initialization
    void Start ()
    {
        //if file exists, load files here
        //else initialize default variables
        doubleJump = true;
        rebound = true;
        jumpCount = 2;
        jumpMaxCount = 2;

        //always default
        facingPositive = true;
        dead = false;
        _lastPosition = transform.position;
        _haveJumped = false;
    }

    void LoadData()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {

        if (_healthComponent.IsAlive())
        {
            float deltaPosition = movementSpeed * Time.deltaTime;
            PlayerMovementBehaviour(deltaPosition);
            PlayerJump();
            PlayerDirectionFace();
            PlayerDash();
            EnergyRegenerate();
            PlayerShoot(GetFireRateFromDistance());
            
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
    }

    public Vector2 GetMouseDirection()
    {
        Vector2 mousePos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = new Vector2(mousePos2.x - shotOrigin.position.x, mousePos2.y - shotOrigin.position.y);
        //normalize to get the same speed. 
        //No normalize to increase projectile speed based on mouse distance from player

        return direction;
    }

    public float GetFireRateFromDistance()
    {
        distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
        float newFireRate = distance / fireRateDivider;
        return newFireRate;
    }

    void PlayerDirectionFace()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        //direction through - vectors of both positions
        if (mousePos.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingPositive = true;
            //print("left");
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingPositive = false;
            //print("right");
        }

    }

    void PlayerMovementBehaviour(float s) 
    {
        float x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2 (x * s, rb.velocity.y);
    }

    void PlayerJump()
    {
        //check if grounded
        if (Input.GetButtonDown("Jump") && jumpCount >= 1)
        {
            rb.velocity = Vector2.up * jumpVelocity;
            jumpCount--;
            _haveJumped = true;
        }

        //fall multiplier
        if (rb.velocity.y < -0.1f)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

            if (doubleJump && !_haveJumped)
                jumpCount = 1;
            else if (!doubleJump && !_haveJumped)
                jumpCount = 0;
        }
    }


    void PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentEnergy >= 50)
        {
            distanceTravelled = 0;
            distanceTravelled += Vector2.Distance(transform.position, _lastPosition);
            _lastPosition = transform.position;
            print("right mouse hit");
            currentEnergy -= 50;
            if (facingPositive)
                rb.velocity = new Vector2(dashSpeed, 0);
            else
                rb.velocity = new Vector2(-dashSpeed, 0);
        }

    }

    IEnumerator colliderDashInvulnerable()
    {
        boxCol.isTrigger = true; 
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.2F);
        boxCol.isTrigger = false;
        rb.gravityScale = 1;
    }

    void PlayerShoot(float newFireRate)
    {
        if (Input.GetKey(KeyCode.Mouse0) && _nextFire < Time.time && currentEnergy >= 1)
        {
            _nextFire = Time.time + newFireRate;

            //crazy maths stuff to rotate sprite on its axis
            float angle = Mathf.Atan2(GetMouseDirection().y, GetMouseDirection().x) * Mathf.Rad2Deg;

            GameObject shot = Instantiate(projectile, shotOrigin.position, Quaternion.AngleAxis(angle, Vector3.forward));
            shot.GetComponent<Rigidbody2D>().velocity = 
                new Vector2(
                    Mathf.Clamp(GetMouseDirection().x * laserSpeed, GetMouseDirection().x * 2, GetMouseDirection().x * 3), 
                    Mathf.Clamp(GetMouseDirection().y * laserSpeed, GetMouseDirection().y * 2, GetMouseDirection().y * 3));

            _laser = shot.GetComponent<Laser>();
            _laser._damage = laserDamage;
            _laser.projectileLife = projectileLife;
        }

    }


    public void PlayerDamageBehaviour()
    {
        StartCoroutine(MaterialShift());
        rb.AddForce(Vector2.up * 100);
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

    public void SpringBehaviour()
    {
        if (jumpCount > 0)
        {
            jumpCount--;
            _haveJumped = true;
        }
    }

    void PlayerDeath()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            print("groundcollide");
            jumpCount = jumpMaxCount;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _haveJumped = false;
        }
            
    }




}
