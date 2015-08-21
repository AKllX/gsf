﻿/**************************************************************************\
   Copyright (c) 2008 - Gbtc, James Ritchie Carroll
   All rights reserved.
  
   Redistribution and use in source and binary forms, with or without
   modification, are permitted provided that the following conditions
   are met:
  
      * Redistributions of source code must retain the above copyright
        notice, this list of conditions and the following disclaimer.
       
      * Redistributions in binary form must reproduce the above
        copyright notice, this list of conditions and the following
        disclaimer in the documentation and/or other materials provided
        with the distribution.
  
   THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER "AS IS" AND ANY
   EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
   IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
   PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
   CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
   EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
   PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
   PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY
   OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
   (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
   OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
  
\**************************************************************************/

using System;

namespace System.Media.Music
{
    /// <summary>
    /// Provides a function signature for methods that damp an amplitude representing a
    /// lowering of the acoustic pressure over time.
    /// </summary>
    /// <param name="sampleIndex">Sample index (0 to <paramref name="samplePeriod"/> - 1).</param>
    /// <param name="samplePeriod">Total period, in whole samples per second (i.e., seconds of time * <paramref name="sampleRate"/>), over which to perform damping.</param>
    /// <param name="sampleRate">Number of samples per second, if useful for calculation.</param>
    /// <returns>Scaling factor in the range of zero to one used to damp an amplitude at the given sample index.</returns>
    public delegate double DampingFunction(long sampleIndex, long samplePeriod, int sampleRate);
    
    public static class Damping
	{
        /// <summary>
        /// Produces a damping signature that represents no damping over time.
        /// </summary>
        /// <param name="sampleIndex">Sample index (0 to <paramref name="samplePeriod"/> - 1).</param>
        /// <param name="samplePeriod">Total period, in whole samples per second (i.e., seconds of time * <paramref name="sampleRate"/>), over which to perform damping.</param>
        /// <param name="sampleRate">Number of samples per second, if useful for calculation.</param>
        /// <returns>Returns a scalar of 1.0 regardless to time.</returns>
        /// <remarks>
        /// Zero damped sounds would be produced by synthetic sources such as an electronic keyboard.
        /// </remarks>
        public static double Zero(long sampleIndex, long samplePeriod, int sampleRate)
        {
            return 1.0D;
        }

        /// <summary>
        /// Produces a natural damping curve similar to that of a piano - slowly damping over
        /// time until the key is released at which point the string is quickly damped.
        /// </summary>
        /// <param name="sampleIndex">Sample index (0 to <paramref name="samplePeriod"/> - 1).</param>
        /// <param name="samplePeriod">Total period, in whole samples per second (i.e., seconds of time * <paramref name="sampleRate"/>), over which to perform damping.</param>
        /// <param name="sampleRate">Number of samples per second, if useful for calculation.</param>
        /// <returns>Scaling factor used to damp an amplitude at the given time.</returns>
        /// <remarks>
        /// This damping algorithm combines both the linear and logarithmic damping algoriths to
        /// produce a more natural damping curve.
        /// </remarks>
        public static double Natural(long sampleIndex, long samplePeriod, int sampleRate)
        {
            return (Logarithmic(sampleIndex, samplePeriod, sampleRate) + 0.5D * Linear(sampleIndex, samplePeriod, sampleRate)) / 1.5D;
        }
        /// <summary>
        /// Produces a logarithmic damping curve - slowly damping with a sharp end from 1 to 0 over the <paramref name="samplePeriod"/>.
        /// </summary>
        /// <param name="sampleIndex">Sample index (0 to <paramref name="samplePeriod"/> - 1).</param>
        /// <param name="samplePeriod">Total period, in whole samples per second (i.e., seconds of time * <paramref name="sampleRate"/>), over which to perform damping.</param>
        /// <param name="sampleRate">Number of samples per second, if useful for calculation.</param>
        /// <returns>Scaling factor used to damp an amplitude at the given time.</returns>
        public static double Logarithmic(long sampleIndex, long samplePeriod, int sampleRate)
        {
            return Math.Log10(samplePeriod - sampleIndex) / Math.Log10(samplePeriod);
        }

        /// <summary>
        /// Produces a linear damping curve - damping with a perfect slope from 1 to 0 over the <paramref name="samplePeriod"/>.
        /// </summary>
        /// <param name="sampleIndex">Sample index (0 to <paramref name="samplePeriod"/> - 1).</param>
        /// <param name="samplePeriod">Total period, in whole samples per second (i.e., seconds of time * <paramref name="sampleRate"/>), over which to perform damping.</param>
        /// <param name="sampleRate">Number of samples per second, if useful for calculation.</param>
        /// <returns>Scaling factor used to damp an amplitude at the given time.</returns>
        public static double Linear(long sampleIndex, long samplePeriod, int sampleRate)
        {
            return (samplePeriod - sampleIndex) * (1.0D / samplePeriod);
        }

        /// <summary>
        /// Produces a reverse linear damping curve - damping with a perfect slope from 0 to 1 over the <paramref name="samplePeriod"/>.
        /// </summary>
        /// <param name="sampleIndex">Sample index (0 to <paramref name="samplePeriod"/> - 1).</param>
        /// <param name="samplePeriod">Total period, in whole samples per second (i.e., seconds of time * <paramref name="sampleRate"/>), over which to perform damping.</param>
        /// <param name="sampleRate">Number of samples per second, if useful for calculation.</param>
        /// <returns>Scaling factor used to damp an amplitude at the given time.</returns>
        /// <remarks>This is just used for an interesting note effect.</remarks>
        public static double ReverseLinear(long sampleIndex, long samplePeriod, int sampleRate)
        {
            return sampleIndex * (1.0D / samplePeriod);
        }
    }
}
