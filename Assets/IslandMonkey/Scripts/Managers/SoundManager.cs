using System;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace Assets.IslandMonkey.Scripts.Managers
{
    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    [Serializable]
    public class Channel
    {
        public AudioSource audioSource;
        [Range(0.0f,1.0f)]
        public float volume;

        public Channel(AudioSource audioSource, float volume)
        {
            this.audioSource = audioSource;
            this.volume = volume;
        }

        public virtual void Play(AudioClip clip)
        {
            audioSource.volume = volume;
            audioSource.PlayOneShot(clip);
        }

        public virtual void Pause()
        {
            audioSource.Pause();
        }

        public virtual void Stop()
        {
            audioSource.Stop();
        }

        public virtual void SetVolume(float volume)
        {
            this.volume=volume;
        }
    }

    [Serializable]
    public class BGMChannel : Channel
    {
        public BGMChannel(AudioSource audioSource, float volume) : base(audioSource, volume)
        {
         
        }

        public virtual void Play(AudioClip clip)
        {
            audioSource.clip=clip;
            audioSource.volume = volume;
            audioSource.Play();
        }
    }

    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField]
        private BGMChannel _bgmChanel = new BGMChannel(null, 1.0f);

        [SerializeField]
        private List<Channel> _channels = new List<Channel>();

        [SerializeField]
        private List<Sound> _sounds = new List<Sound>();

        public BGMChannel GetBGMChanel() => _bgmChanel;

        public Channel GetChanel(int index) => _channels[index];

        public Sound GetSound(int index) => _sounds[index];

        public Sound GetSound(string name) => _sounds.Find(x => x.name == name);

        public void PlaySound(Sound sound, Channel channel) => channel.Play(sound.clip);

        public void Pause(Channel channel) => channel.Pause();

        public void Stop(Channel channel) => channel.Stop();

        public void SetVolume(Channel channel, float volume) => channel.SetVolume(volume);
    }


}