using System.Diagnostics;
using Unity.Entities;

public class CharacterShootSystem : ComponentSystem
{
    private EntityQuery _characterShootQuery;


    protected override void OnCreate()
    {
        _characterShootQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<ShootData>(),
            ComponentType.ReadOnly<UserInputData>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_characterShootQuery).ForEach(
            (Entity entity, UserInputData shootActionData, ref InputData inputData, ref HealthData healthData) =>
            {
                if (!healthData.IsDie)
                    if (inputData.Shoot>0f && shootActionData.shootAction!=null && shootActionData.shootAction is IAbility ability)
                    {
                        ability.Execute();
                    }
            });
    }
}
