//using unityengine;
//using unityeditor.animations;

//public class recordanimation : monobehaviour
//{
//    public animationclip clip;

//    private gameobjectrecorder gameobjectrecorder;

//    void start()
//    {
//        gameobjectrecorder = new gameobjectrecorder(gameobject);
//        gameobjectrecorder.bindcomponentsoftype<transform>(gameobject, true);
//    }

//    void lateupdate()
//    {
//        if (clip == null)
//            return;
//        gameobjectrecorder.takesnapshot(time.deltatime);
//    }

//    void ondisable()
//    {
//        if (clip == null)
//            return;

//        if (gameobjectrecorder.isrecording)
//        {
//            gameobjectrecorder.savetoclip(clip);
//        }
//    }
//}