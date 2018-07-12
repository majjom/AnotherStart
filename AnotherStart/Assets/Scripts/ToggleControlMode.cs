using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleControlMode : MonoBehaviour {

    public Text statusText;

    private PlayerJump playerJumpScript;
    private PlayerJetJump playerJetJumpScript;
    private PlayerFly playerFlyScript;
    private Rigidbody2D playerRigidbody2D;

    private int modeId;

    public void ToggleJumpModes()
    {
        modeId++;

        if (modeId % 3 == 0)
        {
            // player jump
            playerJumpScript.enabled = true;
            playerJetJumpScript.enabled = false;
            playerFlyScript.enabled = false;
            playerRigidbody2D.gravityScale = 2;            
        }
        else if (modeId % 3 == 1)
        {
            // player jet jump
            playerJumpScript.enabled = false;
            playerJetJumpScript.enabled = true;
            playerFlyScript.enabled = false;
            playerRigidbody2D.gravityScale = 2;
        }
        else
        {
            // player fly
            playerJumpScript.enabled = false;
            playerJetJumpScript.enabled = false;
            playerFlyScript.enabled = true;
            playerRigidbody2D.gravityScale = 0;
        }

        statusText.text = GetModeText(modeId);
    }
    

    // Use this for initialization
    void Start () {
        playerJumpScript = GetComponent<PlayerJump>();
        playerJetJumpScript = GetComponent<PlayerJetJump>();
        playerFlyScript = GetComponent<PlayerFly>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();

        ToggleJumpModes();
    }

    private string GetModeText(int modeId)
    {
        if (modeId % 3 == 0) return "Jump";
        if (modeId % 3 == 1) return "Jet Jump";
        return "Fly";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
