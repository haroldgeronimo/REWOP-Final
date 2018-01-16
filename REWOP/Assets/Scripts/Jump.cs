using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Button jumpbutton;
    private float distToGround;
    public CapsuleCollider col;
    public Rigidbody rb;
    public GameObject player;
    public Animator agent;
    public float jumpStrength;
    // Use this for initialization
    void Start () {
        Button jumpbutton = GetComponent<Button>();
        
        jumpbutton.onClick.AddListener(jmpAct);
        
    }

    public void jmpAct()
    {
        Debug.Log("WILL JUMP");
        if (IsGrounded())
        { 
            Debug.Log("JUMP!");
            rb.AddForce(new Vector3(0, jumpStrength, 0),ForceMode.Impulse);

            agent.Play("Jump");

        }
    }

    private bool IsGrounded(){
        return Physics.Raycast(player.transform.position, -Vector3.up, distToGround + 0.2f);
 }
  
}
