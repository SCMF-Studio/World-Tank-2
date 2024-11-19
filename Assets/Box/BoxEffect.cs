using UnityEngine;

public class BoxEffect : MonoBehaviour
{
    public enum EffectType { SpeedBoost, HealBoost, Ricochet, SmallDamage, MediumDamage, HighDamage, SpeedArmo, AdditionalHP }
    public EffectType effectType;
    public float effectDuration = 5f;


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
                    if (tankTS != null)
                    {
                        tankTS.ApplySpeedBoost(effectDuration);
                    }
                    if (tankTH != null)
                    {
                        tankTH.ApplySpeedBoost(effectDuration); 
                    }
                    if (tankTA != null)
                    {
                        tankTA.ApplySpeedBoost(effectDuration);
                    }
                    if (tankTL != null)
                    {
                        tankTL.ApplySpeedBoost(effectDuration);
                    }
                    break;

                case EffectType.HealBoost:
                    if (tankTS != null)
                    {
                        tankTS.ApplyHealBoost();
                    }
                    if (tankTH != null)
                    {
                        tankTH.ApplyHealBoost();
                    }
                    if (tankTA != null)
                    {
                        tankTA.ApplyHealBoost();
                    }
                    if (tankTL != null)
                    {
                        tankTL.ApplyHealBoost();
                    }
                    break;

                case EffectType.SmallDamage:
                    if (tankTS != null)
                    {
                        tankTS.ApplySmallDamageBoost(effectDuration);
                    }
                    if (tankTH != null)
                    {
                        tankTH.ApplySmallDamageBoost(effectDuration);
                    }
                    if (tankTA != null)
                    {
                        tankTA.ApplySmallDamageBoost(effectDuration);
                    }
                    if (tankTL != null)
                    {
                        tankTL.ApplySmallDamageBoost(effectDuration);
                    }
                    break;

                case EffectType.MediumDamage:
                    if (tankTS != null)
                    {
                        tankTS.ApplyMediumDamageBoost(effectDuration);
                    }
                    if (tankTH != null)
                    {
                        tankTH.ApplyMediumDamageBoost(effectDuration);
                    }
                    if (tankTA != null)
                    {
                        tankTA.ApplyMediumDamageBoost(effectDuration);
                    }
                    if (tankTL != null)
                    {
                        tankTL.ApplyMediumDamageBoost(effectDuration);
                    }

                    break;

                case EffectType.HighDamage:
                    if (tankTS != null)
                    {
                        tankTS.ApplyHighDamageBoost(effectDuration);
                    }
                    if (tankTH != null)
                    {
                        tankTH.ApplyHighDamageBoost(effectDuration);
                    }
                    if (tankTA != null)
                    {
                        tankTA.ApplyHighDamageBoost(effectDuration);
                    }
                    if (tankTL != null)
                    {
                        tankTL.ApplyHighDamageBoost(effectDuration);
                    }
                    break;

                case EffectType.SpeedArmo:
                    if (tankTS != null)
                    {
                        tankTS.ApplySpeedArmoBoost(effectDuration);
                    }
                    if (tankTH != null)
                    {
                        tankTH.ApplySpeedArmoBoost(effectDuration);
                    }
                    if (tankTA != null)
                    {
                        tankTA.ApplySpeedArmoBoost(effectDuration);
                    }
                    if (tankTL != null)
                    {
                        tankTL.ApplySpeedArmoBoost(effectDuration);
                    }
                    break;

                case EffectType.AdditionalHP:
                    if (tankTS != null)
                    {
                        tankTS.ApplyAdditionalHPBoost(effectDuration);
                    }
                    if (tankTH != null)
                    {
                        tankTH.ApplyAdditionalHPBoost(effectDuration);
                    }
                    if (tankTA != null)
                    {
                        tankTA.ApplyAdditionalHPBoost(effectDuration);
                    }
                    if (tankTL != null)
                    {
                        tankTL.ApplyAdditionalHPBoost(effectDuration);
                    }

                    break;
            }
            Destroy(gameObject);
        }
    }
}
