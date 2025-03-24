using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour
{
    #region parameters
    [SerializeField] List<Transform> wayPoints;
    [SerializeField] private float velocity = 2.0f;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _enemy;
    [SerializeField] private int _distanceToDetection;
    private float changeDistance = 0.1f; // distancia minima a la que el jugador cambiará su objetivo al siguiente punto
    private int nextPosition = 0; // posicion
    private int currentPosition = 0;
    private float positionx;
    private float positiony;
    private bool detection = false; //Bool para la animación
    
    #endregion

    #region references
    private Transform _myTransform;
    [SerializeField] private Animator _myAnimator;
    private Life_Component _myLifeComponent;
    private AudioSource _Zombie;
    #endregion

    #region methods
    private void DeteccionPlayer()
    {
        _myTransform.position = Vector2.MoveTowards(_myTransform.position, _playerTransform.position, velocity * Time.deltaTime);
    }
    #endregion

    void Start()
    {
        _myTransform = transform;
        _myLifeComponent = GetComponent<Life_Component>();
        _playerTransform = GameManager.Instance._player.transform;
        _Zombie = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!GameManager.Instance.IsGamePaused && !_myLifeComponent._isZombieDead)
        {
            if (Vector2.Distance(_myTransform.position, _playerTransform.position) <= _distanceToDetection) // si la distancia entre player y enemy es menor que 5, sigue al playerzzºz
            {
                DeteccionPlayer();
                detection = true;
            }
            else
            {
                _myTransform.position = Vector3.MoveTowards(_myTransform.position, wayPoints[nextPosition].transform.position, velocity * Time.deltaTime);
                if (Vector3.Distance(_myTransform.position, wayPoints[nextPosition].transform.position) <= changeDistance) // si la distancia es menor a la distancia de cambio, se pasa al siguiente waypoint
                {
                    currentPosition = nextPosition;
                    nextPosition++;

                    if (nextPosition >= wayPoints.Count)
                    {
                        nextPosition = 0;
                    }
                }
                detection = false;             
            }
            
        }

        if (!SoundManager.Instance.zombieMuted)
        {
            _Zombie.mute = false;
        }
        else
        {
            _Zombie.mute = true;
        }

        //Animación
        positionx = wayPoints[nextPosition].position.x - transform.position.x;
        positiony = wayPoints[nextPosition].position.y - transform.position.y;
        AnimatorManager.Instance.EnemyMovement(_myAnimator, positionx, positiony, _playerTransform, _enemy.transform, detection);
    }
}
