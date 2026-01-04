
using Unity.Entities;

public class TempBuffSystem : ComponentSystem
{
    private EntityQuery _tempBuffQuery;

    protected override void OnCreate()
    {
        _tempBuffQuery = GetEntityQuery(ComponentType.ReadOnly<CharacterData>());
    }

    protected override void OnUpdate()
    {
        var dstManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        Entities.With(_tempBuffQuery).ForEach(
            (Entity entity, CharacterData characterData) =>
            {
                if (characterData.CountBuff() > 0)
                {
                    characterData.CheackTimeBuff();
                }
            }
            );
    }
}
