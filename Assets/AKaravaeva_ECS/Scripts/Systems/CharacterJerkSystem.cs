using Unity.Entities;

public class CharacterJerkSystem : ComponentSystem
{
    private EntityQuery _characterJerkQuery;
    
    protected override void OnCreate()
    {
        _characterJerkQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<JerkData>(),
            ComponentType.ReadOnly<UserInputData>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_characterJerkQuery).ForEach(
            (Entity entity, UserInputData jerkActionData, ref InputData inputData, ref JerkData jerkData, ref HealthData healthData) =>
            {
                if (!healthData.IsDie)
                    if ((inputData.Jerk > 0f && jerkActionData.jerkAction != null || jerkData.IsJerk) && jerkActionData.jerkAction is IAbilityRetBool abilityRet)
                    {
                        jerkData.IsJerk = abilityRet.Execute();
                    }
            });
    }
}
