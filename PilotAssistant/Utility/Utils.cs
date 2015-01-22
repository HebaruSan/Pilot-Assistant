﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PilotAssistant.Utility
{
    using PID;

    public static class Utils
    {
        internal static double Clamp(double val, double min, double max)
        {
            if (val < min)
                return min;
            else if (val > max)
                return max;
            else
                return val;
        }

        public static PID_Controller GetAsst(PIDList id)
        {
            return PilotAssistant.controllers[(int)id];
        }

        public static PID_Controller GetSAS(SASList id)
        {
            return SurfSAS.SASControllers[(int)id];
        }
    }
}
