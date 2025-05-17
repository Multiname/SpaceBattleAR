public class StepForward : Skill
{
    public override void Prepare() {
        SkillEventManager.SkillUsed += UseSkill;
    }

    public override void Discharge() {
        SkillEventManager.SkillUsed -= UseSkill;
    }

    private void UseSkill() {
        if (GameManager.TryToMoveSpaceshipForward(spaceship)) {
            SpendAction();
        }
    }
}
