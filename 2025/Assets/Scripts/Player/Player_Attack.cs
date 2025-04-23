using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Telemetry;

public class Player_Attack : MonoBehaviour
{
    #region parameters
    private Vector3 rotationVector;
    [SerializeField] private int BulletsChargerPistol = 10;
    [SerializeField] private int BulletsChargerShotgun = 2;
    [SerializeField] private int PistolBulletsLeft;
    [SerializeField] private int ShotgunBulletsLeft;
    [SerializeField] private float crowbarRecharge = 1;
    [SerializeField] private float RechargePistol = 1;
    [SerializeField] private float RechargeShotgun = 2;
    private float RechargingTime;
    [SerializeField] private bool Recharging;
    [SerializeField] private int TotalPistolAmmount;
    [SerializeField] private int TotalShotgunAmmount;
    [SerializeField] private bool GotPistolBullets = false;
    [SerializeField] private bool GotShotgunBullets = false;
    [SerializeField] private bool CrowbarSpam = false;
    #endregion

    #region properties
    [SerializeField] private GameObject PalancaDash;
    [SerializeField] private GameObject PistolBullet;
    [SerializeField] private GameObject ShotgunBullet;
    #endregion

    #region references
    private Transform _mytransform;
    [SerializeField] private AudioClip _clipG;
    [SerializeField] private AudioClip _clipSg;
    #endregion

    #region methods
    public void ShootBala(int tipobala, int dir)
    {
        if (dir == 1)
        {
            rotationVector = new Vector3(0, 0, 90);
        }
        else if (dir == 2)
        {
            rotationVector = new Vector3(0, 0, -90);
        }
        else if (dir == 3)
        {
            rotationVector = new Vector3(0, 0, 180);
        }
        else if (dir == 4)
        {
            rotationVector = new Vector3(0, 0, 0);
        }

        if (tipobala == 1)
        {
            if (!CrowbarSpam)
            {
                GameObject.Instantiate(PalancaDash, _mytransform.position + new Vector3(0, 0, 0), Quaternion.Euler(rotationVector));
                CrowbarSpam = true;
                SoundManager.Instance.PlayOneShot(FMODEventsManager.Instance.crowbarSwing, this.transform.position);
                Telemetry.Telemetry.Instance.TrackEvent(new AttackEvent(Telemetry.Event.ID_Event.ATTACK, "Palanca", 0));
            }
        }
            

        else if (tipobala == 2)
        {
            if (GotPistolBullets || (PistolBulletsLeft > 0))
            {
                if (!Recharging)
                {
                    GameObject.Instantiate(PistolBullet, _mytransform.position + new Vector3(0, 0, 0), Quaternion.Euler(rotationVector));
                    PistolBulletsLeft -= 1;
                    UI_Manager.Instance.balagUI(PistolBulletsLeft, TotalPistolAmmount);
                    SoundManager.Instance.PlayOneShot(FMODEventsManager.Instance.pistolShot, this.transform.position);
                    Telemetry.Telemetry.Instance.TrackEvent(new AttackEvent(Telemetry.Event.ID_Event.ATTACK, "Pistola", PistolBulletsLeft));
                }
            }
        }
        else if (tipobala == 3)
        {
            if (GotShotgunBullets || (ShotgunBulletsLeft > 0))
            {
                if (!Recharging)
                {
                    GameObject.Instantiate(ShotgunBullet, _mytransform.position + new Vector3(0, 0, 0), Quaternion.Euler(rotationVector));
                    GameObject.Instantiate(ShotgunBullet, _mytransform.position + new Vector3(0, 0, 0), Quaternion.Euler(rotationVector + new Vector3(0, 0, 10)));
                    GameObject.Instantiate(ShotgunBullet, _mytransform.position + new Vector3(0, 0, 0), Quaternion.Euler(rotationVector + new Vector3(0, 0, 20)));
                    GameObject.Instantiate(ShotgunBullet, _mytransform.position + new Vector3(0, 0, 0), Quaternion.Euler(rotationVector + new Vector3(0, 0, -10)));
                    GameObject.Instantiate(ShotgunBullet, _mytransform.position + new Vector3(0, 0, 0), Quaternion.Euler(rotationVector + new Vector3(0, 0, -20)));
                    ShotgunBulletsLeft -= 1;
                    UI_Manager.Instance.balasgUI(ShotgunBulletsLeft, TotalShotgunAmmount);
                }
            }
        }
    }
    
    public void SumaBala(int tipobala, int cargadores)
    {
        if (tipobala == 0)
        {
            TotalPistolAmmount += cargadores * BulletsChargerPistol;
            GotPistolBullets = true;
            UI_Manager.Instance.balagUI(PistolBulletsLeft, TotalPistolAmmount);
        }
        else if (tipobala == 1)
        {
            TotalShotgunAmmount += cargadores * BulletsChargerShotgun;
            GotShotgunBullets = true;
            UI_Manager.Instance.balasgUI(ShotgunBulletsLeft, TotalShotgunAmmount);
        }
    }

    #endregion

    void Start()
    {
        _mytransform = transform;
        PistolBulletsLeft = 9; //lore interno
        ShotgunBulletsLeft = 0;
    }

    void Update()
    {
        if (CrowbarSpam)
        {
            RechargingTime += Time.deltaTime;
            if (RechargingTime >= crowbarRecharge)
            {
                CrowbarSpam = false;
                RechargingTime = 0;
            }
        }

        if ((PistolBulletsLeft == 0) && GotPistolBullets)
        {
            Recharging = true;
            RechargingTime += Time.deltaTime;
            if (RechargingTime >= RechargePistol)
            {
                SoundManager.Instance.PlaySound(_clipG);
                SoundManager.Instance.PlayOneShot(FMODEventsManager.Instance.pistolRecharge, this.transform.position);
                Recharging = false;
                PistolBulletsLeft = BulletsChargerPistol;
                RechargingTime = 0;
                TotalPistolAmmount -= BulletsChargerPistol;
                UI_Manager.Instance.balagUI(PistolBulletsLeft, TotalPistolAmmount);
            }
        }

        if ((ShotgunBulletsLeft == 0) && GotShotgunBullets)
        {
            Recharging = true;
            RechargingTime += Time.deltaTime;
            if (RechargingTime >= RechargeShotgun)
            {
                SoundManager.Instance.PlaySound(_clipSg);
                ShotgunBulletsLeft = BulletsChargerShotgun;
                RechargingTime = 0;
                Recharging = false;
                TotalShotgunAmmount -= BulletsChargerShotgun;
                UI_Manager.Instance.balasgUI(ShotgunBulletsLeft, TotalShotgunAmmount);
            }
        }
        if (TotalPistolAmmount==0)
        {
            GotPistolBullets = false;
        }
        if (TotalShotgunAmmount == 0)
        {
            GotShotgunBullets = false;
        }
    }
}
