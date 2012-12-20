/*
 * Copyright (c) Contributors, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the OpenSimulator Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *
 */
using System;
using System.Collections.Generic;
using System.Text;
using OpenMetaverse;
using OpenSim.Framework;

namespace OpenSim.Region.Physics.BulletSPlugin
{
public abstract class BSMotor
{
    // Timescales and other things can be turned off by setting them to 'infinite'.
    public const float Infinite = 12345.6f;
    public readonly static Vector3 InfiniteVector = new Vector3(BSMotor.Infinite, BSMotor.Infinite, BSMotor.Infinite);

    public BSMotor(string useName)
    {
        UseName = useName;
        PhysicsScene = null;
    }
    public virtual void Reset() { }
    public virtual void Zero() { }

    // A name passed at motor creation for easily identifyable debugging messages.
    public string UseName { get; private set; }

    // Used only for outputting debug information. Might not be set so check for null.
    public BSScene PhysicsScene { get; set; }
    protected void MDetailLog(string msg, params Object[] parms)
    {
        if (PhysicsScene != null)
        {
            if (PhysicsScene.VehicleLoggingEnabled)
            {
                PhysicsScene.DetailLog(msg, parms);
            }
        }
    }
}

// Motor which moves CurrentValue to TargetValue over TimeScale seconds.
// The TargetValue decays in TargetValueDecayTimeScale and
//     the CurrentValue will be held back by FrictionTimeScale.
// This motor will "zero itself" over time in that the targetValue will
//    decay to zero and the currentValue will follow it to that zero.
//    The overall effect is for the returned correction value to go from large
//    values (the total difference between current and target minus friction)
//    to small and eventually zero values.
// TimeScale and TargetDelayTimeScale may be 'infinite' which means no decay.

// For instance, if something is moving at speed X and the desired speed is Y,
//    CurrentValue is X and TargetValue is Y. As the motor is stepped, new
//    values of CurrentValue are returned that approach the TargetValue.
// The feature of decaying TargetValue is so vehicles will eventually
//    come to a stop rather than run forever. This can be disabled by
//    setting TargetValueDecayTimescale to 'infinite'.
// The change from CurrentValue to TargetValue is linear over TimeScale seconds.
public class BSVMotor : BSMotor
{
    // public Vector3 FrameOfReference { get; set; }
    // public Vector3 Offset { get; set; }

    public virtual float TimeScale { get; set; }
    public virtual float TargetValueDecayTimeScale { get; set; }
    public virtual Vector3 FrictionTimescale { get; set; }
    public virtual float Efficiency { get; set; }

    public virtual float ErrorZeroThreshold { get; set; }

    public virtual Vector3 TargetValue { get; private set; }
    public virtual Vector3 CurrentValue { get; private set; }

    public BSVMotor(string useName)
        : base(useName)
    {
        TimeScale = TargetValueDecayTimeScale = BSMotor.Infinite;
        Efficiency = 1f;
        FrictionTimescale = BSMotor.InfiniteVector;
        CurrentValue = TargetValue = Vector3.Zero;
        ErrorZeroThreshold = 0.01f;
    }
    public BSVMotor(string useName, float timeScale, float decayTimeScale, Vector3 frictionTimeScale, float efficiency) 
        : this(useName)
    {
        TimeScale = timeScale;
        TargetValueDecayTimeScale = decayTimeScale;
        FrictionTimescale = frictionTimeScale;
        Efficiency = efficiency;
        CurrentValue = TargetValue = Vector3.Zero;
    }
    public void SetCurrent(Vector3 current)
    {
        CurrentValue = current;
    }
    public void SetTarget(Vector3 target)
    {
        TargetValue = target;
    }

