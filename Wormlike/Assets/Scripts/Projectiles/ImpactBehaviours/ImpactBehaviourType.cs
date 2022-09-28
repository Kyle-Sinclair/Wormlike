namespace Projectiles.ImpactBehaviours
{
    public enum ImpactBehaviourType
    {
        ExplodeOnImpact
    }
    
    public static class ImpactBehaviourTypeMethods
    {

        public static ImpactBehaviour GetInstance(ImpactBehaviourType type) {
            switch (type) {
                case ImpactBehaviourType.ExplodeOnImpact:
                    return ImpactBehaviourPool<ExplodeOnImpactBehaviour>.Get();
            }
            UnityEngine.Debug.Log("Forgot to support " + type);
            return null;
        }
    }
}