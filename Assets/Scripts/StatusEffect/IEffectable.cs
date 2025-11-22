public interface IEffectable
{
    public void ApplyEffect(StatusEffectData effect);
    public void RemoveEffect();
    public void HandleEffect();
}