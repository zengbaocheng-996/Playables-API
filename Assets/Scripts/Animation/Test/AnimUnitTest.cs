using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Animations;
using UnityEngine.Playables;
namespace zAnimation
{
    public class AnimUnitTest : MonoBehaviour
    {
        public AnimationClip clip;
        private AnimUnit anim;
        private PlayableGraph graph;
        private void Start()
        {
            graph = PlayableGraph.Create();
            anim = new AnimUnit(graph, clip);

            //var root = new Root(graph);
            //root.AddInput(anim);

            //var output = AnimationPlayableOutput.Create(graph, "Anim", GetComponent<Animator>());
            //output.SetSourcePlayable(root.GetAnimAdapterPlayable());
            //AnimHelper.SetOutput(graph, GetComponent<Animator>(), root);
            AnimHelper.SetOutput(graph, GetComponent<Animator>(), anim);

            //graph.Play();
            //root.Enable();

            //AnimHelper.Start(graph, root);
            AnimHelper.Start(graph);
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (anim.enable)
                    anim.Disable();
                else
                    anim.Enable();
            }
        }
        private void OnDisable()
        {
            graph.Destroy();
        }
    }
}

