using UnityEngine;

public class BoxEffect : MonoBehaviour
{
    public enum EffectType { SpeedBoost, HealBoost, Ricochet }
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

                case EffectType.Ricochet:
                    if (tankTS != null)
                    {
                        tankTS.ActivateRicochetBullet(effectDuration);
                    }
                    break;

            }
            Destroy(gameObject);
        }
    }
}
