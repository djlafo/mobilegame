using System.Collections.Generic;
using System;

public class gameManager
{

    static gameManager inst;
    public static gameManager Inst
    {
        get
        {
            if (inst == null)
                inst = new gameManager();
            return inst;
        }
    }

    public static void reset()
    {
        inst = new gameManager();
    }

    private gameManager()
    {
        observer.Inst.listen("speechQueueFinished", speechQueueFinished);
    }

    void speechQueueFinished(List<object> l)
    {
        if (!dontAutoEnableControls)
            controlsEnabled = true;
    }

    bool controlsEnabled = true;
    bool dontAutoEnableControls = false;
    public bool ControlsEnabled
    {
        get
        {
            return controlsEnabled;
        }
    }

    public void setControls(bool enabled)
    {
        controlsEnabled = enabled;
    }

}
