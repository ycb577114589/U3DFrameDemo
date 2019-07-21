

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("Returns a TaskStatus of running. Will only stop when interrupted or a conditional abort is triggered.")]
    [TaskIcon("{SkinColor}IdleIcon.png")]
    public class M_Idle : Action
    {
        public override TaskStatus OnUpdate()
        {
            
            return TaskStatus.Running;
        }
    }
}
