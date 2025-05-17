using UnityEngine;

[RequireComponent(typeof(Spaceship))]
public abstract class Skill : MonoBehaviour
{
    [SerializeField]
    protected Spaceship spaceship;
    [HideInInspector]
    public GameManager GameManager { protected get; set; }

    [HideInInspector]
    public virtual bool Targeted { get => false; }

    public bool Available { get; protected set; } = true;

    public abstract void Prepare();
    public abstract void Discharge();

    public void SpendAction() {
        Available = false;
        spaceship.ActionAvailable = false;
    }
}
