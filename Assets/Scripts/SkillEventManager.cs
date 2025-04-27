using UnityEngine;

public class SkillEventManager : MonoBehaviour
{
    public delegate void OnSkillUsed();
    public static OnSkillUsed SkillUsed;

    public delegate void OnTargetedSkillUsed(Spaceship target);
    public static OnTargetedSkillUsed TargetedSkillUsed;
}
