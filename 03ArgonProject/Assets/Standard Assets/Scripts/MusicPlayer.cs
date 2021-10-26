using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
   private void Awake() 
   {
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;  
        if(numMusicPlayers > 1)
        {
            //추가 생성 방지
            Destroy(gameObject);
        }
        else
        {
            //최초생성
            DontDestroyOnLoad(gameObject);
        }
   }
}
