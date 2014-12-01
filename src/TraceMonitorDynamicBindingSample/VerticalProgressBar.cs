#region File Header

// /********************************************************************
//  * COPYRIGHT:
//  *    This software program is furnished to the user under license
//  *    by eSymmetrix, Inc, and use thereof is subject to applicable 
//  *    U.S. and international law. This software program may not be 
//  *    reproduced, transmitted, or disclosed to third parties, in 
//  *    whole or in part, in any form or by any manner, electronic or
//  *    mechanical, without the express written consent of eSymmetrix, Inc,
//  *    except to the extent provided for by applicable license.
//  *
//  *    Copyright © 2008 by eSymmetrix, Inc.  All rights reserved.
//  *******************************************************************/

#endregion

#region Using Statements

using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Gibraltar.TraceMonitorSamples
{
    public partial class VerticalProgressBar : Control
    {
        private Color m_BorderColor = Color.Black;
        private int m_BorderWidth = 50;
        private Color m_FillColor = Color.Green;
        private volatile bool m_Invalidated;
        private int m_ProgressInPercentage = 10;

        public VerticalProgressBar()
        {
            m_ProgressInPercentage = 0;

            InitializeComponent();

            // Enable double buffering
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        public Color FillColor
        {
            get { return m_FillColor; }
            set
            {
                m_FillColor = value;

                ThreadSafeInvalidate();
            }
        }

        public Color BorderColor
        {
            get { return m_BorderColor; }
            set
            {
                m_BorderColor = value;

                ThreadSafeInvalidate();
            }
        }

        public int BorderWidth
        {
            get { return m_BorderWidth; }
            set
            {
                m_BorderWidth = value;

                ThreadSafeInvalidate();
            }
        }

        public int ProgressInPercentage
        {
            get { return m_ProgressInPercentage; }
            set
            {
                m_ProgressInPercentage = value;


                if (m_ProgressInPercentage > 95)
                    Trace.TraceError("Man, that's not good! The value is now about 95%, well, actually it's {0}%", m_ProgressInPercentage);
                else if (m_ProgressInPercentage > 75)
                    Trace.TraceWarning("Uh, oh, the value is still raising, now already by {0}%", m_ProgressInPercentage);
                else if (m_ProgressInPercentage > 50)
                    Trace.TraceWarning("Hmm, the value is going up to now {0}%.", m_ProgressInPercentage);
                else if (m_ProgressInPercentage > 10)
                    Trace.TraceInformation("Aha, something is going on, the value seems to increase, it's now by {0}%", m_ProgressInPercentage);
                else
                    Trace.WriteLine("Cool, nothing to worry about, still below 10%: " + m_ProgressInPercentage + "%", Name);

                ThreadSafeInvalidate();
            }
        }

        private void ThreadSafeInvalidate()
        {
            if (!m_Invalidated)
            {
                m_Invalidated = true;
                if (InvokeRequired)
                    Invoke(new MethodInvoker(Invalidate));
                else
                    Invalidate();
            }
        }

        protected override void OnResize(System.EventArgs e)
        {
            m_Invalidated = true;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Pen pen = null;
            SolidBrush brush = null;

            base.OnPaint(pe);

            try
            {
                pen = new Pen(m_BorderColor, m_BorderWidth);
                brush = new SolidBrush(m_FillColor);

                Rectangle borderRec = ClientRectangle;

                borderRec.X = borderRec.X + m_BorderWidth / 2;
                borderRec.Y = borderRec.Y + m_BorderWidth / 2;
                borderRec.Width = borderRec.Width - m_BorderWidth;
                borderRec.Height = borderRec.Height - m_BorderWidth;

                int fillHeight = (borderRec.Height * (100 - m_ProgressInPercentage)) / 100;

                Rectangle fillRec = new Rectangle(borderRec.X, borderRec.Y + fillHeight, borderRec.Width, borderRec.Height - fillHeight);

                pe.Graphics.FillRectangle(brush, fillRec);
                pe.Graphics.DrawRectangle(pen, borderRec);
            }
            finally
            {
                m_Invalidated = false;

                if (pen != null)
                    pen.Dispose();

                if (brush != null)
                    brush.Dispose();
            }
        }
    }
}