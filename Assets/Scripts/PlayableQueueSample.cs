using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

public class PlayableQueue : PlayableBehaviour
{
    private AnimationMixerPlayable mixer;
    private float timeToNext;
    private int currentClip;

    public void Init(PlayableGraph graph, Playable owner, AnimationClip[] clips)
    {
        owner.SetInputCount(1);
        mixer = AnimationMixerPlayable.Create(graph);
        for(int i=0; i<clips.Length;i++)
        {
            mixer.AddInput(AnimationClipPlayable.Create(graph, clips[i]), 0);
        }
        mixer.SetInputWeight(0, 1f);
        graph.Connect(mixer, 0, owner, 0);

        timeToNext = clips[0].length;
    }
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        base.PrepareFrame(playable, info);
        timeToNext -= info.deltaTime;
        if (timeToNext<=0f && currentClip < mixer.GetInputCount() - 1)
        {
            Debug.Log(currentClip);

            mixer.SetInputWeight(currentClip, 0f);
            mixer.SetInputWeight(currentClip + 1, 1f);
            currentClip++;
            mixer.GetInput(currentClip).SetTime(0f);
            timeToNext = ((AnimationClipPlayable)mixer.GetInput(currentClip)).GetAnimationClip().length;
        }
        else if (timeToNext <= 0f && currentClip == mixer.GetInputCount() - 1)
        {
            mixer.SetInputWeight(mixer.GetInputCount() - 1, 0f);
            mixer.SetInputWeight(0, 1f);
            currentClip = 0;
            mixer.GetInput(currentClip).SetTime(0f);
            timeToNext = ((AnimationClipPlayable)mixer.GetInput(currentClip)).GetAnimationClip().length;
        }
    }
}
public class PlayableQueueSample : MonoBehaviour
{
    public AnimationClip[] clips;
    PlayableGraph graph;
    // Start is called before the first frame update
    void Start()
    {
        graph = PlayableGraph.Create();
        graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

        var queuePlayable = ScriptPlayable<PlayableQueue>.Create(graph);
        var queue = queuePlayable.GetBehaviour();
        queue.Init(graph, queuePlayable, clips);

        var output = AnimationPlayableOutput.Create(graph, "Anim", GetComponent<Animator>());
        output.SetSourcePlayable(queuePlayable);

        graph.Play();
    }

    private void OnDisable()
    {
        graph.Destroy();
    }
}
