using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Åö×²Æ÷ÉèÖÃ
public class E : MonoBehaviour
{
    public Collider2D[] mountainColliders; //É½ÂöÅö×²Ìå
    public Collider2D[] boundaryColliders; //±ß½çÅö×²Ìå

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            foreach(Collider2D mountain in mountainColliders )
            {
                mountain.enabled = false;
            }
            foreach (Collider2D boundary in boundaryColliders)
            {
                boundary.enabled = true;
            }

            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;

        }
    }
}
