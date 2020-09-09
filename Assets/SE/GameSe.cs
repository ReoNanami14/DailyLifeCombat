//using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/* 
public class PlaySE : MonoBehaviour
{
    void Start(){}

    void Update(){}
} 
*/

namespace SoundSystem
{
    public class GameSe :MonoBehaviour
    {
        public AudioSource audioSource;
        public List<AudioClip> audioClipList = new List<AudioClip>();

        public bool IsPaused { get; private set; }

        public void PlaySe(string audioClipName)
        {
            if (IsPaused) return;

            AudioClip audioClip = audioClipList.FirstOrDefault(clip => clip.name == audioClipName);
        }

    }
}
