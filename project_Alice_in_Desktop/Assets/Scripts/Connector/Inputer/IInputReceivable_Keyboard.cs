namespace Connector
{
    namespace Inputer
    {
        interface IInputReceivable
        {
            // ���͎�t�p�C���^�[�t�F�[�X

            float MoveH();
            bool JumpKey_W();
            bool JumpKey_Space();

            bool ActionKey_Down();
            bool ActionKey();
            bool ActionKeyUp();
        }
    }
}
