
using Unity.Entities;
using UnityEngine;

public interface ITempBuff
{
    public void Calculate(Entity entyty, EntityManager entityManager);

    public void CancelAction();
}
