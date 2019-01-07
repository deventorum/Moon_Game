using UnityEngine;
using TMPro;

public class Space_Daddy : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    private float turnSpeed = 100.0f;
    private bool groundedState;
    private float jumpForce = 5000.0f;
    private float forwardForce = 0.5f;
    private float sidewaysForce = 1.0f;
    private Vector3 maxVelocity;
    private Vector3 maxSideVelocity;
    private bool onPlatform;

    AudioSource movementAudio;
    AudioSource landingAudio;
    AudioSource damageAudio;


    private int livesRemaining;
    public const int TOTAL_LIVES = 3;
    public Vector3 origPosition;

    public Collider[] colliderBoxes;
    private static readonly int AnimationPar = Animator.StringToHash("AnimationPar");

    // Predetermined spawn location coordinates for spaceman
    public Vector3[] positions = {
        new Vector3(-396.47f, 0.48f, 331.1f),
        new Vector3(-396.47f, 0.48f, 298.1f),
        new Vector3(-496.47f, 0.48f, 417.1f)
    };

    private int positionIndex;
    public TextMeshProUGUI hudLivesText;

    private void Start()

    {

        livesRemaining = TOTAL_LIVES;
        hudLivesText.text = "Lives: " + livesRemaining;
        positionIndex = 0;
        transform.position = positions[positionIndex];

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        anim = GetComponentInChildren<Animator>();

        AudioSource[] audios = GetComponents<AudioSource>();
        movementAudio = audios[0];
        landingAudio = audios[1];
        damageAudio = audios[2];
    }


    private void Update()
    {
        if (livesRemaining <= 0 || Input.GetKey("l"))
        {
            PlayerPrefs.SetInt("final_score", FindObjectOfType<GameManager>().GetCurrentGold());
            GameController.Instance.GameOver();
        }
    }

    private void FixedUpdate()
    {
        var currentForward = transform.forward;
        var currentVelocity = new Vector3();
        onPlatform = checkForPlatform(colliderBoxes[0]);
        groundedState = CheckGroundCollision(colliderBoxes[0]);
        CheckAttackCollision(colliderBoxes[1]);

        midAirControl(maxSideVelocity, groundedState);
        Accelerate(maxVelocity, groundedState, onPlatform);

        if (movementAudio.isPlaying && !groundedState)
        {
            movementAudio.Pause();
        }

        Jump(groundedState);

        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.constraints = RigidbodyConstraints.None;
            currentVelocity = Turn(Input.GetAxis("Horizontal"), rb.velocity);
            if ((transform.forward.x != currentForward.x || transform.forward.z != currentForward.z) && groundedState)
            {
                rb.velocity -= currentVelocity / 10;
                Accelerate(maxVelocity, groundedState, onPlatform);

            }

        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
        }


        if (Input.GetKeyDown("f") && !groundedState)
        {
            Flip();
        }

    }

    private bool checkForPlatform(Collider col)
    {
        Collider[] cols = Physics.OverlapBox(col.transform.position, col.transform.localScale / 2, Quaternion.identity, LayerMask.GetMask("Platform"));
        return cols.Length > 0;
    }

    private void Accelerate(Vector3 maxVelocity, bool groundedState, bool platform)
    {
        if (Input.GetAxis("Vertical") != 0)

        {
            //rb.isKinematic = false;
            anim.SetInteger(AnimationPar, 1);
            maxVelocity = transform.forward * forwardForce * 20;

            if (!movementAudio.isPlaying && groundedState)
            {
                movementAudio.Play(0);
            }
            if (platform)
            {
                maxVelocity *= 2;
            }
            if (Vector3.Distance(new Vector3(0, 0, 0), rb.velocity) < Vector3.Distance(new Vector3(0, 0, 0), maxVelocity))
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    rb.velocity += transform.forward * forwardForce;
                }
                else
                {
                    rb.velocity += transform.forward * -forwardForce;
                }
            }
        }
        else
        {
            movementAudio.Pause();
            anim.SetInteger(AnimationPar, 0);
            if (!groundedState) return;
            rb.isKinematic = true;
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            rb.isKinematic = false;
        }
    }

    private Vector3 Turn(float turn, Vector3 v)
    {
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        return v;


    }
    private void Flip()
    {

        anim.Play("Flip");
    }

    private void midAirControl(Vector3 maxSideVelocity, bool onGround)
    {
        maxSideVelocity = transform.right * forwardForce * 20;
        if (Vector3.Distance(new Vector3(0, 0, 0), rb.velocity) < Vector3.Distance(new Vector3(0, 0, 0), maxSideVelocity))
        {
            if (Input.GetKey("e"))
            {
                rb.velocity += transform.right * sidewaysForce;
            }
            else if (Input.GetKey("q"))
            {
                rb.velocity += transform.right * -sidewaysForce;
            }
        }

    }

    private void Jump(bool isGrounded)
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown("space"))
            {
                rb.AddForce(0, jumpForce * Time.deltaTime, 0, ForceMode.Impulse);
                anim.Play("Jump_start");
            }
        }
        else
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Flip"))
            {
                anim.Play("Jump_loop");
            }

        }
    }

    private bool CheckGroundCollision(Collider col)
    {
        var transform1 = col.transform;
        Collider[] cols = Physics.OverlapBox(
            transform1.position,
            transform1.localScale / 2,
            Quaternion.identity,
            LayerMask.GetMask("Terrain", "Base", "Platform")
        );

        if (cols.Length > 0 && !groundedState)
        {
            landingAudio.Play(0);
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            anim.Play("Jump_end");
        }

        return cols.Length > 0;
    }


    private void CheckAttackCollision(Collider other)
    {
        var transform1 = other.transform;
        Collider[] cols = Physics.OverlapBox(
            transform1.position,
            transform1.localScale / 2,
            Quaternion.identity,
            LayerMask.GetMask("Enemy")
        );



        if (cols.Length > 0)
        {
            if (!damageAudio.isPlaying)
            {
                damageAudio.Play(0);
            }

            if (livesRemaining > 0)
            {
                livesRemaining--;
            }

            hudLivesText.text = "Lives: " + livesRemaining;
            Respawn();
        }
    }


    private void Respawn()
    {
        if (positionIndex < 2)
        {
            transform.position = positions[++positionIndex];
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var transform1 = transform;
        Gizmos.DrawWireCube(transform1.position, transform1.localScale / 2);
    }


    protected void LateUpdate()
    {
        var transform1 = transform;
        transform1.localEulerAngles = new Vector3(0, transform1.localEulerAngles.y, 0);
    }
}


