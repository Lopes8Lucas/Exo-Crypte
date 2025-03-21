using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float runSpeed = 20f;
    [SerializeField] private float crouchSpeed = 5f;

    [Header("Stress")]
    public float stressLevel = 0f;

    [Header("Battery")]
    public float batteryLife = 100f;
    [SerializeField] private float batteryDrainRate = 10f;

    private GameObject flashlight;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private float moveSpeed;

    private enum MovementState
    {
        walk,
        run,
        crouch
    }

    private void Start()
    {
        // Cursor.visible = false;
        rb = GetComponent<Rigidbody2D>();
        flashlight = GameObject.FindGameObjectWithTag("Flashlight");
        flashlight.SetActive(false);
    }

    private void Update()
    {
        LookAtMouse();
        Flashlight();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    //Function to look at mouse and rotate player
    private void LookAtMouse()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    //Function of movement with sprint and crouch states
    private void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed = crouchSpeed;
        }
        else moveSpeed = walkSpeed;

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        rb.linearVelocity = moveInput * moveSpeed;
    }

    private void Flashlight()
    {
        if (Input.GetKeyDown(KeyCode.F) && batteryLife > 0)
        {
            flashlight.SetActive(!flashlight.activeInHierarchy);
        }

        if (flashlight.activeInHierarchy)
        {
            if (batteryLife > 0)
            {
                batteryLife -= batteryDrainRate * Time.deltaTime;
            } else
            {
                flashlight.SetActive(false);
            }   
        }
    }
}
