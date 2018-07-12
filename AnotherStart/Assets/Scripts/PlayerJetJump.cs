using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerJetJump : MonoBehaviour {

    [SerializeField] public float MaxJumpForce = 200;    
    [SerializeField] public float MinDelayBetweenJumps = 0.25f;
    [SerializeField] public float JumpRequestConsideredZero = 0.2f;
    [SerializeField] public bool JumpRequestButtonLogic = true;
    [SerializeField] public bool AllowNegativeJump = false;
    [SerializeField] public LayerMask WhatIsGround;    
    
    private float timeForNextJump = 0f;
    private Rigidbody2D playerRigidbody2D;        

    private void Awake()
    {                
        playerRigidbody2D = GetComponent<Rigidbody2D>();        
    }

    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per fixed time delta
    void FixedUpdate()
    {       
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
    
    public void Jump(float jumpRequestIntensity)
    {
        if (Math.Abs(jumpRequestIntensity) < JumpRequestConsideredZero && jumpRequestIntensity != 0)
        {
            //Debug.Log("PJJ: NULLing Requested jump intensity: " + jumpRequestIntensity);
            jumpRequestIntensity = 0;
        }
        if (JumpRequestButtonLogic && jumpRequestIntensity != 0f)
        {
            var originalJumpRequestIntensity = jumpRequestIntensity;
            jumpRequestIntensity = jumpRequestIntensity < 0 ? -1 : 1;
            //Debug.Log("PJJ: Adjusting Requested jump original: " + originalJumpRequestIntensity + "new: " + jumpRequestIntensity);
        }
        if (!AllowNegativeJump)
        {
            if (jumpRequestIntensity < 0)
                jumpRequestIntensity = 0;
        }

        if (jumpRequestIntensity == 0) return;

        if (CanPlayerJump())
        {
            DoJump(jumpRequestIntensity);
        }
    }

   

    #region jumping

    public void RequestJump(float jumpRequestIntensity)
    {
        
    }

    private bool CanPlayerJump()
    {
        // player can jump next time after some time elapsed
        if (Time.time >= timeForNextJump)
            return true;

        return false;
    }

    private void DoJump(float jumpRequestIntensity)
    {
        playerRigidbody2D.AddForce(new Vector2(0f, jumpRequestIntensity * MaxJumpForce));
        timeForNextJump = Time.time + MinDelayBetweenJumps;
        Debug.Log("PJJ: Jump intensity:" + jumpRequestIntensity + "  maxJumpForce:" + MaxJumpForce + " player Y velocity: " + playerRigidbody2D.velocity.y);
    }

    
    #endregion 
}
