using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;


public class BoxEffect : MonoBehaviour
{
    public enum EffectType { SpeedBoost, HealBoost, Ricochet, SmallDamage, MediumDamage, HighDamage, SpeedArmo, AdditionalHP, Freez, Juggernaut }
    public EffectType effectType;
    public float effectDuration;
    private HudBar hudBar;


    private void Start()
    {
        hudBar = FindObjectOfType<HudBar>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TS001 tankTS = other.GetComponent<TS001>();
        TH001 tankTH = other.GetComponent<TH001>();
        TA001 tankTA = other.GetComponent<TA001>();
        TL001 tankTL = other.GetComponent<TL001>();

        if (tankTS != null || tankTH != null || tankTA != null || tankTL != null)
        {
            
            switch (effectType)
            {

                case EffectType.SpeedBoost:
                    ApplySpeedBoost(tankTS, tankTH, tankTA, tankTL);
                    hudBar.AddEffectIcon(effectType, effectDuration);
                    break;

                case EffectType.HealBoost:
                    ApplyHealBoost(tankTS, tankTH, tankTA, tankTL);
                    hudBar.AddEffectIcon(effectType, effectDuration);
                    break;

                case EffectType.SmallDamage:
                    ApplySmallDamageBoost(tankTS, tankTH, tankTA, tankTL);
                    hudBar.AddEffectIcon(effectType, effectDuration);
                    break;

                case EffectType.MediumDamage:
                    ApplyMediumDamageBoost(tankTS, tankTH, tankTA, tankTL);
                    hudBar.AddEffectIcon(effectType, effectDuration);
                    break;

                case EffectType.HighDamage:
                    ApplyHighDamageBoost(tankTS, tankTH, tankTA, tankTL);
                    hudBar.AddEffectIcon(effectType, effectDuration);
                    break;

                case EffectType.SpeedArmo:
                    ApplySpeedArmoBoost(tankTS, tankTH, tankTA, tankTL);
                    hudBar.AddEffectIcon(effectType, effectDuration);
                    break;

                case EffectType.AdditionalHP:
                    ApplyAdditionalHPBoost(tankTS, tankTH, tankTA, tankTL);
                    hudBar.AddEffectIcon(effectType, effectDuration);
                    break;

                case EffectType.Freez:
                    ApplyFreezeEffect(tankTS, tankTH, tankTA, tankTL);
                    hudBar.AddEffectIcon(effectType, effectDuration);
                    break;

               
            }
            hudBar.AddEffectIcon(effectType, effectDuration);
            Destroy(gameObject);
        }

    }

    private void ApplySpeedBoost(TS001 tankTS, TH001 tankTH, TA001 tankTA, TL001 tankTL)
    {
        if (tankTS != null) tankTS.ApplySpeedBoost(effectDuration);
        if (tankTH != null) tankTH.ApplySpeedBoost(effectDuration);
        if (tankTA != null) tankTA.ApplySpeedBoost(effectDuration);
        if (tankTL != null) tankTL.ApplySpeedBoost(effectDuration);
    }

    private void ApplyHealBoost(TS001 tankTS, TH001 tankTH, TA001 tankTA, TL001 tankTL)
    {
        if (tankTS != null) tankTS.ApplyHealBoost();
        if (tankTH != null) tankTH.ApplyHealBoost();
        if (tankTA != null) tankTA.ApplyHealBoost();
        if (tankTL != null) tankTL.ApplyHealBoost();
    }

    private void ApplySmallDamageBoost(TS001 tankTS, TH001 tankTH, TA001 tankTA, TL001 tankTL)
    {
        if (tankTS != null) tankTS.ApplySmallDamageBoost(effectDuration);
        if (tankTH != null) tankTH.ApplySmallDamageBoost(effectDuration);
        if (tankTA != null) tankTA.ApplySmallDamageBoost(effectDuration);
        if (tankTL != null) tankTL.ApplySmallDamageBoost(effectDuration);
    }

    private void ApplyMediumDamageBoost(TS001 tankTS, TH001 tankTH, TA001 tankTA, TL001 tankTL)
    {
        if (tankTS != null) tankTS.ApplyMediumDamageBoost(effectDuration);
        if (tankTH != null) tankTH.ApplyMediumDamageBoost(effectDuration);
        if (tankTA != null) tankTA.ApplyMediumDamageBoost(effectDuration);
        if (tankTL != null) tankTL.ApplyMediumDamageBoost(effectDuration);
    }

    private void ApplyHighDamageBoost(TS001 tankTS, TH001 tankTH, TA001 tankTA, TL001 tankTL)
    {
        if (tankTS != null) tankTS.ApplyHighDamageBoost(effectDuration);
        if (tankTH != null) tankTH.ApplyHighDamageBoost(effectDuration);
        if (tankTA != null) tankTA.ApplyHighDamageBoost(effectDuration);
        if (tankTL != null) tankTL.ApplyHighDamageBoost(effectDuration);
    }

    private void ApplySpeedArmoBoost(TS001 tankTS, TH001 tankTH, TA001 tankTA, TL001 tankTL)
    {
        if (tankTS != null) tankTS.ApplySpeedArmoBoost(effectDuration);
        if (tankTH != null) tankTH.ApplySpeedArmoBoost(effectDuration);
        if (tankTA != null) tankTA.ApplySpeedArmoBoost(effectDuration);
        if (tankTL != null) tankTL.ApplySpeedArmoBoost(effectDuration);
    }

    private void ApplyAdditionalHPBoost(TS001 tankTS, TH001 tankTH, TA001 tankTA, TL001 tankTL)
    {
        if (tankTS != null) tankTS.ApplyAdditionalHPBoost(effectDuration);
        if (tankTH != null) tankTH.ApplyAdditionalHPBoost(effectDuration);
        if (tankTA != null) tankTA.ApplyAdditionalHPBoost(effectDuration);
        if (tankTL != null) tankTL.ApplyAdditionalHPBoost(effectDuration);
    }

    private void ApplyFreezeEffect(TS001 tankTS, TH001 tankTH, TA001 tankTA, TL001 tankTL)
    {
        if (tankTS != null) tankTS.ActivateFreezeEffect(effectDuration);
        if (tankTH != null) tankTH.ActivateFreezeEffect(effectDuration);
        if (tankTA != null) tankTA.ActivateFreezeEffect(effectDuration);
        if (tankTL != null) tankTL.ActivateFreezeEffect(effectDuration);

        StartCoroutine(RemoveEffectAfterDuration(effectType));
    }

    private List<Image> activeIcons; 

    private IEnumerator RemoveEffectAfterDuration(BoxEffect.EffectType effectType)
    {
        yield return new WaitForSeconds(effectDuration);

        Image iconToRemove = activeIcons.FirstOrDefault(icon => icon.sprite != null && icon.sprite.name == effectType.ToString());

        if (iconToRemove != null)
        {
            hudBar.RemoveEffectIcon(iconToRemove);
        }
    }
}
