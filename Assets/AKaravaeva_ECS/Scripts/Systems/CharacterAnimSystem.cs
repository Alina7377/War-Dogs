using System.Text.RegularExpressions;
using Unity.Entities;
using UnityEngine;

public class CharacterAnimSystem : ComponentSystem
{
    private EntityQuery _characterAnimQuery;
    
    protected override void OnCreate()
    {
        _characterAnimQuery = GetEntityQuery(ComponentType.ReadOnly<AnimateData>(),
            ComponentType.ReadOnly<Animator>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_characterAnimQuery).ForEach(
            (Entity entity, Animator animator, ref InputData inputData, ref JerkData jerkData, ref ShootData shootData, ref HealthData healthData) =>
            {
                if (!healthData.IsDie)
                {
                    if (jerkData.IsJerk)
                    {
                        animator.SetFloat("Speed", 2);
                    }
                    else
                    {
                        float moveX = Mathf.Abs(inputData.Move.x);
                        float moveY = Mathf.Abs(inputData.Move.y);
                        if (moveX == moveY && moveX > 0.7f) moveX = 1f;
                        animator.SetFloat("Speed", Mathf.Max(moveX, moveY));

                        if (shootData.IsShoot)
                        {
                            animator.SetTrigger("IsShoot");
                            shootData.IsShoot = false;
                        }

                    }
                }
                else
                    if (!animator.GetBool("IsDie")) 
                {
                    animator.SetBool("IsDie", true);
                }


            });
    }
}
