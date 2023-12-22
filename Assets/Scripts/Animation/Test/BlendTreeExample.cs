using UnityEngine;
using UnityEngine.Playables;

namespace zAnimation
{
    public class BlendTreeExample : MonoBehaviour
    {
        public Vector2 pointer;
        public BlendClip2D[] clips;
        private PlayableGraph graph;
        private BlendTree2D blend;

        private void Start()
        {
            graph = PlayableGraph.Create();
            blend = new BlendTree2D(graph, clips);

            AnimHelper.SetOutput(graph, GetComponent<Animator>(), blend);
            AnimHelper.Start(graph);
        }
        private void Update()
        {
            blend.SetPointer(pointer);
        }

        private void OnDisable()
        {
            graph.Destroy();
        }
    }
}
