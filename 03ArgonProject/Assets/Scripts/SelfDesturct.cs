using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDesturct : MonoBehaviour
{
    [SerializeField] float timeTillDestroy = 3f;

    // Update is called once per frame
    void Start()
    {
        //호출대상을 지정시간 이후 파괴
        Destroy(gameObject, timeTillDestroy);
    }
}
