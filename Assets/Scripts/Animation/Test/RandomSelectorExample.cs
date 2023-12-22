using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

namespace zAnimation
{
    public class RandomSelectorExample : MonoBehaviour
    {
        public bool isTransition;
        public float remainTime;

        public AnimationClip[] clips;
        private PlayableGraph graph;
        private Mixer mixer;
        private RandomSelector random;
        void Start()
        {
            graph = PlayableGraph.Create();
            var idle = new AnimUnit(graph, clips[0], 0.5f);
            random = new RandomSelector(graph);
            for(int i=1; i<clips.Length;i++)
            {
                random.AddInput(clips[i], 0.5f);
            }
            mixer = new Mixer(graph);
            mixer.AddInput(idle);
            mixer.AddInput(random);
            
            
            AnimHelper.SetOutput(graph, GetComponent<Animator>(), mixer);

            AnimHelper.Start(graph);
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                random.Select();
                mixer.TransitionTo(1);
            }
            isTransition = mixer.isTransition;
            remainTime = random.remainTime;
            if(!mixer.isTransition&&random.remainTime<=0.5f && mixer.currentIndex!=0)
            {
                mixer.TransitionTo(0);
            }
        }

        private void OnDisable()
        {
            graph.Destroy();
        }
    }
}