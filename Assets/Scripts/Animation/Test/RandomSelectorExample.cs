using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

namespace zAnimation
{
    public class RandomSelectorExample : MonoBehaviour
    {
        public AnimationClip[] clips;
        private PlayableGraph graph;

        void Start()
        {
            graph = PlayableGraph.Create();
            var randomSelector = new RandomSelector(graph);
            foreach(var clip in clips)
            {
                randomSelector.AddInput(AnimationClipPlayable.Create(graph, clip));
            }
            AnimHelper.SetOutput(graph, GetComponent<Animator>(), randomSelector);
            var index = randomSelector.Select();
            Debug.Log(index);
            AnimHelper.Start(graph);
        }

        private void OnDisable()
        {
            graph.Destroy();
        }
    }
}