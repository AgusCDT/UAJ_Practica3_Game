using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Manager : MonoBehaviour
{
    #region parameters
    public int dir;
    #endregion

    #region properties
    [HideInInspector] public bool InDetectionZone = false; // Booleano que detecta si se está dentro de la zona de interacción con algun objeto interactuable
    public bool _leverActivated = false; //Palanca
    public bool _pistolaActivada = false;
    public bool _escopetaActivada = false;
    public bool _isMoving = false;
    #endregion

    #region references
    [SerializeField] private Animator _myAnimator;
    private Player_MovementController _myPlayerMovementController;
    private Player_Attack _myPlayerAttack;
    private Player_Interact _myPlayerInteract;
    private Player_Life_Component _myPlayerLifeComponent;
    [SerializeField] private GameObject _nota;
    private ActivaNota _myActivaNota;
    #endregion

    void Start()
    {
        _myPlayerMovementController = GetComponent<Player_MovementController>();
        _myPlayerLifeComponent = GetComponent<Player_Life_Component>();
        _myPlayerAttack = GetComponent<Player_Attack>();
        _myPlayerInteract = GetComponent<Player_Interact>();
        _myActivaNota = _nota.GetComponent<ActivaNota>();
    }

    void Update()
    {
        Vector3 movementDirection = Vector3.zero;

        // Detección de movimiento WASD
        if (!_myPlayerLifeComponent._isPlayerDead && !GameManager.Instance.IsGamePaused)
        {
            if (Input.GetKey(KeyCode.W))
            {
                _isMoving = true;
                // Arriba
                movementDirection.y = 1.0f;
                dir = 1;
                _myPlayerMovementController._movementSpeed = 5.0f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _isMoving = true;
                // Abajo
                movementDirection.y = -1.0f;
                dir = 2;
                _myPlayerMovementController._movementSpeed = 5.0f;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                _isMoving = true;
                // Izquierda
                movementDirection.x = -1.0f;
                dir = 3;
                _myPlayerMovementController._movementSpeed = 5.0f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _isMoving = true;
                // Derecha
                movementDirection.x = 1.0f;
                dir = 4;
                _myPlayerMovementController._movementSpeed = 5.0f;
            }
            else
            {
                _isMoving = false;
            }

            AnimatorManager.Instance.IsMoving(_myAnimator, _isMoving, dir);
            _myPlayerMovementController.SetDirection(movementDirection);

            // Interacción con objetos
            if (InDetectionZone)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _myPlayerInteract.ToCallInteraction(); // Cambia booleano del script attack
                }
            }

            // Selección de armas
            if (_leverActivated)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _myPlayerAttack.ShootBala(1,dir);
                }
            }
            if (_pistolaActivada)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _myPlayerAttack.ShootBala(2, dir);
                }
            }

            if (_escopetaActivada)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    _myPlayerAttack.ShootBala(3, dir);
                }
            }

            // Menu pausa
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.PauseGame();
                _myPlayerMovementController._movementSpeed = 0;
            }
        }
    }
}
