namespace Connector
{
    namespace Inputer
    {
        interface IInputReceivable
        {
            // ���͎�t�p�C���^�[�t�F�[�X

            float MoveH();
            bool JumpKey();

            bool ActionKey_Down();
            bool ActionKey();
            bool ActionKeyUp();
        }
    }
}
