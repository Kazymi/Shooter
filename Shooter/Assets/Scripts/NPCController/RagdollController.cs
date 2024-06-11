using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RagdollController : MonoBehaviour
{
    private Rigidbody[] ragDollBodies;
    private Animator animator;

    private void OnEnable()
    {
        if (ragDollBodies.Length != 0)
        {
            SetRagDoll(false);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        ragDollBodies = GetComponentsInChildren<Rigidbody>();
        SetRagDoll(false);
    }

    public void SetRagDoll(bool isActive)
    {
        animator.enabled = !isActive;
        foreach (var ragDollBody in ragDollBodies)
        {
            ragDollBody.isKinematic = !isActive;
        }
    }
}