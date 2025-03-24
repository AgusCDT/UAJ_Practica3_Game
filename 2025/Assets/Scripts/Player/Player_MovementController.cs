using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Player_MovementController : MonoBehaviour
{
    #region parameters
    [SerializeField] public float _movementSpeed = 1.0f;
    private float _velocityX = 0.0f;
    private float _velocityZ = 0.0f;
    #endregion

    #region references
    private Transform _myTransform;
    [SerializeField] private Animator _myAnimator;
    private Rigidbody2D playerBody;
    private Player_Life_Component _myPlayerLifeComponent;
    #endregion

    #region properties
    private Vector3 _movementDirection;
    private float _count;

    #endregion

    #region methods
    public void SetDirection(Vector3 movementDirection)
    {
        _movementDirection = movementDirection.normalized;
    }

    public void PlayFootstepSound()
    {
        SoundManager.Instance.PlayOneShot(FMODEventsManager.Instance.playerFootsteps, this.transform.position);
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        playerBody = GetComponent<Rigidbody2D>();
        _myPlayerLifeComponent = GetComponent<Player_Life_Component>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_myPlayerLifeComponent.pushing)
        {
            
            Vector3 directionVector = (_movementSpeed * _movementDirection);
            playerBody.velocity = directionVector;
        }
        else
        {
            _count += Time.deltaTime;
            if (_count >= 0.9)
            {
                _count = 0;
                _myPlayerLifeComponent.pushing = false;
            }
        }

        //Animación
        AnimatorManager.Instance.PlayerMovement(_myAnimator, _velocityX, _velocityZ, _movementSpeed);
    }
}
