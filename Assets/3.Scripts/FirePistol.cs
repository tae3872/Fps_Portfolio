using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirePistol : MonoBehaviour
{
    public GameObject bombFactory;
    public GameObject firePosition;
    public float throwPower = 15f;
    public GameObject bulletEffect;
    public GameObject[] rifleEffects;
    ParticleSystem ps;
    public bool zoomMode;
    public bool sniperMode;
    public Text weaponType;
    public GameObject grenadeImg;
    public GameObject scopeImg;
    public GameObject crossHair;
    public GameObject scopeBack;

    Animator anim;
    bool isFire = false;
    void Start()
    {
        anim = this.transform.parent.GetChild(1).GetComponent<Animator>();
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (PlayerStats.instance.weaponType == WeaponType.NONE)
            return;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            sniperMode = false;
            zoomMode = false;
            Camera.main.fieldOfView = 60f;
            weaponType.text = "1.Grenade";
            grenadeImg.SetActive(true);
            scopeImg.SetActive(false);
            scopeBack.SetActive(false);
            crossHair.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            sniperMode = true;
            weaponType.text = "  2.Scope"; 
            grenadeImg.SetActive(false);
            scopeImg.SetActive(true);
        }
        if (Input.GetButton("Fire1"))
        {
            if (!isFire)
            {
                if (PlayerStats.instance.UseAmmo(1))
                {
                    if (PlayerStats.instance.weaponType == WeaponType.PISTOL)
                    {
                        StartCoroutine(Fire(0.2f));
                    }
                    else if (PlayerStats.instance.weaponType == WeaponType.MAGNUM)
                    {
                        StartCoroutine(Fire(0.06f));
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (sniperMode)
            {
                if (!zoomMode)
                {
                    Camera.main.fieldOfView = 15f;
                    zoomMode = true;
                    scopeBack.SetActive(true);
                    crossHair.SetActive(false);
                }
                else
                {
                    Camera.main.fieldOfView = 60f;
                    zoomMode = false;
                    scopeBack.SetActive(false);
                    crossHair.SetActive(true);
                }

            }
            else
            {
                if (PlayerStats.instance.grenadeNum <= 0)
                    return;
                PlayerStats.instance.grenadeNum--;
                GameObject bomb = Instantiate(bombFactory);
                bomb.transform.position = firePosition.transform.position;

                Rigidbody rb = bomb.GetComponent<Rigidbody>();
                rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
            }
        }
    }
    IEnumerator Fire(float rate)
    {
        anim.SetTrigger("Attack");
        isFire = true;
        float attackDamage = PlayerStats.instance.attack.GetValue();
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit))
        {
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<Zombie>().TakeDamage(attackDamage);
            }
            else if (hit.transform.tag == "BreakObject")
            {
                hit.transform.GetComponent<BreakObject>().TakeDamage(1);
            }
            else if (hit.transform.tag == "Mutant")
            {
                hit.transform.GetComponent<Mutant>().TakeDamage(attackDamage);
            }
            bulletEffect.transform.position = hit.point;
            bulletEffect.transform.forward = hit.normal;
            ps.Play();
        }
        if (PlayerStats.instance.weaponType == WeaponType.MAGNUM)
        {
            int num = Random.Range(0, rifleEffects.Length - 1);
            rifleEffects[num].SetActive(true);
            AudioManager.instance.Play("PistolShot");
            yield return new WaitForSeconds(rate);
            rifleEffects[num].SetActive(false);
            isFire = false;
        }
        else if (PlayerStats.instance.weaponType == WeaponType.PISTOL)
        {
            rifleEffects[5].SetActive(true);
            rifleEffects[5].GetComponent<Animation>().Play("FireFlashAnim");
            AudioManager.instance.Play("PistolShot");
            yield return new WaitForSeconds(rate);
            rifleEffects[5].SetActive(false);
            isFire = false;
        }
    }
    void OnDrawGizmosSelected()
    {
        float maxDistance = 100f;
        Gizmos.color = Color.red;
        bool isHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, maxDistance);
        if (isHit)
        {
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
        }
        else
        {
            Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
        }
    }
}
