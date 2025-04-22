public class StepForward : Skill
{
    public override void UseSkill() {
        if (GameManager.TryToMoveSpaceshipForward(spaceship)) {
            Available = false;
        }
    }
}
