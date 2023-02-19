using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class AnimController : MonoBehaviour
{
    [SerializeField] private AnimancerComponent character;
    [SerializeField] private AnimationClip run, dance, dead, down;

    public void calLDownAnim()
    {
        character.Play(down, 0.2f);
    }
    public void CallRunAnim()
    {
        character.Play(run, 0.2f);
    }
    public void CallDanceAnim()
    {
        character.Play(dance, 0.2f);
    }
    public void CallDeadAnim()
    {
        character.Play(dead, 0.2f);
    }
}
