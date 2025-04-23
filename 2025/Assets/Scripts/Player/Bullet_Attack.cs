using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Telemetry;

public class Bullet_Attack : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _velocity = 1.0f;
    [SerializeField]
    private int _damage = 1;
    [SerializeField]
    private float _life = 1.0f;
    private float _timer;
    [SerializeField]
    private float empuje = 2f; //Fuerza con la que se va a ipulsar hacia atrás al zombie al ser golpeado por un ataque
    #endregion

    #region references
    private Transform _mytransform;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision) //Cuando colisione el misil
    {
        Life_Component hitZombie = collision.GetComponent<Life_Component>();
        ColisionParedes hitWalls = collision.GetComponent<ColisionParedes>();

        if (hitZombie)
        {
            SoundManager.Instance.PlayOneShot(FMODEventsManager.Instance.zombieDamaged, hitZombie.transform.position);
            hitZombie.Damage(_damage);
            Destroy(gameObject);
            var heading = _mytransform.position - hitZombie.transform.position;
            collision.attachedRigidbody.AddForce(heading * -empuje, ForceMode2D.Impulse);
            Telemetry.Telemetry.Instance.TrackEvent(new HitEvent(Telemetry.Event.ID_Event.HIT, gameObject.name, 0));
        }

        if (hitWalls)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    void Start()
    {
        _mytransform = transform;
    }

    void Update()
    {
        transform.Translate(_velocity * Vector3.right * Time.deltaTime);
        _timer += Time.deltaTime;

        if (_timer >= _life)
        {
            Destroy(gameObject);
        }
    }
}