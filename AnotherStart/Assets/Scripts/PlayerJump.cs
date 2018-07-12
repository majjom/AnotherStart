using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerJump : MonoBehaviour {

    [SerializeField] public float MaxJumpForce = 200;    
    [SerializeField] public int MaxJumpCount = 1;
    [SerializeField] public float MinDelayBetweenJumps = 0.25f;
    [SerializeField] public float JumpRequestConsideredZero = 0.2f;
    [SerializeField] public bool JumpRequestButtonLogic = true;
    [SerializeField] public LayerMask WhatIsGround;
    [SerializeField] public Transform GroundCheck;

    private bool previousGroundedSituation;
    private int jumpCount = 0;
    private float timeForNextJump = 0f;
    private float timeForPossibleJumpReset = 0f;
    private Rigidbody2D playerRigidbody2D;
    private GameObject thePlayer;
    private const float groundedRadius = .2f;

    private void Awake()
    {
        // for jumping
        previousGroundedSituation = IsPlayerGrounded();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        thePlayer = gameObject;
    }

    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per fixed time delta
    void FixedUpdate()
    {
        RequestJumpReset();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
    
    public void Jump(float jumpRequestIntensity)
    {
        if (Math.Abs(jumpRequestIntensity) < JumpRequestConsideredZero && jumpRequestIntensity != 0)
        {
            Debug.Log("PJ: NULLing Requested jump intensity: " + jumpRequestIntensity);
            jumpRequestIntensity = 0;
        }
        if (JumpRequestButtonLogic && jumpRequestIntensity != 0f)
        {
            var originalJumpRequestIntensity = jumpRequestIntensity;
            jumpRequestIntensity = jumpRequestIntensity < 0 ? -1 : 1;
            Debug.Log("PJ: Adjusting Requested jump original: " + originalJumpRequestIntensity + "new: " + jumpRequestIntensity);
        }

        if (jumpRequestIntensity == 0) return;
        if (CanPlayerJump())
        {
            DoJump(jumpRequestIntensity);
        }
    }

    #region jumping   

    public void RequestJumpReset()
    {
        // reseting jump counter if user becomes NotGrounded --> grounded
        var isPlayerGrounded = IsPlayerGrounded();
        if (isPlayerGrounded != previousGroundedSituation)
        {
            if (previousGroundedSituation == false)
            {
                if (isPlayerGrounded)
                {
                    ResetJump();
                }
            }
            previousGroundedSituation = isPlayerGrounded;
        }

        // sometimes the player tries to jump, but does not become NotGrounded (e.g. hits obstacle), than we reset also after some time
        if ((jumpCount > 0) && (isPlayerGrounded) && (Time.time >= timeForPossibleJumpReset))
        {
            ResetJump();
        }
    }

    private bool CanPlayerJump()
    {
        // clear border conditions
        if (MaxJumpCount <= 0) return false;
        if (jumpCount >= MaxJumpCount) return false;

        // in case player just starts his first jump
        // player must be grounded and has landed on the ground meaning he has no negative speed anymore
        if (jumpCount == 0 && IsPlayerGrounded() && playerRigidbody2D.velocity.y > -1)
            return true;

        // in case player wanting perform another jumps (multijumps) - we allow it after some period of time from previosu jump
        if ((jumpCount > 0) && (Time.time >= timeForNextJump))
            return true;

        return false;
    }

    private void DoJump(float jumpRequestIntensity)
    {
        playerRigidbody2D.AddForce(new Vector2(0f, jumpRequestIntensity * MaxJumpForce));
        jumpCount++;
        timeForNextJump = Time.time + MinDelayBetweenJumps;
        timeForPossibleJumpReset = Time.time + MinDelayBetweenJumps;
        Debug.Log("PJ: Jump intensity:" + jumpRequestIntensity + "  maxJumpForce:" + MaxJumpForce + "  count:" + jumpCount + " player Y velocity: " + playerRigidbody2D.velocity.y);
    }

    private void ResetJump()
    {
        jumpCount = 0;
        timeForNextJump = 0;
        timeForPossibleJumpReset = 0;
        Debug.Log("PJ: Jump RESET count:" + jumpCount);
    }

    public bool IsPlayerGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, groundedRadius, WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != thePlayer) return true;
        }

        return false;
    }

    #endregion 
}
