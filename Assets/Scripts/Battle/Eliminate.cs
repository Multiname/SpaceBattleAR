public class Eliminate : Skill
{
    public override bool Targeted { get => true; }

    public override void Prepare() {
        SkillEventManager.TargetedSkillUsed += UseSkill;
    }

    public override void Discharge() {
        SkillEventManager.TargetedSkillUsed -= UseSkill;
    }

    public void UseSkill(Spaceship target) {
        GameManager.EliminateSpaceship(spaceship, target);
        SpendAction();
    }
}
