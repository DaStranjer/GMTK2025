using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] public PlayerController playerController;
    Animator animator;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {

        animator.SetBool("isAttack", playerController.playerAttack);
    }
}