    // Compute the next step and return the new current value
    public virtual Vector3 Step(float timeStep)
    {
        Vector3 origTarget = TargetValue;       // DEBUG
        Vector3 origCurrVal = CurrentValue;     // DEBUG

        Vector3 correction = Vector3.Zero;
        Vector3 error = TargetValue - CurrentValue;
        if (!error.ApproxEquals(Vector3.Zero, ErrorZeroThreshold))
        {
            correction = Step(timeStep, error);

            CurrentValue += correction;

            // The desired value reduces to zero which also reduces the difference with current.
            // If the decay time is infinite, don't decay at all.
            float decayFactor = 0f;
            if (TargetValueDecayTimeScale != BSMotor.Infinite)
            {
                decayFactor = (1.0f / TargetValueDecayTimeScale) * timeStep;
                TargetValue *= (1f - decayFactor);
            }

            // The amount we can correct the error is reduced by the friction
            Vector3 frictionFactor = Vector3.Zero;
            if (FrictionTimescale != BSMotor.InfiniteVector)
            {
                // frictionFactor = (Vector3.One / FrictionTimescale) * timeStep;
                // Individual friction components can be 'infinite' so compute each separately.
                frictionFactor.X = (FrictionTimescale.X == BSMotor.Infinite) ? 0f : (1f / FrictionTimescale.X);
                frictionFactor.Y = (FrictionTimescale.Y == BSMotor.Infinite) ? 0f : (1f / FrictionTimescale.Y);
                frictionFactor.Z = (FrictionTimescale.Z == BSMotor.Infinite) ? 0f : (1f / FrictionTimescale.Z);
                frictionFactor *= timeStep;
                CurrentValue *= (Vector3.One - frictionFactor);
            }

            MDetailLog("{0},  BSVMotor.Step,nonZero,{1},origCurr={2},origTarget={3},timeStep={4},error={5},corr={6},targetDecay={6},decayFact={7},frictFac{8},curr={9},target={10},ret={11}",
                                BSScene.DetailLogZero, UseName, origCurrVal, origTarget,
                                timeStep, error, correction,
                                TargetValueDecayTimeScale, decayFactor, frictionFactor,
                                CurrentValue, TargetValue, CurrentValue);
        }
        else
        {
            // Difference between what we have and target is small. Motor is done.
            CurrentValue = Vector3.Zero;
            TargetValue = Vector3.Zero;
            MDetailLog("{0},  BSVMotor.Step,zero,{1},ret={2}",
                        BSScene.DetailLogZero, UseName, CurrentValue);
        }

        return CurrentValue;
    }
    public virtual Vector3 Step(float timeStep, Vector3 error)
    {
        Vector3 returnCorrection = Vector3.Zero;
        if (!error.ApproxEquals(Vector3.Zero, ErrorZeroThreshold))
        {
            // correction =  error / secondsItShouldTakeToCorrect
            Vector3 correctionAmount = error / TimeScale * timeStep;

            returnCorrection = correctionAmount;
            MDetailLog("{0},  BSVMotor.Step,nonZero,{1},timeStep={2},timeScale={3},err={4},corr={5},frictTS={6},ret={7}",
                                    BSScene.DetailLogZero, UseName, timeStep, TimeScale, error,
                                    correctionAmount, FrictionTimescale, returnCorrection);
        }
        return returnCorrection;
    }
    public override string ToString()
    {
        return String.Format("<{0},curr={1},targ={2},decayTS={3},frictTS={4}>",
            UseName, CurrentValue, TargetValue, TargetValueDecayTimeScale, FrictionTimescale);
    }
}

public class BSFMotor : BSMotor
{
    public float TimeScale { get; set; }
    public float DecayTimeScale { get; set; }
    public float Friction { get; set; }
    public float Efficiency { get; set; }

    public float Target { get; private set; }
    public float CurrentValue { get; private set; }

    public BSFMotor(string useName, float timeScale, float decayTimescale, float friction, float efficiency)
        : base(useName)
    {
    }
    public void SetCurrent(float target)
    {
    }
    public void SetTarget(float target)
    {
    }
    public virtual float Step(float timeStep)
    {
        return 0f;
    }
}

// Proportional, Integral, Derivitive Motor
// Good description at http://www.answers.com/topic/pid-controller . Includes processes for choosing p, i and d factors.
public class BSPIDVMotor : BSVMotor
{
    // Larger makes more overshoot, smaller means converge quicker. Range of 0.1 to 10.
    public Vector3 proportionFactor { get; set; }
    public Vector3 integralFactor { get; set; }
    public Vector3 derivFactor { get; set; }
    // Arbritrary factor range.
    // EfficiencyHigh means move quickly to the correct number. EfficiencyLow means might over correct.
    public float EfficiencyHigh = 0.4f;
    public float EfficiencyLow = 4.0f;

    Vector3 IntegralFactor { get; set; }
    Vector3 LastError { get; set; }

    public BSPIDVMotor(string useName)
        : base(useName)
    {
        proportionFactor = new Vector3(1.00f, 1.00f, 1.00f);
        integralFactor = new Vector3(1.00f, 1.00f, 1.00f);
        derivFactor = new Vector3(1.00f, 1.00f, 1.00f);
        IntegralFactor = Vector3.Zero;
        LastError = Vector3.Zero;
    }

    public override void Zero()
    {
        base.Zero();
    }

    public override float Efficiency
    {
        get { return base.Efficiency; }
        set
        {
            base.Efficiency = Util.Clamp(value, 0f, 1f);
            // Compute factors based on efficiency.
            // If efficiency is high (1f), use a factor value that moves the error value to zero with little overshoot.
            // If efficiency is low (0f), use a factor value that overcorrects.
            // TODO: might want to vary contribution of different factor depending on efficiency.
            float factor = ((1f - this.Efficiency) * EfficiencyHigh + EfficiencyLow) / 3f;
            // float factor = (1f - this.Efficiency) * EfficiencyHigh + EfficiencyLow;
            proportionFactor = new Vector3(factor, factor, factor);
            integralFactor = new Vector3(factor, factor, factor);
            derivFactor = new Vector3(factor, factor, factor);
        }
    }

    // Ignore Current and Target Values and just advance the PID computation on this error.
    public Vector3 Step(float timeStep, Vector3 error)
    {
        // Add up the error so we can integrate over the accumulated errors
        IntegralFactor += error * timeStep;

        // A simple derivitive is the rate of change from the last error.
        Vector3 derivFactor = (error - LastError) * timeStep;
        LastError = error;

        // Correction = -(proportionOfPresentError +      accumulationOfPastError    +     rateOfChangeOfError)
        Vector3 ret   = -(error * proportionFactor + IntegralFactor * integralFactor + derivFactor * derivFactor);

        return ret;
    }
}
}
