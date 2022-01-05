namespace Connector 
{
    namespace Player 
    {
        interface IPlayerStatusSentable 
        {
            void PlayerSizeChange_Small();

            void PlayerSizeChange_Big();

            void PlayerIsInput(bool flg);
        }
    }
}