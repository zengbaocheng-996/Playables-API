using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;
namespace zAnimation
{
    public class MixerExample : MonoBehaviour
    {
        public AnimationClip[] clips;
        private PlayableGraph graph;
        private Mixer mixer;
        public bool isTransition;
        public int current;
        void Start()
        {
            graph = PlayableGraph.Create();

            var anim1 = new AnimUnit(graph, clips[0], 0.5f);
            var anim2 = new AnimUnit(graph, clips[1], 0.5f);
            var anim3 = new AnimUnit(graph, clips[2], 0.5f);

            mixer = new Mixer(graph);
            mixer.AddInput(anim1);
            mixer.AddInput(anim2);
            mixer.AddInput(anim3);

            AnimHelper.SetOutput(graph, GetComponent<Animator>(), mixer);
            AnimHelper.Start(graph);
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                mixer.TransitionTo(0);
            }
            else if(Input.GetKeyDown(KeyCode.C))
            {
                mixer.TransitionTo(1);
            }
            else if(Input.GetKeyDown(KeyCode.V))
            {
                mixer.TransitionTo(2);
            }
            isTransition = mixer.isTransition;
            current = mixer.currentIndex;
        }
        private void OnDisable()
        {
            graph.Destroy();
        }
    }
}