namespace Connector 
{
    namespace Player 
    {
        interface IPlayerStatusSentable 
        {
            float ScaleMagnification { get; set; }

            void PlayerIsInput(bool flg);
        }
    }
}