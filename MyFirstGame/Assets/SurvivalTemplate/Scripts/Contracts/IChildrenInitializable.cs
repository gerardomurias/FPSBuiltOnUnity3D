using UnityEngine;

public interface IChildrenInitializable
{
    void CheckNullTransform(Transform objectTransform);

    void InitializeChildrenReferences();

    void ActivateChildrenObjects();
}