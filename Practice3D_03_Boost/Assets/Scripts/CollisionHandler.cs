using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

  [SerializeField] float levelLoadDealy = 2f;
  [SerializeField] AudioClip audioBoom;
  [SerializeField] AudioClip audioSuccess;
  [SerializeField] ParticleSystem sucessParticle;
  [SerializeField] ParticleSystem explosionParticle;
   
  private AudioSource oneShotAudioSource;

  bool isTransitioning = false;
  bool collisionCheck = true;

  private void Start()
    {
        oneShotAudioSource = GetComponent<AudioSource>();
    }

    private void Update() 
    {
      GetSpecialKey();
    }

    private void GetSpecialKey()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionCheck = !collisionCheck;
        }
    }

    void OnCollisionEnter(Collision other) 
  {
    if(isTransitioning || !collisionCheck)
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
    explosionParticle.Play();
    GetComponent<Movements>().enabled = false;
    Invoke("ReloadLevel", levelLoadDealy);
  }

  void StartNextSequence()
  {
    isTransitioning = true;
    oneShotAudioSource.Stop();
    oneShotAudioSource.PlayOneShot(audioSuccess);
    sucessParticle.Play();
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
