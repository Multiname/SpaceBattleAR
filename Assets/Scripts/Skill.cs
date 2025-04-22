using UnityEngine;

[RequireComponent(typeof(Spaceship))]
public abstract class Skill : MonoBehaviour
{
    [SerializeField]
    protected Spaceship spaceship;
    [HideInInspector]
    public GameManager GameManager { protected get; set; }

    abstract public void UseSkill();
    public bool Available { get; protected set; } = true;
}
