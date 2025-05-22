using UnityEngine;

[RequireComponent(typeof(Spaceship))]
public abstract class Skill : MonoBehaviour
{
    [SerializeField]
    private GameObject skillAvailable;

    [SerializeField]
    protected Spaceship spaceship;
    [HideInInspector]
    public GameManager GameManager { protected get; set; }

    [HideInInspector]
    public virtual bool Targeted { get => false; }

    private bool available = true;
    public bool Available {
        get => available;
        protected set {
            available = value;
            skillAvailable.SetActive(value);
        }
    }

    public abstract void Prepare();
    public abstract void Discharge();

    public void SpendAction() {
        Available = false;
        spaceship.ActionAvailable = false;
    }
}
