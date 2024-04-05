using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float jumpForce = 8.0f;
    private float currentSpeed;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Vector3 DeathScreen;
    private int currentHealth;
    public TextMeshProUGUI healthText;
    public bool isDead = false;
    public bool hasWon = false;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip winSound;

    [Header("Look Settings")]
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float upDownLimit = 65f;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip sprintSound;
    private AudioSource audioSource;
    private Coroutine audioCoroutine;

    private float verticalRotation;
    private Camera playerCamera;
    private Vector3 currentMovement = Vector3.zero;
    private CharacterController characterController;

    public GameObject endGame;
    public GameObject winGame;

    private Vector3 checkpointPosition;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        currentSpeed = walkSpeed;
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (!isDead)
        {
            HandleMovement();
            HandleLook();
            healthText.text = "Health: " + currentHealth;

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            if (characterController.isGrounded)
            {
                if (Input.GetKey(KeyCode.LeftShift) && IsMovingOnGround())
                {
                    currentSpeed = sprintSpeed;
                    if (audioSource.clip != sprintSound || !audioSource.isPlaying)
                    {
                        PlaySound(sprintSound);
                    }
                }
                else if (IsMovingOnGround() && !audioSource.isPlaying)
                {
                    currentSpeed = walkSpeed;
                    PlaySound(walkSound);
                }
            }
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthText.text = "Health: " + currentHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Cursor.visible = true;
        isDead = true;
        endGame.SetActive(true);
        Debug.Log("Player died!");

        transform.position = DeathScreen;

        GameObject pauseGameObject = GameObject.Find("PauseGame");
        if (pauseGameObject != null)
        {
            pauseGameObject.SetActive(false);
        }

        // Play the death sound
        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }

    void HandleMovement()
    {
        if (PlatformTeleporter.playerTeleporting)
        {
            return; 
        }

        Vector3 horizontalMovement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        horizontalMovement = transform.rotation * horizontalMovement;

        currentMovement.x = horizontalMovement.x * currentSpeed;
        currentMovement.z = horizontalMovement.z * currentSpeed;

        currentMovement.y += Physics.gravity.y * Time.deltaTime;

        characterController.Move(currentMovement * Time.deltaTime);

    }

    void HandleLook()
    {
        float mouseXRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownLimit, upDownLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    void Jump()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpForce);
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            if (audioCoroutine != null)
            {
                StopCoroutine(audioCoroutine);
                audioCoroutine = null;
            }
        }
    }

    private bool IsMovingOnGround()
    {
        Vector3 horizontalVelocity = characterController.velocity;
        horizontalVelocity.y = 0; 
        return horizontalVelocity.magnitude > 0;
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioCoroutine != null)
        {
            StopCoroutine(audioCoroutine);
        }
        audioCoroutine = StartCoroutine(PlaySoundCoroutine(clip));
    }

    private IEnumerator PlaySoundCoroutine(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(clip.length);
    }

    private void Win()
    {
        Cursor.visible = true;
        winGame.SetActive(true);
        Debug.Log("Player won!");

        GameObject pauseGameObject = GameObject.Find("PauseGame");
        if (pauseGameObject != null)
        {
            pauseGameObject.SetActive(false);
        }

        isDead = true;

        // Play the win sound
        if (winSound != null)
        {
            audioSource.PlayOneShot(winSound);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinPoint"))
        {
            Win();
        }
    }
}
