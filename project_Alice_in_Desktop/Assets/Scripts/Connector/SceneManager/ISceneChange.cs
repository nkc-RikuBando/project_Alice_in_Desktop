namespace Connector
{
    namespace MySceneManager
    {
        interface ISceneChange
        {
            // シーンを変更するインターフェース
            void SceneChange(string name);

            // 同じシーンをやり直すインターフェース
            void ReloadScene();
        }
    }
}
