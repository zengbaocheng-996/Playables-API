using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using UnityEngine.Playables;
using UnityEngine.Animations;
namespace zAnimation
{
    public class AnimHelper
    {
        public static void Enable(Playable playable)
        {
            var adapter = GetAdapter(playable);
            if(adapter!=null)
            {
                adapter.Enable();
            }
        }

        public static void Enable(AnimationMixerPlayable mixer, int index)
        {
            Enable(mixer.GetInput(index));
        }

        public static void Disable(Playable playable)
        {
            var adapter = GetAdapter(playable);
            if (adapter != null)
            {
                adapter.Disable();
            }
        }
        public static void Disable(AnimationMixerPlayable mixer, int index)
        {
            Disable(mixer.GetInput(index));
        }
        public static AnimAdapter GetAdapter(Playable playable)
        {
            if(typeof(AnimAdapter).IsAssignableFrom(playable.GetPlayableType()))
            {
                return ((ScriptPlayable<AnimAdapter>)playable).GetBehaviour();
            }
            return null;
        }

        public static void SetOutput(PlayableGraph graph, Animator animator, AnimBehaviour behaviour)
        {
            var root = new Root(graph);
            root.AddInput(behaviour);
            AnimationPlayableOutput.Create(graph, "Anim", animator).SetSourcePlayable(behaviour.GetAnimAdapterPlayable());
        }

        public static void Start(PlayableGraph graph, AnimBehaviour behaviour)
        {
            graph.Play();
            behaviour.Enable();
        }

        public static void Start(PlayableGraph graph)
        {
            graph.Play();
            GetAdapter(graph.GetOutputByType<AnimationPlayableOutput>(0).GetSourcePlayable()).Enable();
        }
        public static ComputeShader GetCompute(string name)
        {
            return UnityEngine.Object.Instantiate(Resources.Load<ComputeShader>("Compute/" + name));
        }
    }
}
