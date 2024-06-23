using Leopotam.Ecs;

namespace Ecs
{
    sealed class PlayerInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, DirectionComponent> _directionFilter = null;

        public void Run()
        {
            
            foreach (var i in _directionFilter)
            {
                ref var directionComponent = ref _directionFilter.Get2(i);
                ref var direction = ref directionComponent.Input;

                direction.x = directionComponent.Joystick.Vertical;
                direction.y = directionComponent.Joystick.Horizontal;
            }
        }
    }
}