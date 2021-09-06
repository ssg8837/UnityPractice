using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

  [SerializeField] float levelLoadDealy = 2f;
  [SerializeField] AudioClip audioBoom;
  [SerializeField] AudioClip audioSuccess;
   
  private AudioSource oneShotAudioSource;

  bool isTransitioning = false;

  private void Start() 
  {
    oneShotAudioSource = GetComponent<AudioSource>();
  }

  void OnCollisionEnter(Collision other) 
  {
    if(isTransitioning)
    {
      return ;
    }
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
    isTransitioning = true;
    oneShotAudioSource.Stop();
    oneShotAudioSource.PlayOneShot(audioBoom);
    //todo add Particle effect when Crash
    GetComponent<Movements>().enabled = false;
    Invoke("ReloadLevel", levelLoadDealy);
  }

  void StartNextSequence()
  {
    isTransitioning = true;
    oneShotAudioSource.Stop();
    oneShotAudioSource.PlayOneShot(audioSuccess);
    //todo add Particle effect when NextLevel
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
