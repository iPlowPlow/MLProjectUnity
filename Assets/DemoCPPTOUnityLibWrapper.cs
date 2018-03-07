using System.Runtime.InteropServices;

public static class DemoCPPTOUnityLibWrapper {

    [DllImport("MachineLearningTP1")]
	public static extern System.IntPtr linear_create();

	//[DllImport("MachineLearningTP1")]
	//public static extern void linear_train_classify(double* W, int elem, int elemsize, double* tabSphere);


}
