using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Playables;

namespace zAnimation
{
    public class AnimBehaviour
    {
        public bool enable { get; protected set; }
        public float remainTime { get; protected set; }
        protected float m_enterTime;
        protected float m_animLength;
        protected Playable m_adapterPlayable;

        public AnimBehaviour() { }
        public AnimBehaviour(PlayableGraph graph, float enterTime = 0f)
        {
            m_adapterPlayable = ScriptPlayable<AnimAdapter>.Create(graph);
            ((ScriptPlayable<AnimAdapter>)m_adapterPlayable).GetBehaviour().Init(this);
            m_enterTime = enterTime;
            m_animLength = float.NaN;
        }
        public virtual void Enable()
        {
            enable = true;
            remainTime = GetAnimLength();
        }
        public virtual void Disable()
        {
            enable = false;
        }
        public virtual void Execute(Playable playable, FrameData info)
        {
            if (!enable) return;
            remainTime = remainTime > 0 ? remainTime - info.deltaTime : 0f;
        }
        public virtual void Stop()
        {
        }
        public Playable GetAnimAdapterPlayable()
        {
            return m_adapterPlayable;
        }

        public virtual void AddInput(Playable playable)
        {

        }

        public void AddInput(AnimBehaviour behaviour)
        {
            AddInput(behaviour.GetAnimAdapterPlayable());
        }

        public virtual float GetEnterTime()
        {
            return m_enterTime;
        }

        public virtual float GetAnimLength()
        {
            return m_animLength;
        }
    }
}

