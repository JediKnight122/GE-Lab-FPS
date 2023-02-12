

namespace AI.StateMachine
{
    public abstract class AIState
    {
        public abstract void Execute();

        protected EnemyMovement movement;
        protected EnemyAnimationManager animationManager;
    }
}
