using System;
using System.ComponentModel;
using System.Diagnostics;

namespace ZTStudio.My
{
    internal static partial class MyProject
    {
        internal partial class MyForms
        {
            [EditorBrowsable(EditorBrowsableState.Never)]
            public FrmBatchConversion m_FrmBatchConversion;

            public FrmBatchConversion FrmBatchConversion
            {
                [DebuggerHidden]
                get
                {
                    m_FrmBatchConversion = Create__Instance__(m_FrmBatchConversion);
                    return m_FrmBatchConversion;
                }

                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_FrmBatchConversion))
                        return;
                    if (value is object)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_FrmBatchConversion);
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public FrmBatchOffsetFix m_FrmBatchOffsetFix;

            public FrmBatchOffsetFix FrmBatchOffsetFix
            {
                [DebuggerHidden]
                get
                {
                    m_FrmBatchOffsetFix = Create__Instance__(m_FrmBatchOffsetFix);
                    return m_FrmBatchOffsetFix;
                }

                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_FrmBatchOffsetFix))
                        return;
                    if (value is object)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_FrmBatchOffsetFix);
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public FrmMain m_FrmMain;

            public FrmMain FrmMain
            {
                [DebuggerHidden]
                get
                {
                    m_FrmMain = Create__Instance__(m_FrmMain);
                    return m_FrmMain;
                }

                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_FrmMain))
                        return;
                    if (value is object)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_FrmMain);
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public FrmPal m_FrmPal;

            public FrmPal FrmPal
            {
                [DebuggerHidden]
                get
                {
                    m_FrmPal = Create__Instance__(m_FrmPal);
                    return m_FrmPal;
                }

                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_FrmPal))
                        return;
                    if (value is object)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_FrmPal);
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public FrmSettings m_FrmSettings;

            public FrmSettings FrmSettings
            {
                [DebuggerHidden]
                get
                {
                    m_FrmSettings = Create__Instance__(m_FrmSettings);
                    return m_FrmSettings;
                }

                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_FrmSettings))
                        return;
                    if (value is object)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_FrmSettings);
                }
            }
        }
    }
}