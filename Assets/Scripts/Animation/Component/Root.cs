﻿                                                                            using System.Collections;
using UnityEngine;

using UnityEngine.Playables;
namespace zAnimation
{
    public class Root : AnimBehaviour
    {
        public Root(PlayableGraph graph):base(graph)
        {

        }

        public override void AddInput(Playable playable)
        {
            m_adapterPlayable.AddInput(playable, 0, 1f);
        }

        public override void Enable()
        {
            base.Enable();
            for(int i = 0; i< m_adapterPlayable.GetInputCount(); i++)
            {
                AnimHelper.Enable(m_adapterPlayable.GetInput(i));
            }
            m_adapterPlayable.SetTime(0f);
            m_adapterPlayable.Play();
        }
        public override void Disable()
        {
            base.Disable();
            for(int i =0; i<m_adapterPlayable.GetInputCount();i++)
            {
                AnimHelper.Disable(m_adapterPlayable.GetInput(i));
            }
            m_adapterPlayable.Pause();
        }
    }
}