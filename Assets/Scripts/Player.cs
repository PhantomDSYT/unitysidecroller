using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private bool superJumpKeyWasPressed;
    private int coins = 0;
    private int superJumpsRemaining = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            superJumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0);

        if (superJumpKeyWasPressed && superJumpsRemaining > 0)
        {
            rigidbodyComponent.AddForce(Vector3.up * 12, ForceMode.VelocityChange);
            superJumpKeyWasPressed = false;
            superJumpsRemaining--;
        }
        
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (jumpKeyWasPressed)
        {
            rigidbodyComponent.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            coins++;
            Destroy(other.gameObject);
        }

        if(other.gameObject.layer == 10)
        {
            superJumpsRemaining++;
            Destroy(other.gameObject);
        }
    }
}
