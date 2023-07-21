using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour {
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float changeSizeTime;
    [SerializeField] private float changeSizeInterval;

    [SerializeField] private CinemachineVirtualCamera[] cams;

    private Animator animator;
    private Rigidbody2D rb;

    private float horizontal;
    private bool shouldJump;
    private bool grounded;

    private int sizeIndex;
    private bool changingSize;
        
    void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (grounded && !shouldJump && Input.GetButtonDown("Jump")) {
            shouldJump = true;
        }

        if (!changingSize) {
            // Start coroutines to change size
            if (sizeIndex < 2 && Input.GetKeyDown(KeyCode.P)) {
                StartCoroutine(ChangeSize(true));
            }

            if (sizeIndex > 0 && Input.GetKeyDown(KeyCode.L)) {
                StartCoroutine(ChangeSize(false));
            }

            // Handle left/right flipping
            Vector3 localScale = transform.localScale;
            if (horizontal > 0) {
                localScale.x = Mathf.Abs(localScale.x);
            } else if (horizontal < 0) {
                localScale.x = -Mathf.Abs(localScale.x);
            }

            localScale.z = 1;
            transform.localScale = localScale;
        }

        animator.SetFloat("xSpeed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("Grounded", grounded);
    }

    void FixedUpdate() {
        Vector2 currVelocity = rb.velocity;
        currVelocity.x = horizontal * moveSpeed * Time.fixedDeltaTime;

        // TODO: Change ground check to use box instead of circle
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.05f, groundLayer);

        if (grounded && shouldJump) {
            // TODO: Probably better to just index into an array of predefined jump velocities, easier to test
            float largeFactor = sizeIndex * 2f;
            currVelocity.y = jumpSpeed + largeFactor;
            shouldJump = false;
        }
        rb.velocity = currVelocity;
    }

    IEnumerator ChangeSize(bool grow) {
        changingSize = true;

        Vector3 localScale = transform.localScale;
        Vector3 target = grow ? localScale * 2 : localScale / 2;
        target.z = 1;
        float currTime = 0f;

        int nextIndex = sizeIndex = grow ? sizeIndex + 1 : sizeIndex - 1;

        cams[sizeIndex].m_Priority = 0;
        yield return null;
        cams[nextIndex].m_Priority = 10;

        while (currTime < changeSizeTime) {
            Vector3 currScale = Vector3.Lerp(localScale, target, currTime / changeSizeTime);

            // Handle left/right flipping
            if (horizontal > 0) {
                currScale.x = Mathf.Abs(currScale.x);
            } else if (horizontal < 0) {
                currScale.x = -Mathf.Abs(currScale.x);
            }
            transform.localScale = currScale;

            yield return new WaitForSeconds(changeSizeInterval);
            currTime += changeSizeInterval;
        }

        // Set scale one final time
        if (horizontal > 0) {
            target.x = Mathf.Abs(target.x);
        } else if (horizontal < 0) {
            target.x = -Mathf.Abs(target.x);
        }
        transform.localScale = target;

        sizeIndex = nextIndex;
        changingSize = false;
    }
}
