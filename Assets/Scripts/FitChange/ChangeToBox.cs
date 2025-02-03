using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToBox : MonoBehaviour
{
    public GameObject box;
    public GameObject playerAnim;
    public Player player;
    public GameObject boxInstianted;
    private Rigidbody2D rb;
    public bool isBox = false;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            isBox = true;
            
            boxInstianted=Instantiate(box, transform.position, Quaternion.identity);
            player.enabled=false;
            playerAnim.SetActive(false);
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            rb.isKinematic = true;
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(boxInstianted!=null)  {
                Destroy(boxInstianted);
            } 
            
            player.enabled=true;
            isBox = false;
            playerAnim.SetActive(true);
            rb.gravityScale = 1;
            rb.isKinematic = false;
        }
    }
}
