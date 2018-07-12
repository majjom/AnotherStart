using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerLeftRightMove : MonoBehaviour {

    // for moving
    [SerializeField] public float MaxSpeed = 10f;
    [SerializeField] public float SpeedRequestConsideredZero = 0.2f;
    [SerializeField] public bool SpeedRequestButtonLogic = false;
    
    private float previouslyRequestedSpeedIntensity;
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
    
    public void Move(float moveRequestIntensity)
    {
        if (Math.Abs(moveRequestIntensity) < SpeedRequestConsideredZero && moveRequestIntensity != 0)
        {
            Debug.Log("PLRM: NULLing Requested speed intensity: " + moveRequestIntensity);
            moveRequestIntensity = 0;
        }
        if (SpeedRequestButtonLogic && moveRequestIntensity != 0f)
        {
            var originalMoveRequestIntensity = moveRequestIntensity;
            moveRequestIntensity = moveRequestIntensity < 0 ? -1 : 1;
            Debug.Log("PLRM: Adjusting Requested speed original: " + originalMoveRequestIntensity + "new: " + moveRequestIntensity);
        }

        playerRigidbody2D.velocity = new Vector2(moveRequestIntensity * MaxSpeed, playerRigidbody2D.velocity.y);

        if (previouslyRequestedSpeedIntensity != moveRequestIntensity)
            Debug.Log("PLRM: Player velocity: " + playerRigidbody2D.velocity);
        previouslyRequestedSpeedIntensity = moveRequestIntensity;
    }    
}
