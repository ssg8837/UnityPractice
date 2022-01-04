using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    int basicCost = 50;
    [SerializeField] float buildDelay = 1f;

    private void Start() 
    {
        StartCoroutine(Build());
    }
   public bool CreateTower(Tower tower, Vector3 position, int tyleType)
   {
       int cost = basicCost + tyleType * 15;
        Bank bank = FindObjectOfType<Bank>();

        

        if(bank == null)
        {
            return false;
        }
        if(bank.CurrentBalance >= cost)
        {
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.WithDraw(cost);

            return true;
        }
        return false;
   }

   IEnumerator Build()
   {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);   
                
                foreach (Transform grandChild in child)
                {
                    grandChild.gameObject.SetActive(false);   
                }       
            }
       
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);   
                yield return new WaitForSeconds(buildDelay);  
                foreach (Transform grandChild in child)
                {
                    grandChild.gameObject.SetActive(true);   
                }       
            }   
   }
}
