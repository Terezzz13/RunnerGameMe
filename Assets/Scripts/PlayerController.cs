using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] AudioSource characterSounds;
    //[SerializeField] AudioClip jump;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] float shiftSpeed = 10f;
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float movementSpeed = 5f;
    Vector3 direction;
    bool isGrounded = true;
    float currentSpeed;
    float stamina = 5f;
    int health;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        currentSpeed = movementSpeed;
        health = 100;
    }
    public void ChangeHealth(int count)
    {
        health -= count;
        if (health <= 0)
        {
            anim.SetBool("Die", true);
            this.enabled = false;
        }
    }
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);

        if (direction.x != 0 || direction.z != 0)
        {
            anim.SetBool("Walk", true);
            //if (!characterSounds.isPlaying && isGrounded)
            //{
            //    characterSounds.Play();
            //}
        }
        if (direction.x == 0 && direction.z == 0)
        {
            anim.SetBool("Walk", false);
            //characterSounds.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
            //characterSounds.Stop();
            //AudioSource.PlayClipAtPoint(jump, transform.position);
            anim.SetBool("SprintJump", true);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (stamina > 0)
            {
                stamina -= Time.deltaTime;
                currentSpeed = shiftSpeed;
            }
            else
            {
                currentSpeed = movementSpeed;
            }
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            stamina += Time.deltaTime;
            currentSpeed = movementSpeed;
            anim.SetBool("Walk", false);
            anim.SetBool("SprintJump", false);
            anim.SetBool("SprintSlide", true);
        }
        if (stamina > 5f)
        {
            stamina = 5f;
        }
        else if (stamina < 0)
        {
            stamina = 0;
            anim.SetBool("Walk", true);
            anim.SetBool("SprintJump", false);
            anim.SetBool("SprintSlide", false);
        }

    }
   

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * currentSpeed * Time.fixedDeltaTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        anim.SetBool("SprintJump", false);
    }
    private void OnTriggerEnter(Collider other)
    {
                
    }
}