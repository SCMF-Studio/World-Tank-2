﻿using System;
using System.Collections;
using TMPro.Examples;
using UnityEngine;

public class TS001 : MonoBehaviour
{
    public float hp = 100f; 
    public float maxHP = 100f; 
    public float currentHP;
    public float damage = 10f;
    public float speed = 2f;
    public float rotationSpeed = 200f;
    public float turnSpeed = 200f;
    public float armoSpeed = 5f;
    public float reloading = 2f;
    public float ReloadTime { get { return reloading; } }
    private Transform muzzleTransform;
    private Transform shootPos;
    [SerializeField] private GameObject bullet_standart;
    [SerializeField] private GameObject bullet_ricochet;
    private bool canShoot = true;
    private Rigidbody2D rb;
    public ParticleSystem particleDownOne, particleDownTwo, particleUpOne, particleUpTwo;

    public delegate void ReloadStartedHandler(float reloadTime);
    public event ReloadStartedHandler OnReloadStarted;


    // Boost System
    private float originalSpeed, originalHeal, originalSpeedArmo, originalReloading, originalAdditionalHP, 
        originalSmallDamage,
        originalMediumDamage,
        originalHighDamage, originalDamage;
    


    void Start()
    {
        muzzleTransform = GameObject.Find("TS-001_muzzle").transform;
        shootPos = GameObject.Find("ShootPos").transform;
        rb = GetComponent<Rigidbody2D>();

        currentHP = maxHP;


        // Boost System
        originalSpeed = speed;
        originalHeal = hp;
        originalSmallDamage = damage;
        originalMediumDamage = damage;
        originalHighDamage = damage;
        originalDamage = damage;
        originalSpeedArmo = armoSpeed;
        originalReloading = reloading;
        originalAdditionalHP = maxHP;
    }

