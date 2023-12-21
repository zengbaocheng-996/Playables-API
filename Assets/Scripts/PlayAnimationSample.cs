using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Playables;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public class PlayAnimationSample : MonoBehaviour
{
    public AnimationClip clip;
    private PlayableGraph playableGraph;
    private AnimationClipPlayable clipPlayable;

    // Start is called before the first frame update
    void Start()
    {
        playableGraph = PlayableGraph.Create();
        playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

        clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);

        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());
        playableOutput.SetSourcePlayable(clipPlayable);
        playableGraph.Play();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(clipPlayable.GetPlayState() == PlayState.Paused)
            {
                clipPlayable.Pause();
            }
            else
            {
                clipPlayable.Play();
                clipPlayable.SetTime(0f);
            }
        }
    }


    void OnDisable()
    {
        playableGraph.Destroy();
    }
}
