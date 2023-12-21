using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Playables;
using UnityEngine.Animations;

public class MixAnimationSample : MonoBehaviour
{
    public AnimationClip clip1, clip2;
    private PlayableGraph graph;
    private AnimationMixerPlayable mixer;
    [Range(0f, 1f)]
    public float weight;
    private void Start()
    {
        graph = PlayableGraph.Create();
        graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

        mixer = AnimationMixerPlayable.Create(graph);
        mixer.SetInputCount(2);

        var clipPlayable1 = AnimationClipPlayable.Create(graph, clip1);
        var clipPlayable2 = AnimationClipPlayable.Create(graph, clip2);

        graph.Connect(clipPlayable1, 0, mixer, 0);
        graph.Connect(clipPlayable2, 0, mixer, 1);

        mixer.SetInputWeight(0, 0.5f);
        mixer.SetInputWeight(1, 0.5f);


        var output = AnimationPlayableOutput.Create(graph, "Anim", GetComponent<Animator>());
        output.SetSourcePlayable(mixer);

        graph.Play();
    }

    private void OnDisable()
    {
        graph.Destroy();
    }

    private void Update()
    {
        mixer.SetInputWeight(0, 1 - weight);
        mixer.SetInputWeight(1, weight);
    }

//#if UNITY_EDITOR
//    private void OnValidate()
//    {
//        graph.Stop();
//        graph = PlayableGraph.Create();

//        var clipPlayable1 = AnimationClipPlayable.Create(graph, clip1);
//        var clipPlayable2 = AnimationClipPlayable.Create(graph, clip2);

        
//        graph.Connect(clipPlayable1, 0, mixer, 0);
//        graph.Connect(clipPlayable2, 0, mixer, 1);

//        mixer.SetInputWeight(0, 0.5f);
//        mixer.SetInputWeight(1, 0.5f);


//        var output = AnimationPlayableOutput.Create(graph, "Anim", GetComponent<Animator>());
//        output.SetSourcePlayable(mixer);

//        graph.Play();
//    }
//#endif
}
