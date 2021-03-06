﻿using System.Runtime.InteropServices;

public static class DemoCPPTOUnityLibWrapper
{

    [DllImport("MachineLearningTP1")]
    public static extern System.IntPtr linear_create(int type, int size);

    [DllImport("MachineLearningTP1")]
    public unsafe static extern void linear_delete(System.IntPtr W);

    [DllImport("MachineLearningTP1")]
    public unsafe static extern void linear_train_classification(System.IntPtr W, int elem, int elemsize, double[] tabSphere, double alpha, int iter);

    [DllImport("MachineLearningTP1")]
    public unsafe static extern double linear_classify(System.IntPtr W, double x, double y);

    [DllImport("MachineLearningTP1")]
    public unsafe static extern void linear_train_regression(System.IntPtr W, int elem, int elemsize, double[] tabSphere);

    [DllImport("MachineLearningTP1")]
    public unsafe static extern void linear_train_RBF(System.IntPtr W, int elem, int elemsize, double[] tabSphere, double gamma);

    [DllImport("MachineLearningTP1")]
    public unsafe static extern double execute_RBF(System.IntPtr W, double[] x, double[] tabSphere, int elem, int elemsize, double gamma);



}