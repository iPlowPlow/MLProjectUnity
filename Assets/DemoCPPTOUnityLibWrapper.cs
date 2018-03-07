using System.Runtime.InteropServices;

public static class DemoCPPTOUnityLibWrapper {

    [DllImport("MachineLearningTP1")]
    public static extern int add_to_42(int value_to_add);
}
