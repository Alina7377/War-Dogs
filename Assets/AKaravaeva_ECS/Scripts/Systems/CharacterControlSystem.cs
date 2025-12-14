using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CharacterControlSystem : ComponentSystem
{
    private EntityQuery _characterControlQuery;


    protected override void OnCreate()
    {
        _characterControlQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<ControlData>(),
            ComponentType.ReadOnly<CharacterController>(),
            ComponentType.ReadOnly<JerkData>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_characterControlQuery).ForEach(
            (Entity entity, CharacterController characterController, ref InputData inputData, ref ControlData controlData, ref JerkData jerkData, ref HealthData healthData) =>
            {
                // Если выполняется Рывок, другие перемещения и смена направления невозможны
                if (!jerkData.IsJerk && !healthData.IsDie)
                {
                    //Перемещение
                    Vector3 pos = (new Vector3(inputData.Move.x, 0, inputData.Move.y) * controlData.Speed * Time.DeltaTime);
                    characterController.Move(pos);

                    // Поворот
                    Vector3 direction = new Vector3(inputData.Move.x, 0, inputData.Move.y);
                    if (direction == Vector3.zero) return;
                    Quaternion currentRotation = characterController.transform.rotation;
                    Quaternion newRotation = Quaternion.LookRotation(Vector3.Normalize(direction));
                    if (currentRotation == newRotation) return;

                    characterController.transform.rotation = Quaternion.Lerp(currentRotation, newRotation, Time.DeltaTime * controlData.Speed);
                }
            });
    }
}
