namespace Connector
{
    namespace Inputer
    {
        interface IInputReceivable
        {
            // ���͎�t�p�C���^�[�t�F�[�X

            float MoveH();
            bool JumpKey();

            bool WallJumpKey_A();
            bool WallJumpKey_D();

            bool WallStickKey_A();
            bool WallStickKey_D();

            bool ActionKey_Down();
            bool ActionKey();
            bool ActionKeyUp();
        }
    }
}
