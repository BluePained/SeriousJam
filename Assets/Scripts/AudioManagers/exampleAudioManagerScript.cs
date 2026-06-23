using UnityEngine;

public class exampleAudioManagerScript : MonoBehaviour
{
    public void doBurst()
    {
        //put this code anywhere to play sound of specified string name from global audio sfx manager
        //process is the same for global music player, but reference instead globalAudio.instance.Play("etc");
        globalAudio_SFX.instance.Play("testBurst");
    }

    public void doCharge()
    {
        globalAudio_SFX.instance.Play("testCharge");
    }

    public void stopCharge()
    {
        //put this code anywhere to stop sound of specified string name from global audio sfx manager
        globalAudio_SFX.instance.Stop("testCharge");
    }

    public void doComplete()
    {
        globalAudio_SFX.instance.Play("testFullCharge");
    }
}
