using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

  [SerializeField] float levelLoadDealy = 2f;

  void OnCollisionEnter(Collision other) 
  {
      switch (other.gameObject.tag)
      {
          case "Friendly":
            Debug.Log("Friendly");
            break;
          case "Finish":
            Debug.Log("Finish");
            StartNextSequence();
            break;
          default:
            Debug.Log("Damage!");
            StartCrashSequence();//ReloadLevel();
            break;
      }      
  }

  void StartCrashSequence()
  {
    //todo add SFX when Crash
    //todo add Particle effect when Crash
    GetComponent<Movements>().enabled = false;
    Invoke("ReloadLevel", levelLoadDealy);
  }

  void StartNextSequence()
  {
    GetComponent<Movements>().enabled = false;
    Invoke("LoadNextLevel", levelLoadDealy);
  }
  void ReloadLevel()
  {
    int currentScene = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentScene);
  }
  void LoadNextLevel()
  {
    int currentScene = SceneManager.GetActiveScene().buildIndex;
    if(SceneManager.sceneCount == currentScene+1)
    {
      SceneManager.LoadScene(currentScene+1);
    }
    else
    {
      SceneManager.LoadScene(0);
    }
  }
}
