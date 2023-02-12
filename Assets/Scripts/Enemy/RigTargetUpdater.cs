using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigTargetUpdater : MonoBehaviour
{
    public GameObject ikTarget;

    void Start()
    {
        MultiAimConstraint[] components = GetComponentsInChildren<MultiAimConstraint>();
        var data = components[0].data.sourceObjects;
        data.Clear();
        data.Add(new WeightedTransform(FindObjectOfType<CharacterPlayer>().enemyTargetPoint, 1));

        foreach (var var in components)
        {
            var.data.sourceObjects = data;
        }

        transform.root.transform.GetComponentInChildren<RigBuilder>().Build();
    }
}
