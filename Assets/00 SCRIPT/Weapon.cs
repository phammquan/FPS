using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class Weapon : MonoBehaviour
{
    public int damage;
    public float fireRate;
    private float nextFire;

    public int bangdan = 5;
    public int dan;

    public int dan_bangdan;

    [Space] public Camera camera;

    [Header("hitVFX")] public GameObject hitVFX;

    [Header("DanText")] public TextMeshProUGUI danText;
    public TextMeshProUGUI BangdanText;
    private bool _isFiring;
    private float timeLoad;
    public float timeLoad1;
    public float timeLoad2;

    public bool isReloading;


    [Header("Animation")] Animator animator;

    [Header("Recoil")]
    // [Range(0, 1f)]

    // public float recoilPercent = 0.3f;
    [Range(0, 2)]
    public float recoverPercent = 0.7f;

    [Space] public float recoilUp = 1f;
    public float recoilBack = 0f;

    private Vector3 originalPosition;
    private Vector3 recoilVelocity = Vector3.zero;


    private bool recoiling;
    private bool recovering;

    private float recoilLength;
    private float recoverLength;

    [Header("Aim")] private Vector3 aimPosition;
    private Vector3 aimPosition1;

    private Vector3 originalPositionAim;

    public float aim;

    [Header("Recoil")] public bool semi;
    private bool isAiming = false;


    void Awake()
    {
        animator = this.transform.GetComponent<Animator>();

        BangdanText.text = bangdan.ToString();
        danText.text = dan.ToString() + "/" + dan_bangdan.ToString();
        originalPosition = transform.localPosition;

        recoilLength = 0f;
        recoverLength = 1 / fireRate * recoverPercent;

        originalPositionAim = transform.localPosition;
        aimPosition = new Vector3(0, originalPosition.y, originalPosition.z);
        aimPosition1 = new Vector3(-aim, originalPosition.y, originalPosition.z);
    }


    // Update is called once per frame
    void Update()
    {
        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }

        if (semi == true)
        {
            if (Input.GetMouseButtonDown(0) && nextFire <= 0 && !isReloading)
            {
                if (dan > 0)
                {
                    animator.SetTrigger("Fire");
                    _isFiring = true;
                    nextFire = 1 / fireRate;
                    dan--;
                    Fire();
                    BangdanText.text = bangdan.ToString();
                    danText.text = dan.ToString() + "/" + dan_bangdan.ToString();
                }
                else if(bangdan > 0 && !_isFiring && !isReloading)
                {
                    if (dan <= 0)
                    {
                        timeLoad = timeLoad1;
                        animator.SetTrigger("Reload");
                    }
                    else
                    {
                        timeLoad = timeLoad2;
                        animator.SetTrigger("Reload2");
                    }

                    StartCoroutine(Reload());
                }
                
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && nextFire <= 0 && !isReloading)
            {
                if (dan > 0)
                {
                    animator.SetTrigger("Fire");
                    _isFiring = true;
                    nextFire = 1 / fireRate;
                    dan--;
                    Fire();
                    BangdanText.text = bangdan.ToString();
                    danText.text = dan.ToString() + "/" + dan_bangdan.ToString();
                }
                else if(bangdan > 0 && !_isFiring && !isReloading)
                {
                    if (dan <= 0)
                    {
                        timeLoad = timeLoad1;
                        animator.SetTrigger("Reload");
                    }
                    else
                    {
                        timeLoad = timeLoad2;
                        animator.SetTrigger("Reload2");
                    }

                    StartCoroutine(Reload());
                }
            }
        }


        if (Input.GetMouseButtonUp(0) || dan <= 0)
        {
            _isFiring = false;
        }

        if (Input.GetKeyDown(KeyCode.R) && bangdan > 0 && !_isFiring && !isReloading)
        {
            if (dan <= 0)
            {
                timeLoad = timeLoad1;
                animator.SetTrigger("Reload");
            }
            else
            {
                timeLoad = timeLoad2;
                animator.SetTrigger("Reload2");
            }

            StartCoroutine(Reload());
        }

        if (Input.GetMouseButtonDown(1))
        {
            isAiming = !isAiming;
        }

        if (isAiming)
        {
            Aim();
        }
        else
        {
            UnAim();
        }

        if (recoiling)
        {
            Recoil();
        }

        if (recovering)
        {
            Recover();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(timeLoad);
        if (bangdan > 0)
        {
            bangdan--;
            dan = dan_bangdan;
        }

        BangdanText.text = bangdan.ToString();
        danText.text = dan.ToString() + "/" + dan_bangdan.ToString();
        isReloading = false;
    }

    public void Fire()
    {
        recoiling = true;
        recovering = false;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f))
        {
            PhotonNetwork.Instantiate(hitVFX.name, hit.point, Quaternion.identity);
            if (hit.transform.gameObject.GetComponent<Health>())
            {
                if (damage >= hit.transform.gameObject.GetComponent<Health>().health)
                {
                    PhotonNetwork.LocalPlayer.AddScore(1);
                }
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            }
        }
    }

    void Recoil()
    {
        Vector3 finalPosition = new Vector3(originalPosition.x, originalPosition.y + recoilUp,
            originalPosition.z - recoilBack);

        transform.localPosition =
            Vector3.SmoothDamp(transform.localPosition, finalPosition, ref recoilVelocity, recoilLength);

        if (transform.localPosition == finalPosition)
        {
            recoiling = false;
            recovering = true;
        }
    }

    void Recover()
    {
        Vector3 finalPosition = originalPosition;
        transform.localPosition =
            Vector3.SmoothDamp(transform.localPosition, finalPosition, ref recoilVelocity, recoverLength);

        if (transform.localPosition == finalPosition)
        {
            recoiling = false;
            recovering = false;
        }
    }

    void Aim()
    {
        originalPosition = aimPosition;
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, aimPosition1, 9 * Time.deltaTime);
    }

    void UnAim()
    {
        originalPosition = originalPositionAim;
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, originalPosition, 9 * Time.deltaTime);
    }
}