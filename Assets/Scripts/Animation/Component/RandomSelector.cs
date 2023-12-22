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
        public RandomSelector(PlayableGraph graph):base(graph)
        {
            m_mixer = AnimationMixerPlayable.Create(graph);
            m_adapterPlayable.AddInput(m_mixer, 0, 1f);
            currentIndex = -1;
        }
        public override void AddInput(Playable playable)
        {
            m_mixer.AddInput(playable,0);
            clipCount++;
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
    }
}
