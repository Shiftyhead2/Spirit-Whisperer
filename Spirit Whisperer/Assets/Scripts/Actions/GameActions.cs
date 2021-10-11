using System;

public class GameActions
{
    public static Action<int> onButtonPress;
    public static Action<int> onAwaitResponse;
    public static Action<bool> onQuestionInOrder;
    public static Action onResponseFailed;
    public static Action onResponseSucceded;
    public static Action<bool> onDisableToggleButton;
    public static Action onToggleInformation;
    public static Action onTogglePanels;
    public static Action<int> onQuestionAsked;
}
