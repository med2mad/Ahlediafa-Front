using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class player : MonoBehaviour
{
    Animator anim;
    bool hit;
    public CinemachineVirtualCamera virtualCamera;
    public float scrollSensitivity = 30f;
    public AudioSource swingSound;
    public Collider swordCollider;

    void Start(){
        anim = GetComponent<Animator>();
        hit = false;
    }

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
          if (scrollInput != 0f){
            float currentFOV = virtualCamera.m_Lens.FieldOfView;
            float newFOV = currentFOV - scrollInput * scrollSensitivity;
            virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(newFOV, 40f, 80f);
        }
        if(Input.GetKeyDown("i")){
            float currentFOV = virtualCamera.m_Lens.FieldOfView;
            if(currentFOV>40f){virtualCamera.m_Lens.FieldOfView = 40f;}
            else{virtualCamera.m_Lens.FieldOfView = 80f;}
        }

        if((Input.GetKeyDown("j") || Input.GetMouseButtonDown(0)) && !anim.GetCurrentAnimatorStateInfo(0).IsName("attack2")){
            hit = true;
        }

        if(hit && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle Walk Run Blend")){
            hit = false;
            anim.Play("attack1");
        }
        if(hit && anim.GetCurrentAnimatorStateInfo(0).normalizedTime>0.75f && anim.GetCurrentAnimatorStateInfo(0).IsName("attack1")){
            hit = false;
            anim.Play("attack2");
        }
    }

    public void enableSwordColider(){
        swordCollider.enabled = true;
        swingSound.Play();
    }
    public void disableSwordColider(){
        swordCollider.enabled = false;
    }
}
