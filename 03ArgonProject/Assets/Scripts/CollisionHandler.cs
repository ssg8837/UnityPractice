using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField]float loadDelay = 1f;
    [SerializeField] ParticleSystem explosionParticle;

    // private void OnCollisionEnter(Collision other) 
    // {
    //     Debug.Log(other.gameObject.name);
    // }

    private void OnTriggerEnter(Collider other) 
    {
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        // 우주선 렌더링 안함
        GetComponent<MeshRenderer>().enabled = false;
        explosionParticle.Play();
        // 컨트롤 비활성화
        GetComponent<PlayerControls>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        // 1초후 Invoke 판단하여 실행
        Invoke("ReloadLevel", loadDelay);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