    void Update()
    {
        
        MoveTank();
        RotateTurret();
        ParcticleSysteme();

        // Стрельба
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Shoot());
        }
    }
    public void UpdateTankStats(float newHP, float newDamage, float newSpeed)
    {
        maxHP = newHP;
        currentHP = maxHP; 
        damage = newDamage;
        speed = newSpeed;
        Debug.Log($"Танк {name}: HP = {currentHP}, Damage = {damage}, Speed = {speed}");
    }

    void MoveTank()
    {
        float MoveVerticalInput = Input.GetAxis("Vertical");
        float MoveHorizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.W))
        {
            MoveVerticalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MoveVerticalInput = -1f;
        }
        else
        {
            MoveVerticalInput = 0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveHorizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            MoveHorizontalInput = 1f;
        }
        else
        {
            MoveHorizontalInput = 0f;
        }

        transform.Translate(Vector3.up * MoveVerticalInput * speed * Time.deltaTime);
        transform.Rotate(Vector3.forward, MoveHorizontalInput * turnSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damageAmount)
    {
        currentHP -= damageAmount;

        if (currentHP <= 0)
        {
            Die();
        }

        UpdateHUD();
    }

    private void Die()
    {
        Debug.Log("Танк уничтожен!");
        Destroy(gameObject); 
    }

    private void UpdateHUD()
    {
        if (FindObjectOfType<HudBar>() != null)
        {
            HudBar hudBar = FindObjectOfType<HudBar>();
            hudBar.UpdateHPBar(currentHP, maxHP);
        }
    }

    public void SetMaxHP(float newMaxHP)
    {
        maxHP = newMaxHP;

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        UpdateHUD();
    }

    public void SetCurrentHP(float newCurrentHP)
    {
        currentHP = Mathf.Clamp(newCurrentHP, 0, maxHP);  

        UpdateHUD();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            rb.velocity = Vector3.zero;
        }
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            TakeDamage(10f); 
        }
    }

    void RotateTurret()
    {
        float rotationInput = 0f;
        if (Input.GetMouseButton(0))
        {
            rotationInput = 1f;
        }
        else if (Input.GetMouseButton(1))
        {
            rotationInput = -1f;
        }

        muzzleTransform.Rotate(Vector3.forward, rotationInput * rotationSpeed * Time.deltaTime);
    }

    private IEnumerator Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            GameObject newRocket = Instantiate(bullet_standart, shootPos.position, muzzleTransform.rotation);
            Rigidbody2D rocketRb = newRocket.GetComponent<Rigidbody2D>();
            rocketRb.velocity = muzzleTransform.right * armoSpeed;

            var bulletHandler = newRocket.GetComponent<BulletDamageHandler>();
            if (bulletHandler != null)
            {
                bulletHandler.Initialize(gameObject);
            }

            OnReloadStarted?.Invoke(reloading);
            yield return new WaitForSeconds(reloading);
            canShoot = true;
        }
    }

   
    void ParcticleSysteme()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!particleDownOne.isEmitting)
            {
                particleDownOne.Play();
                particleDownTwo.Play();
            }
        }
        else
        {
            if (!particleDownOne.isEmitting)
            {
                particleDownOne.Stop();
                particleDownTwo.Stop();
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (!particleUpOne.isEmitting)
            {
                particleUpOne.Play();
                particleUpTwo.Play();
            }
        }
        else
        {
            if (!particleUpOne.isEmitting)
            {
                particleUpOne.Stop();
                particleUpTwo.Stop();
            }
        }
     
    }

    // Boost System
    public void ApplySpeedBoost(float duration)
    {
        StopCoroutine("SpeedBoostCoroutine");
        StartCoroutine(SpeedBoostCoroutine(duration));
    }

    private IEnumerator SpeedBoostCoroutine(float duration)
    {
        speed *= 1.50f; 
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;
    }

    public void ApplyHealBoost()
    {
        float healAmount = maxHP * 0.25f; 
        currentHP += healAmount; 
        currentHP = Mathf.Clamp(currentHP, 0, maxHP); 
        UpdateHUD(); 
    }

    public void ApplySmallDamageBoost(float duration)
    {
        StopCoroutine("SmallDamageBoostCoroutine");
        StartCoroutine(SmallDamageBoostCoroutine(duration));
    }

    private IEnumerator SmallDamageBoostCoroutine(float duration)
    {
        damage *= 1.30f;
        yield return new WaitForSeconds(duration);
        damage = originalSmallDamage;
    }

    public void ApplyMediumDamageBoost(float duration) 
    {
        StopCoroutine("MediumDamageBoostCoroutine");
        StartCoroutine(MediumDamageBoostCoroutine(duration));
    }

    private IEnumerator MediumDamageBoostCoroutine(float duration)
    {
        damage *= 1.70f;
        yield return new WaitForSeconds(duration);
        damage = originalMediumDamage;
    }

    public void ApplyHighDamageBoost(float duration)
    {
        StopCoroutine("HighDamageBoostCoroutine");
        StartCoroutine(HighDamageBoostCoroutine(duration));
    }

    private IEnumerator HighDamageBoostCoroutine(float duration)
    {
        damage *= 3.00f;
        yield return new WaitForSeconds(duration);
        damage = originalHighDamage;
    }

    public void ApplySpeedArmoBoost(float duration)
    {
        StopCoroutine("SpeedArmoBoostCoroutine");
        StartCoroutine(SpeedArmoBoostCoroutine(duration));
    }

    private IEnumerator SpeedArmoBoostCoroutine(float duration)
    {
        armoSpeed *= 1.70f;
        reloading /= 1.80f;

        HudBar hudBar = FindObjectOfType<HudBar>();
        if (hudBar != null)
        {
            hudBar.SetReloadTime(reloading);
            hudBar.StartReload(); 
        }

        yield return new WaitForSeconds(duration);

        armoSpeed = originalSpeedArmo;
        reloading = originalReloading;

        if (hudBar != null)
        {
            hudBar.SetReloadTime(reloading); 
        }
    }

    public void ApplyAdditionalHPBoost(float duration)
    {
        StopCoroutine("AdditionalHPBoostCoroutine");
        StartCoroutine(AdditionalHPBoostCoroutine(duration));
    }

    private IEnumerator AdditionalHPBoostCoroutine(float duration)
    {
        maxHP += 50f;
        currentHP = maxHP; 
        UpdateHUD(); 

        yield return new WaitForSeconds(duration);

        maxHP = originalAdditionalHP; 

     
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHUD(); 
    }


    private bool freezeEffectActive = false;
    public bool IsFreezeEffectActive
    {
        get { return freezeEffectActive; }
    }

    public void ActivateFreezeEffect(float duration)
    {
        freezeEffectActive = true;
        StartCoroutine(FreezeEffectCoroutine(duration));
    }

    private IEnumerator FreezeEffectCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        freezeEffectActive = false;
    }




}
