using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speedBooster = 0.2f;
    public float jumpBooster = 10f;
    public float maxSpeed = 6;

    public Camera camera;
    private Vector3 cameraOffset;
    
    private Rigidbody rigidbody;
    private Collider collider;

    public Bank bank;
    
    public TextMeshProUGUI score;
    private int currentScore;
    
    private Animator animator;
    
    public bool isGrounded;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        animator = GetComponent<Animator>();

        cameraOffset = camera.transform.position - transform.position;
    }

    void Update()
    {
        animator.SetFloat("Speed", rigidbody.velocity.magnitude);
        animator.SetBool("Jump", !isGrounded);
        
        if (!CountDown.run)
            return;

        float step = Input.GetAxis("Horizontal");
        
        // apply the speed to the player according to his input
        rigidbody.velocity += step * Time.deltaTime * Vector3.right * speedBooster;
        // check if the speed is too high and clamp it
        rigidbody.velocity = new Vector3(Mathf.Clamp(rigidbody.velocity.x, -maxSpeed, maxSpeed),
            rigidbody.velocity.y, rigidbody.velocity.z);
            
        // check if the player touches the ground
        float halfHeight = collider.bounds.extents.y + 0.03f;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, halfHeight);

        // allows the player to jump if he touches the ground
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpBooster);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            maxSpeed = 10;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            maxSpeed = 6;
        }

        camera.transform.position = new Vector3(transform.position.x + cameraOffset.x, camera.transform.position.y,
            camera.transform.position.z);
    }

    private string NbZeros()
    {
        string zeros = "000000";
        int backupScore = currentScore;
        while (backupScore >= 1)
        {
            backupScore /= 10;
            zeros = zeros.Remove(0, 1);
        }

        return zeros;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, collider.bounds.extents.y + 1f))
        {
            if (collision.gameObject.CompareTag("Brick") && hit.transform.CompareTag("Brick"))
            {
                Destroy(collision.gameObject);
                currentScore += 100;
                score.text = "MARIO\n" + NbZeros() + currentScore;
            }
        
            else if (collision.gameObject.CompareTag("Interrogation") && hit.transform.CompareTag("Interrogation"))
            {
                bank.AddCoins(1);
                currentScore += 100;
                score.text = "MARIO\n" + NbZeros() + currentScore;
            }
        }
        
    }
}
