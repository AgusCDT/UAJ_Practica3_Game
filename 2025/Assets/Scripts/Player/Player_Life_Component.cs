using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Telemetry;

public class Player_Life_Component : Life_Component
{
    #region parameters
    [SerializeField] private float breathingSpeed = 0.0f;
    #endregion

    #region references
    private Rigidbody2D _myRigidBody;
    private Player_MovementController _myPlayerMovementController;
    private Transform _myTransform;
    private Image _lifeBar;
    public bool pushing = false;
    public bool _isPlayerDead = false;
    #endregion

    #region properties
    [SerializeField] private float push = 2f; //Fuerza con la que se va a impulsar hacia atrás al jugador al ser golpeado por un objeto hostil
    [SerializeField] private float pushZombie = 0.5f; // Fuerza con la que se va a impulsar el zombie cuando nos ataque
    #endregion

    #region methods
    public override void Damage(int DamagePoints)
    {
        base.Damage(DamagePoints);
        SoundManager.Instance.PlayOneShot(FMODEventsManager.Instance.playerDamaged, this.transform.position);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy_Behaviour enemigo = collision.gameObject.GetComponent<Enemy_Behaviour>();

        Barricada barricada = collision.gameObject.GetComponent<Barricada>();

        if (enemigo) //Si el objeto con el que colisiona es un enemigo se usa la posición del enemigo para calcular la direccion de empuje
        {
            Telemetry.Telemetry.Instance.TrackEvent(new DamageReceivedEvent(Telemetry.Event.ID_Event.DAMAGE_RECIEVED, enemigo.name, 10, false));
            SoundManager.Instance.PlayOneShot(FMODEventsManager.Instance.zombieBite, enemigo.transform.position);
            var heading = enemigo.transform.position - _myTransform.position; //Direccion de empuje    
            //Necesitamos la direccion del jugador o del zombie
            _myRigidBody.AddForce(heading.normalized * -push, ForceMode2D.Impulse);
            var direction = _myTransform.position - enemigo.transform.position;
            collision.rigidbody.AddForce(direction * -pushZombie, ForceMode2D.Impulse);
            pushing = true;
            SoundManager.Instance.SetPlayerBreathing(_currentLife);
        }

        else if (barricada) //Si el objeto con el que colisiona es la barricada se usa su posicion para calcular la direccion de empuje
        {
            var heading = barricada.transform.position - _myTransform.position; //Direccion de empuje
            //Necesitamos la direccion del jugador o del zombie
            _myRigidBody.AddForce(heading.normalized * -push / 4, ForceMode2D.Impulse);
            pushing = true;
        }
    }
    public void Cura(int curacion)
    {
        _currentLife += curacion;
        _currentLife = Mathf.Clamp(_currentLife, 0, 100);
        SoundManager.Instance.SetPlayerBreathing(_currentLife);
    }
    public void LifeBarReference()
    {
        _lifeBar = GameObject.Find("LifeBar").transform.Find("CurrentLife").GetComponent<Image>();
    }
    #endregion

    override public void Start()
    {
        base.Start();

        _myRigidBody = GetComponent<Rigidbody2D>();
        _myTransform = GetComponent<Transform>();
        _myPlayerMovementController = GetComponent<Player_MovementController>();
        SoundManager.Instance.InitializePlayerBreathing();
    }

    override public void Update()
    {
        SoundManager.Instance.UpdateBreathingPosition(this.transform.position);
        if (_currentLife <= 0)
        {
            // Para no poder moverse durante la animación de muerte
            AnimatorManager.Instance.PlayerisDead(_myAnimator);
            _myPlayerMovementController._movementSpeed = 0;
            _cont -= Time.deltaTime;
            if (_cont <= 0)
            {
                SoundManager.Instance.ReleasePlayerBreathing();
                Telemetry.Telemetry.Instance.TrackEvent(new DeathEvent(Telemetry.Event.ID_Event.DEATH));
                Die(); //GameManager.Instance.OnPlayerDies();
                GameManager.Instance.LoadGameOverMenu();
                _cont = 1.7f;
            }
        }
        _lifeBar.fillAmount = _currentLife / _maxLife;
    }
}