using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject player;

    [SerializeField]
    Transform playerPos;

    Animator anim;

    
    public bool canDodge;
    public bool lockon;

    public GameObject target;
    public Vector3 targetPoint;
    public Quaternion targetRotation;

    

    [SerializeField]
    float dodgeCooldown;
    float dodgeCooldownDefaultValue = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        anim = player.GetComponent<Animator>();
        canDodge = true;
        dodgeCooldown = dodgeCooldownDefaultValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(lockon)
        {
            targetPoint = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position;
            targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
        }

        anim.SetFloat("horizontal", (Input.GetAxis("Horizontal")));
        anim.SetFloat("vertical", (Input.GetAxis("Vertical")));
        dodge();
        Debug.Log(anim.GetFloat("horizontal"));

    }

    void dodge()
    {
        if (Input.GetButtonDown("Jump") && canDodge)
        {
            anim.SetTrigger("dodge");
            canDodge = false;
        }
        if(!canDodge)
        {
            dodgeCooldown -= Time.deltaTime;
            if (dodgeCooldown <= 0)
            {
                dodgeCooldown = dodgeCooldownDefaultValue;
                canDodge = true;
            }
        }
            
    }
}
