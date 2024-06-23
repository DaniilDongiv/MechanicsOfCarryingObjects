using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;

        private readonly EcsFilter<ModelComponent, MovableComponent, DirectionComponent> _movableFilter = null;
        private static readonly int Walk = Animator.StringToHash("Walk");
        
        public void Run()
        {
            foreach (var i in _movableFilter)
            {
                ref var modelComponent = ref _movableFilter.Get1(i);
                ref var movableComponent = ref _movableFilter.Get2(i);
                ref var directionComponent = ref _movableFilter.Get3(i);

                ref var direction = ref directionComponent.Input;
                ref var transform = ref modelComponent.modelTransform;

                ref var rigidbody = ref movableComponent.Rigidbody;
                ref var speed = ref movableComponent.Speed;
                ref var animator = ref movableComponent.AnimatorController;

                rigidbody.velocity = new Vector3(direction.y * speed, rigidbody.velocity.y,direction.x * speed);
            
                if (direction.y != 0 || direction.x != 0)
                {
                    transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
                    MoveAnimation(true, animator);
                }
                else
                {
                    MoveAnimation(false, animator);
                }
            }
        }
        
        public void MoveAnimation(bool isMove, Animator animator)
        {
            animator.SetBool(Walk, isMove);
        }
    }
}