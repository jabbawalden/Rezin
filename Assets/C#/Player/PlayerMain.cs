﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour {

    
    public Rigidbody2D rb;
    public Vector2 startPosition;
    GameData gameData = new GameData();
    public Camera playerCamera;

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
    [SerializeField] private bool _haveJumped;
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

    public Transform shotOrigin;
    float distance;

    //private Laser _laser;
    //public int projectileLife;

    public bool isWallSliding;
    [Space(5)]
    

    [Header("Player Uprades")]
    public bool doubleJump;
    public bool rebound;
    public bool wallSlide;

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
        pMat.color = new Color(0, 191, 156);
        //if file exists, load files here
        //else initialize default variables
        //doubleJump = true;
        //rebound = true;
        //jumpCount = 2;
        //jumpMaxCount = 2;

        //always default
        facingPositive = true;
        dead = false;
        _lastPosition = transform.position;
        _haveJumped = false;
    }

    public void LoadData()
    {
        //Load data
        startPosition = JsonData.gameData.startPosition;
        maxEnergy = JsonData.gameData.maxEnergy;
        //set variables
        transform.position = startPosition;
        playerCamera.transform.position = new Vector3(startPosition.x, startPosition.y, -10);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            maxEnergy += 10;
            startPosition += new Vector2(21, -5);
        }

        if (_healthComponent.IsAlive())
        {
            float deltaPosition = movementSpeed * Time.deltaTime;
            PlayerMovementBehaviour(deltaPosition);
            PlayerJump();
            PlayerDirectionFace();
            PlayerDash();
            EnergyRegenerate();
            
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
        //using transform instead of ShotOrigin for now
        Vector3 direction = new Vector2(mousePos2.x - shotOrigin.position.x, mousePos2.y - shotOrigin.position.y);
        //normalize to get the same speed. 
        //No normalize to increase projectile speed based on mouse distance from player

        return direction;
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentEnergy >= 100)
        {
            distanceTravelled = 0;
            distanceTravelled += Vector2.Distance(transform.position, _lastPosition);
            _lastPosition = transform.position;
            print("right mouse hit");
            currentEnergy -= 100;
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

    void WallSlide()
    {
        if (wallSlide)
        {
            if (isWallSliding)
            {

            }

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
        foreach (ContactPoint2D hitPos in collision.contacts)
        {
            Debug.Log(hitPos.normal);
            if (collision.collider.CompareTag("Ground") && hitPos.normal.y > 0)
            {
                jumpCount = jumpMaxCount;
                _haveJumped = false;
            }
            

        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.collider.CompareTag("Ground"))
        //{
        //    _haveJumped = false;
        //}


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