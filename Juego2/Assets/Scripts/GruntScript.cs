using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public Transform John;
    public GameObject BulletPrefab;
    private Rigidbody2D rb;
    
    //funciona el cambio de su valor de colision
    private bool colision = false;
    //private bool emparejada = true;
    //private float der;
    //private bool encasa = false;
    

    void Update()
    {
        transform.localScale = new Vector3(0.3f, 0.25f,1.0f);
        if (John == null) return;

        Vector3 direction = John.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(0.3f, 0.25f,1.0f);
        else transform.localScale = new Vector3(-0.3f, 0.25f,1.0f);

        float distance = Mathf.Abs(John.position.x - transform.position.x);
        float distancey = Mathf.Abs(John.position.y - transform.position.y);
        
        if (distance < 0.3f && distancey < 0.3f)
        {
          //  posxjohn = John.position.x
            if(colision == false)  Shoot();

            if(John.localScale.x == 1.0f) transform.position = new Vector3(John.position.x-0.1f, John.position.y+0.1f, John.position.z);
            else
            {
                transform.position = new Vector3(John.position.x+0.1f, John.position.y+0.1f, John.position.z);
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            
            colision = true;
            
        }

        if (colision == true) {
            if (John.localScale.x < 0) transform.position = new Vector3(John.position.x+0.2f, John.position.y, John.position.z-0.1f);
            else transform.position = new Vector3(John.position.x-0.2f, John.position.y, John.position.z-0.1f);
            transform.localScale = John.localScale;
        }
       
        
        
        
        //aqui juntase e ir juntos
    }

    
    private void Shoot()
    {
        GameObject bullet = Instantiate(BulletPrefab, John.position , Quaternion.identity);
        
        
    }
    

    
}
