using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
namespace zAnimation
{
    public class RandomSelector : AnimBehaviour
    {
        public int currentIndex { get; private set; }
        public int clipCount { get; private set; } 
        private AnimationMixerPlayable m_mixer;
        private List<float> m_enterTimes;
        private List<float> m_clipLength;
        public RandomSelector(PlayableGraph graph):base(graph)
        {
            m_mixer = AnimationMixerPlayable.Create(graph);
            m_adapterPlayable.AddInput(m_mixer, 0, 1f);
            currentIndex = -1;
            m_enterTimes = new List<float>();
            m_clipLength = new List<float>();
        }
        public override void AddInput(Playable playable)
        {
            m_mixer.AddInput(playable,0);
            clipCount++;
        }
        public void AddInput(AnimationClip clip, float enterTime)
        {
            AddInput(new AnimUnit(m_adapterPlayable.GetGraph(), clip, enterTime));
            m_clipLength.Add(clip.length);
            m_enterTimes.Add(enterTime);
        }
        public override void Enable()
        {
            base.Enable();
            if (currentIndex < 0 || currentIndex >= clipCount) return;
            m_mixer.SetInputWeight(currentIndex, 1f);
            AnimHelper.Enable(m_mixer,currentIndex);
            m_adapterPlayable.SetTime(0f);
            m_adapterPlayable.Play();
            m_mixer.SetTime(0f);
            m_mixer.Play();
        }
        public override void Disable()
        {
            base.Disable();
            if (currentIndex < 0 || currentIndex >= clipCount) return;
            m_mixer.SetInputWeight(currentIndex, 0f);
            AnimHelper.Disable(m_mixer,currentIndex);
            m_adapterPlayable.Pause();
            m_mixer.Pause();
            currentIndex = -1;
        }
        public int Select()
        {
            currentIndex = UnityEngine.Random.Range(0, clipCount);
            return currentIndex;
        }
        public override float GetEnterTime()
        {
            if(currentIndex>=0f && currentIndex < m_enterTimes.Count)
            {
                return m_enterTimes[currentIndex];
            }
            return 0f;
        }
        public override float GetAnimLength()
        {
            if (currentIndex >= 0f && currentIndex < m_clipLength.Count)
            {
                return m_clipLength[currentIndex];
            }
            return 0f;
        }
    }
}
