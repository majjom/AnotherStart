using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour {

    private PlayerLeftRightMove playerMoveScript;
    private PlayerJump playerJumpScript;
    private PlayerJetJump playerJetJumpScript;
    private PlayerFly playerFlyScript;

    private void Awake()
    {
        playerMoveScript = GetComponent<PlayerLeftRightMove>();
        playerJumpScript = GetComponent<PlayerJump>();
        playerJetJumpScript = GetComponent<PlayerJetJump>();
        playerFlyScript = GetComponent<PlayerFly>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    // Update is called once per fixed time delta
    void FixedUpdate()
    {
        /*
         * This is from Joystic of standard assest
        var moveRequest = CrossPlatformInputManager.GetAxis("Horizontal");
        var jumpRequest = CrossPlatformInputManager.GetAxis("Vertical");        
        */

        // this is from Joystick from SimpleInput system
        var moveRequest = SimpleInput.GetAxis("Horizontal");
        var jumpRequest = SimpleInput.GetAxis("Vertical");

        if (playerMoveScript != null && playerMoveScript.isActiveAndEnabled)
            playerMoveScript.Move(moveRequest);

        if (playerJumpScript != null && playerJumpScript.isActiveAndEnabled)
            playerJumpScript.Jump(jumpRequest);

        if (playerJetJumpScript != null && playerJetJumpScript.isActiveAndEnabled)
            playerJetJumpScript.Jump(jumpRequest);

        if (playerFlyScript != null && playerFlyScript.isActiveAndEnabled)
            playerFlyScript.Fly(moveRequest, jumpRequest);

    }
}
