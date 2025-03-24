using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    #region properties
    bool opening = false;
    bool elevator = false;
    bool _isZombieDead = false;
    bool _isPlayerDead = false;
    private float delay = 2.2f;
    #endregion

    #region references
    private Animator animator_;
    static private AnimatorManager _instance;
    static public AnimatorManager Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion

    #region methods
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void OpenDoor(Animator animator)
    {
        opening = true;
        animator.SetBool("abriendo", opening);
    }
    public void OpenElevator(Animator animator)
    {
        elevator = true;
        animator_ = animator;
      
        Invoke(nameof(StartAnimElevator), delay);
    }

    private void StartAnimElevator()
    {
        animator_.SetBool("Elevator", elevator);
    }


    public void EnemyMovement(Animator animator, float positionx, float positiony, Transform player, Transform zombie, bool detection)
    {
        animator.SetFloat("Posx", positionx);
        animator.SetFloat("Posy", positiony);
        animator.SetFloat("Puntox", player.position.x - zombie.position.x);
        animator.SetFloat("Puntoy", player.position.y - zombie.position.y);
        animator.SetBool("Jugador", detection);
    }
    public void PlayerMovement(Animator animator, float _velocityX, float _velocityZ, float _movementSpeed)
    {
        if (!GameManager.Instance.IsGamePaused)
        {
            _velocityX = Input.GetAxisRaw("Horizontal") * _movementSpeed;
            _velocityZ = Input.GetAxisRaw("Vertical") * _movementSpeed;

            animator.SetFloat("VelocityX", _velocityX);
            animator.SetFloat("VelocityZ", _velocityZ);
        }
        else if (GameManager.Instance.IsGamePaused)
        {
            _velocityX = Input.GetAxisRaw("Horizontal") * 0;
            _velocityZ = Input.GetAxisRaw("Vertical") * 0;

            animator.SetFloat("VelocityX", _velocityX);
            animator.SetFloat("VelocityZ", _velocityZ);
        }
    }
    public void IsMoving(Animator animator, bool _isMoving, int dir)
    {
        animator.SetBool("Movimiento", _isMoving);
        animator.SetInteger("Dir", dir);
    }
    public void ZombieisDead(Animator animator)
    {
        _isZombieDead = true;
        animator.SetBool("ZombieMuerto", _isZombieDead);
    }
    public void PlayerisDead(Animator animator)
    {
        _isPlayerDead = true;
        animator.SetBool("isDead", _isPlayerDead);
    }
    #endregion
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
