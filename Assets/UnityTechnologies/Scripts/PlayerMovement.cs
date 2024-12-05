using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 5f;
    public float speed = 5f; 
    public float speedBoostMultiplier = 2.5f; 
    public float speedBoostDuration = 5f; 

    private bool isSpeedBoostActive = false; 
    private float originalSpeed; 

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
        originalSpeed = speed; 
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        Vector3 rootMotion = m_Animator.deltaPosition;
        rootMotion.y = 0f; 

        m_Rigidbody.MovePosition(m_Rigidbody.position + rootMotion * (speed / originalSpeed));
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    public void ActivateSpeedBoost()
    {
        if (!isSpeedBoostActive)
        {
            StartCoroutine(SpeedBoostCoroutine());
        }
        else
        {
            Debug.Log("Speed Boost already active!");
        }
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        Debug.Log("Speed Boost Activated! Original Speed: " + originalSpeed);
        isSpeedBoostActive = true;

        speed = originalSpeed * speedBoostMultiplier;
        Debug.Log("Boosted Speed: " + speed);

        yield return new WaitForSeconds(speedBoostDuration);

        speed = originalSpeed;
        isSpeedBoostActive = false;

        Debug.Log("Speed Boost Ended! Reset Speed: " + speed);
    }
}
