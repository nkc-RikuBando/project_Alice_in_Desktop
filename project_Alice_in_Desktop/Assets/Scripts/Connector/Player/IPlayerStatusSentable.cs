namespace Connector 
{
    namespace Player 
    {
        interface IPlayerStatusSentable 
        {
            void PlayerSizeChange(float mag);

            void PlayerIsInput(bool flg);
        }
    }
}