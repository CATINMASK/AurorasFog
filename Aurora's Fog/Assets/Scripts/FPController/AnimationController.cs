using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    private Animator animator;
    //public float idle;
    public float walkingV;
    public float walkingH;
    //public bool vertical;
    public bool horizontal;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.localPosition = new Vector3(0f, 0f, 0f);      // Выравнивает координаты аниматора в соответствии с контроллером (убирает баг).
        animator.SetBool("walkH", horizontal);
        walkingV = Input.GetAxis("Vertical");
        animator.SetFloat("walkingV", walkingV);

        walkingH = Input.GetAxis("Horizontal");
        animator.SetFloat("walkingH", walkingH);

        if (walkingH != 0)
            horizontal = true;
        else
            horizontal = false;
    }
}
