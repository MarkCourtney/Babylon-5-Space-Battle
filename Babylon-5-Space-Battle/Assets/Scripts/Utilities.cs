// Copyright (c) 2002-2003, Sony Computer Entertainment America
// Copyright (c) 2002-2003, Craig Reynolds <craig_reynolds@playstation.sony.com>
// Copyright (C) 2007 Bjoern Graf <bjoern.graf@gmx.net>
// Copyright (C) 2007 Michael Coles <michael@digini.com>
// All rights reserved.
//
// This software is licensed as described in the file license.txt, which
// you should have received as part of this distribution. The terms
// are also available at http://www.codeplex.com/SharpSteer/Project/License.aspx.

using UnityEngine;
using System;
using System.Collections.Generic;

public class Utilities
{

    public static Vector3 RandomPosition(float range)
    {
        Vector3 pos = new Vector3();
        pos.x = (RNG.Next() % range) - (range / 2);
        pos.y = (RNG.Next() % range) - (range / 2);
        pos.z = (RNG.Next() % range) - (range / 2);
        return pos;
    }

    public static System.Random RNG = new System.Random(DateTime.Now.Millisecond);

    public static float Interpolate(float alpha, float x0, float x1)
    {
        return x0 + ((x1 - x0) * alpha);
    }

    public static Vector3 Interpolate(float alpha, Vector3 x0, Vector3 x1)
    {
        return x0 + ((x1 - x0) * alpha);
    }

    /// <summary>
    /// Constrain a given value (x) to be between two (ordered) bounds min
    /// and max.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns>Returns x if it is between the bounds, otherwise returns the nearer bound.</returns>
    public static float Clip(float x, float min, float max)
    {
        if (x < min) return min;
        if (x > max) return max;
        return x;
    }


    /// <summary>
    /// blends new values into an accumulator to produce a smoothed time series
    /// </summary>
    /// <remarks>
    /// Modifies its third argument, a reference to the float accumulator holding
    /// the "smoothed time series."
    /// 
    /// The first argument (smoothRate) is typically made proportional to "dt" the
    /// simulation time step.  If smoothRate is 0 the accumulator will not change,
    /// if smoothRate is 1 the accumulator will be set to the new value with no
    /// smoothing.  Useful values are "near zero".
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    /// <param name="smoothRate"></param>
    /// <param name="newValue"></param>
    /// <param name="smoothedAccumulator"></param>
    /// <example>blendIntoAccumulator (dt * 0.4f, currentFPS, smoothedFPS)</example>
    public static void BlendIntoAccumulator(float smoothRate, float newValue, ref float smoothedAccumulator)
    {
        smoothedAccumulator = Interpolate(Clip(smoothRate, 0, 0.75f), smoothedAccumulator, newValue);
    }

    public static void BlendIntoAccumulator(float smoothRate, Vector3 newValue, ref Vector3 smoothedAccumulator)
    {
        smoothedAccumulator = Interpolate(Clip(smoothRate, 0, 0.75f), smoothedAccumulator, newValue);
    }
}
