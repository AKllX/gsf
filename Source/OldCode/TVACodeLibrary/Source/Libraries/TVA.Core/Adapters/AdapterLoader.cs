﻿//*******************************************************************************************************
//  AdapterLoader.cs - Gbtc
//
//  Tennessee Valley Authority, 2009
//  No copyright is claimed pursuant to 17 USC § 105.  All Other Rights Reserved.
//
//  This software is made freely available under the TVA Open Source Agreement (see below).
//
//  Code Modification History:
//  -----------------------------------------------------------------------------------------------------
//  07/20/2009 - Pinal C. Patel
//       Generated original version of source code.
//  08/06/2009 - Pinal C. Patel
//       Modified Dispose(boolean) to iterate through the adapter collection correctly.
//  09/14/2009 - Stephen C. Wills
//       Added new header and license agreement.
//  09/17/2009 - Pinal C. Patel
//       Modified ProcessAdapter() to instantiate types with a default public contructor only.
//  12/10/2009 - Pinal C. Patel
//       Added new AdapterCreated event.
//       Implemented IProvideStatus interface.
//       Enhanced the implementation of Enabled property.
//  09/23/2010 - Pinal C. Patel
//       Added adapter isolation capability to allow for loading qualified adapters in seperate 
//       application domain for isolated execution.
//  10/01/2010 - Pinal C. Patel
//       Added adapter monitoring capability to monitor the resource utilization of adapters.
//
//*******************************************************************************************************

#region [ TVA Open Source Agreement ]
/*

 THIS OPEN SOURCE AGREEMENT ("AGREEMENT") DEFINES THE RIGHTS OF USE,REPRODUCTION, DISTRIBUTION,
 MODIFICATION AND REDISTRIBUTION OF CERTAIN COMPUTER SOFTWARE ORIGINALLY RELEASED BY THE
 TENNESSEE VALLEY AUTHORITY, A CORPORATE AGENCY AND INSTRUMENTALITY OF THE UNITED STATES GOVERNMENT
 ("GOVERNMENT AGENCY"). GOVERNMENT AGENCY IS AN INTENDED THIRD-PARTY BENEFICIARY OF ALL SUBSEQUENT
 DISTRIBUTIONS OR REDISTRIBUTIONS OF THE SUBJECT SOFTWARE. ANYONE WHO USES, REPRODUCES, DISTRIBUTES,
 MODIFIES OR REDISTRIBUTES THE SUBJECT SOFTWARE, AS DEFINED HEREIN, OR ANY PART THEREOF, IS, BY THAT
 ACTION, ACCEPTING IN FULL THE RESPONSIBILITIES AND OBLIGATIONS CONTAINED IN THIS AGREEMENT.

 Original Software Designation: openPDC
 Original Software Title: The TVA Open Source Phasor Data Concentrator
 User Registration Requested. Please Visit https://naspi.tva.com/Registration/
 Point of Contact for Original Software: J. Ritchie Carroll <mailto:jrcarrol@tva.gov>

 1. DEFINITIONS

 A. "Contributor" means Government Agency, as the developer of the Original Software, and any entity
 that makes a Modification.

 B. "Covered Patents" mean patent claims licensable by a Contributor that are necessarily infringed by
 the use or sale of its Modification alone or when combined with the Subject Software.

 C. "Display" means the showing of a copy of the Subject Software, either directly or by means of an
 image, or any other device.

 D. "Distribution" means conveyance or transfer of the Subject Software, regardless of means, to
 another.

 E. "Larger Work" means computer software that combines Subject Software, or portions thereof, with
 software separate from the Subject Software that is not governed by the terms of this Agreement.

 F. "Modification" means any alteration of, including addition to or deletion from, the substance or
 structure of either the Original Software or Subject Software, and includes derivative works, as that
 term is defined in the Copyright Statute, 17 USC § 101. However, the act of including Subject Software
 as part of a Larger Work does not in and of itself constitute a Modification.

 G. "Original Software" means the computer software first released under this Agreement by Government
 Agency entitled openPDC, including source code, object code and accompanying documentation, if any.

 H. "Recipient" means anyone who acquires the Subject Software under this Agreement, including all
 Contributors.

 I. "Redistribution" means Distribution of the Subject Software after a Modification has been made.

 J. "Reproduction" means the making of a counterpart, image or copy of the Subject Software.

 K. "Sale" means the exchange of the Subject Software for money or equivalent value.

 L. "Subject Software" means the Original Software, Modifications, or any respective parts thereof.

 M. "Use" means the application or employment of the Subject Software for any purpose.

 2. GRANT OF RIGHTS

 A. Under Non-Patent Rights: Subject to the terms and conditions of this Agreement, each Contributor,
 with respect to its own contribution to the Subject Software, hereby grants to each Recipient a
 non-exclusive, world-wide, royalty-free license to engage in the following activities pertaining to
 the Subject Software:

 1. Use

 2. Distribution

 3. Reproduction

 4. Modification

 5. Redistribution

 6. Display

 B. Under Patent Rights: Subject to the terms and conditions of this Agreement, each Contributor, with
 respect to its own contribution to the Subject Software, hereby grants to each Recipient under Covered
 Patents a non-exclusive, world-wide, royalty-free license to engage in the following activities
 pertaining to the Subject Software:

 1. Use

 2. Distribution

 3. Reproduction

 4. Sale

 5. Offer for Sale

 C. The rights granted under Paragraph B. also apply to the combination of a Contributor's Modification
 and the Subject Software if, at the time the Modification is added by the Contributor, the addition of
 such Modification causes the combination to be covered by the Covered Patents. It does not apply to
 any other combinations that include a Modification. 

 D. The rights granted in Paragraphs A. and B. allow the Recipient to sublicense those same rights.
 Such sublicense must be under the same terms and conditions of this Agreement.

 3. OBLIGATIONS OF RECIPIENT

 A. Distribution or Redistribution of the Subject Software must be made under this Agreement except for
 additions covered under paragraph 3H. 

 1. Whenever a Recipient distributes or redistributes the Subject Software, a copy of this Agreement
 must be included with each copy of the Subject Software; and

 2. If Recipient distributes or redistributes the Subject Software in any form other than source code,
 Recipient must also make the source code freely available, and must provide with each copy of the
 Subject Software information on how to obtain the source code in a reasonable manner on or through a
 medium customarily used for software exchange.

 B. Each Recipient must ensure that the following copyright notice appears prominently in the Subject
 Software:

          No copyright is claimed pursuant to 17 USC § 105.  All Other Rights Reserved.

 C. Each Contributor must characterize its alteration of the Subject Software as a Modification and
 must identify itself as the originator of its Modification in a manner that reasonably allows
 subsequent Recipients to identify the originator of the Modification. In fulfillment of these
 requirements, Contributor must include a file (e.g., a change log file) that describes the alterations
 made and the date of the alterations, identifies Contributor as originator of the alterations, and
 consents to characterization of the alterations as a Modification, for example, by including a
 statement that the Modification is derived, directly or indirectly, from Original Software provided by
 Government Agency. Once consent is granted, it may not thereafter be revoked.

 D. A Contributor may add its own copyright notice to the Subject Software. Once a copyright notice has
 been added to the Subject Software, a Recipient may not remove it without the express permission of
 the Contributor who added the notice.

 E. A Recipient may not make any representation in the Subject Software or in any promotional,
 advertising or other material that may be construed as an endorsement by Government Agency or by any
 prior Recipient of any product or service provided by Recipient, or that may seek to obtain commercial
 advantage by the fact of Government Agency's or a prior Recipient's participation in this Agreement.

 F. In an effort to track usage and maintain accurate records of the Subject Software, each Recipient,
 upon receipt of the Subject Software, is requested to register with Government Agency by visiting the
 following website: https://naspi.tva.com/Registration/. Recipient's name and personal information
 shall be used for statistical purposes only. Once a Recipient makes a Modification available, it is
 requested that the Recipient inform Government Agency at the web site provided above how to access the
 Modification.

 G. Each Contributor represents that that its Modification does not violate any existing agreements,
 regulations, statutes or rules, and further that Contributor has sufficient rights to grant the rights
 conveyed by this Agreement.

 H. A Recipient may choose to offer, and to charge a fee for, warranty, support, indemnity and/or
 liability obligations to one or more other Recipients of the Subject Software. A Recipient may do so,
 however, only on its own behalf and not on behalf of Government Agency or any other Recipient. Such a
 Recipient must make it absolutely clear that any such warranty, support, indemnity and/or liability
 obligation is offered by that Recipient alone. Further, such Recipient agrees to indemnify Government
 Agency and every other Recipient for any liability incurred by them as a result of warranty, support,
 indemnity and/or liability offered by such Recipient.

 I. A Recipient may create a Larger Work by combining Subject Software with separate software not
 governed by the terms of this agreement and distribute the Larger Work as a single product. In such
 case, the Recipient must make sure Subject Software, or portions thereof, included in the Larger Work
 is subject to this Agreement.

 J. Notwithstanding any provisions contained herein, Recipient is hereby put on notice that export of
 any goods or technical data from the United States may require some form of export license from the
 U.S. Government. Failure to obtain necessary export licenses may result in criminal liability under
 U.S. laws. Government Agency neither represents that a license shall not be required nor that, if
 required, it shall be issued. Nothing granted herein provides any such export license.

 4. DISCLAIMER OF WARRANTIES AND LIABILITIES; WAIVER AND INDEMNIFICATION

 A. No Warranty: THE SUBJECT SOFTWARE IS PROVIDED "AS IS" WITHOUT ANY WARRANTY OF ANY KIND, EITHER
 EXPRESSED, IMPLIED, OR STATUTORY, INCLUDING, BUT NOT LIMITED TO, ANY WARRANTY THAT THE SUBJECT
 SOFTWARE WILL CONFORM TO SPECIFICATIONS, ANY IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
 PARTICULAR PURPOSE, OR FREEDOM FROM INFRINGEMENT, ANY WARRANTY THAT THE SUBJECT SOFTWARE WILL BE ERROR
 FREE, OR ANY WARRANTY THAT DOCUMENTATION, IF PROVIDED, WILL CONFORM TO THE SUBJECT SOFTWARE. THIS
 AGREEMENT DOES NOT, IN ANY MANNER, CONSTITUTE AN ENDORSEMENT BY GOVERNMENT AGENCY OR ANY PRIOR
 RECIPIENT OF ANY RESULTS, RESULTING DESIGNS, HARDWARE, SOFTWARE PRODUCTS OR ANY OTHER APPLICATIONS
 RESULTING FROM USE OF THE SUBJECT SOFTWARE. FURTHER, GOVERNMENT AGENCY DISCLAIMS ALL WARRANTIES AND
 LIABILITIES REGARDING THIRD-PARTY SOFTWARE, IF PRESENT IN THE ORIGINAL SOFTWARE, AND DISTRIBUTES IT
 "AS IS."

 B. Waiver and Indemnity: RECIPIENT AGREES TO WAIVE ANY AND ALL CLAIMS AGAINST GOVERNMENT AGENCY, ITS
 AGENTS, EMPLOYEES, CONTRACTORS AND SUBCONTRACTORS, AS WELL AS ANY PRIOR RECIPIENT. IF RECIPIENT'S USE
 OF THE SUBJECT SOFTWARE RESULTS IN ANY LIABILITIES, DEMANDS, DAMAGES, EXPENSES OR LOSSES ARISING FROM
 SUCH USE, INCLUDING ANY DAMAGES FROM PRODUCTS BASED ON, OR RESULTING FROM, RECIPIENT'S USE OF THE
 SUBJECT SOFTWARE, RECIPIENT SHALL INDEMNIFY AND HOLD HARMLESS  GOVERNMENT AGENCY, ITS AGENTS,
 EMPLOYEES, CONTRACTORS AND SUBCONTRACTORS, AS WELL AS ANY PRIOR RECIPIENT, TO THE EXTENT PERMITTED BY
 LAW.  THE FOREGOING RELEASE AND INDEMNIFICATION SHALL APPLY EVEN IF THE LIABILITIES, DEMANDS, DAMAGES,
 EXPENSES OR LOSSES ARE CAUSED, OCCASIONED, OR CONTRIBUTED TO BY THE NEGLIGENCE, SOLE OR CONCURRENT, OF
 GOVERNMENT AGENCY OR ANY PRIOR RECIPIENT.  RECIPIENT'S SOLE REMEDY FOR ANY SUCH MATTER SHALL BE THE
 IMMEDIATE, UNILATERAL TERMINATION OF THIS AGREEMENT.

 5. GENERAL TERMS

 A. Termination: This Agreement and the rights granted hereunder will terminate automatically if a
 Recipient fails to comply with these terms and conditions, and fails to cure such noncompliance within
 thirty (30) days of becoming aware of such noncompliance. Upon termination, a Recipient agrees to
 immediately cease use and distribution of the Subject Software. All sublicenses to the Subject
 Software properly granted by the breaching Recipient shall survive any such termination of this
 Agreement.

 B. Severability: If any provision of this Agreement is invalid or unenforceable under applicable law,
 it shall not affect the validity or enforceability of the remainder of the terms of this Agreement.

 C. Applicable Law: This Agreement shall be subject to United States federal law only for all purposes,
 including, but not limited to, determining the validity of this Agreement, the meaning of its
 provisions and the rights, obligations and remedies of the parties.

 D. Entire Understanding: This Agreement constitutes the entire understanding and agreement of the
 parties relating to release of the Subject Software and may not be superseded, modified or amended
 except by further written agreement duly executed by the parties.

 E. Binding Authority: By accepting and using the Subject Software under this Agreement, a Recipient
 affirms its authority to bind the Recipient to all terms and conditions of this Agreement and that
 Recipient hereby agrees to all terms and conditions herein.

 F. Point of Contact: Any Recipient contact with Government Agency is to be directed to the designated
 representative as follows: J. Ritchie Carroll <mailto:jrcarrol@tva.gov>.

*/
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using TVA.Collections;
using TVA.IO;
using TVA.Units;

namespace TVA.Adapters
{
    /// <summary>
    /// Represents a generic loader of adapters.
    /// </summary>
    /// <typeparam name="T"><see cref="Type"/> of adapters to be loaded.</typeparam>
    /// <example>
    /// This example show how to use the <see cref="AdapterLoader{T}"/> to isolate adapters in seperate <see cref="AppDomain"/>s and monitor their resource usage:
    /// <code>
    /// using System;
    /// using System.Collections.Generic;
    /// using System.Text;
    /// using System.Threading;
    /// using TVA;
    /// using TVA.Adapters;
    /// using TVA.Security.Cryptography;
    /// using TVA.Units;
    /// 
    /// class Program
    /// {
    ///     static AdapterLoader&lt;PublishAdapterBase&gt; s_adapterLoader;
    /// 
    ///     static void Main(string[] args)
    ///     {
    ///         // Enable app domain resource monitoring.
    ///         AppDomain.MonitoringIsEnabled = true;
    /// 
    ///         // Load adapters that mimic data publishing.
    ///         s_adapterLoader = new AdapterLoader&lt;PublishAdapterBase&gt;();
    ///         s_adapterLoader.IsolateAdapters = true;
    ///         s_adapterLoader.MonitorAdapters = true;
    ///         s_adapterLoader.AllowableProcessMemoryUsage = 200 * SI2.Mega;
    ///         s_adapterLoader.AllowableProcessProcessorUsage = 50;
    ///         s_adapterLoader.AllowableAdapterMemoryUsage = 100 * SI2.Mega;
    ///         s_adapterLoader.AllowableAdapterProcessorUsage = 25;
    ///         s_adapterLoader.AdapterLoaded += OnAdapterLoaded;
    ///         s_adapterLoader.AdapterUnloaded += OnAdapterUnloaded;
    ///         s_adapterLoader.AdapterResourceUsageExceeded += OnAdapterResourceUsageExceeded;
    ///         s_adapterLoader.Initialize();
    /// 
    ///         // Take user input to alter adapter working state.
    ///         string userInput;
    ///         Console.WriteLine("Possible commands:");
    ///         Console.WriteLine("  Connect - Connects all disconnected adapters");
    ///         Console.WriteLine("  Disconnect - Disconnects all connected adapters");
    ///         Console.WriteLine("  Exit - Exits this application\r\n");
    ///         while (string.Compare(userInput = Console.ReadLine(), "Exit", true) != 0)
    ///         {
    ///             switch (userInput.ToLower())
    ///             {
    ///                 case "connect":
    ///                     // Connect all adapters.
    ///                     Console.Write("Connecting adapters...");
    ///                     foreach (PublishAdapterBase adapter in s_adapterLoader.Adapters)
    ///                     {
    ///                         adapter.Enabled = true;
    ///                     }
    ///                     Console.WriteLine("done.\r\n");
    ///                     break;
    ///                 case "disconnect":
    ///                     // Disconnect all adapters.
    ///                     Console.Write("Disconnecting adapters...");
    ///                     foreach (PublishAdapterBase adapter in s_adapterLoader.Adapters)
    ///                     {
    ///                         adapter.Enabled = false;
    ///                     }
    ///                     Console.WriteLine("done.\r\n");
    ///                     break;
    ///             }
    ///         }
    /// 
    ///         // Shutdown.
    ///         s_adapterLoader.Dispose();
    ///     }
    /// 
    ///     static void OnAdapterLoaded(object sender, EventArgs&lt;PublishAdapterBase&gt; e)
    ///     {
    ///         Console.WriteLine("{0} has been loaded\r\n", e.Argument.GetType().Name);
    ///     }
    /// 
    ///     static void OnAdapterUnloaded(object sender, EventArgs&lt;PublishAdapterBase&gt; e)
    ///     {
    ///         Console.WriteLine("{0} has been unloaded\r\n", e.Argument.GetType().Name);
    ///     }
    /// 
    ///     static void OnAdapterResourceUsageExceeded(object sender, TVA.EventArgs&lt;PublishAdapterBase&gt; e)
    ///     {
    ///         Console.WriteLine("{0} status:", e.Argument.Name);
    ///         Console.WriteLine(e.Argument.Status);
    /// 
    ///         // Remove the adapter in order to reclaim the resources used by it.
    ///         lock (s_adapterLoader.Adapters)
    ///         {
    ///             s_adapterLoader.Adapters.Remove(e.Argument);
    ///         }
    ///     }
    /// }
    /// 
    /// /// &lt;summary&gt;
    /// /// Base adapter class.
    /// /// &lt;/summary&lg;
    /// public abstract class PublishAdapterBase : Adapter
    /// {
    ///     public PublishAdapterBase()
    ///     {
    ///         Data = new List&lt;byte[]&gt;();
    ///     }
    /// 
    ///     public List&lt;byte[]&gt; Data { get; set; }
    /// 
    ///     public override void Initialize()
    ///     {
    ///         base.Initialize();
    ///         new Thread(Publish).Start();
    ///         Enabled = true;
    ///     }
    /// 
    ///     protected abstract void Publish();
    /// }
    /// 
    /// /// &lt;summary&gt;
    /// /// Adapter that does not manage memory well.
    /// /// &lt;/summary&gt;
    /// public class PublisAdapterA : PublishAdapterBase
    /// {
    ///     protected override void Publish()
    ///     {
    ///         while (true)
    ///         {
    ///             for (int i = 0; i &lt; 10000; i++)
    ///             {
    ///                 Data.Add(new byte[10]);
    ///             }
    ///             Thread.Sleep(100);
    /// 
    ///             if (Enabled)
    ///                 Data.RemoveRange(0, Data.Count / 2);
    ///         }
    ///     }
    /// }
    /// 
    /// /// &lt;summary&gt;
    /// /// Adapter that uses the processor in excess.
    /// /// &lt;/summary&gt;
    /// public class PublishAdapterB : PublishAdapterBase
    /// {
    ///     protected override void Publish()
    ///     {
    ///         string text = string.Empty;
    ///         System.Random random = new System.Random();
    ///         while (true)
    ///         {
    ///             for (int i = 0; i &lt; 10; i++)
    ///             {
    ///                 for (int j = 0; j &lt; 4; j++)
    ///                 {
    ///                     text += (char)random.Next(256);
    ///                 }
    ///                 Data.Add(Encoding.ASCII.GetBytes(text.Encrypt("C1pH3r", CipherStrength.Aes256)).BlockCopy(0, 1));
    ///             }
    ///             Thread.Sleep(10);
    /// 
    ///             if (Enabled)
    ///                 Data.RemoveRange(0, Data.Count / 2);
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="Adapter"/>
    /// <seealso cref="IAdapter"/>
    public class AdapterLoader<T> : ISupportLifecycle, IProvideStatus
    {
        #region [ Members ]

        // Constants

        /// <summary>
        /// Specified the default value for the <see cref="AdapterDirectory"/> property.
        /// </summary>
        public const string DefaultAdapterDirectory = "";

        /// <summary>
        /// Specifies the default value for the <see cref="WatchForAdapters"/> property.
        /// </summary>
        public const bool DefaultWatchForAdapters = true;

        /// <summary>
        /// Specifies the default value for the <see cref="IsolateAdapters"/> property.
        /// </summary>
        public const bool DefaultIsolateAdapters = false;

        /// <summary>
        /// Specifies the default value for the <see cref="MonitorAdapters"/> property.
        /// </summary>
        public const bool DefaultMonitorAdapters = false;

        /// <summary>
        /// Specifies the default value for the <see cref="AllowableProcessMemoryUsage"/> property
        /// </summary>
        public const double DefaultAllowableProcessMemoryUsage = 1000 * SI2.Mega;

        /// <summary>
        /// Specifies the default value for the <see cref="AllowableProcessProcessorUsage"/> property.
        /// </summary>
        public const double DefaultAllowableProcessProcessorUsage = 75;

        /// <summary>
        /// Specifies the default value for the <see cref="AllowableAdapterMemoryUsage"/> property.
        /// </summary>
        public const double DefaultAllowableAdapterMemoryUsage = 100 * SI2.Mega;

        /// <summary>
        /// Specifies the default value for the <see cref="AllowableAdapterProcessorUsage"/> property.
        /// </summary>
        public const double DefaultAllowableAdapterProcessorUsage = 50;

        // Events

        /// <summary>
        /// Occurs when a new adapter is found and instantiated.
        /// </summary>
        /// <remarks>
        /// <see cref="EventArgs{T}.Argument"/> is the adapter that was created.
        /// </remarks>
        public event EventHandler<EventArgs<T>> AdapterCreated;

        /// <summary>
        /// Occurs when a new adapter is loaded to the <see cref="Adapters"/> list.
        /// </summary>
        /// <remarks>
        /// <see cref="EventArgs{T}.Argument"/> is the adapter that was loaded.
        /// </remarks>
        public event EventHandler<EventArgs<T>> AdapterLoaded;

        /// <summary>
        /// Occurs when an existing adapter is unloaded from the <see cref="Adapters"/> list.
        /// </summary>
        /// <remarks>
        /// <see cref="EventArgs{T}.Argument"/> is the adapter that was unloaded.
        /// </remarks>
        public event EventHandler<EventArgs<T>> AdapterUnloaded;

        /// <summary>
        /// Occurs when an adapter has exceeded either the <see cref="AllowableAdapterMemoryUsage"/> or <see cref="AllowableAdapterProcessorUsage"/>.
        /// </summary>
        /// <see cref="EventArgs{T}.Argument"/> is the adapter that exceeded the allowable system resource utlization.
        public event EventHandler<EventArgs<T>> AdapterResourceUsageExceeded;

        /// <summary>
        /// Occurs when an <see cref="Exception"/> is encountered when loading an adapter.
        /// </summary>
        /// <remarks>
        /// <see cref="EventArgs{T1,T2}.Argument1"/> is the <see cref="Type"/> of adapter that was being loaded.<br/>
        /// <see cref="EventArgs{T1,T2}.Argument2"/> is the <see cref="Exception"/> encountered when loading the adapter.
        /// </remarks>
        public event EventHandler<EventArgs<Type, Exception>> AdapterLoadException;

        /// <summary>
        /// Occurs when an <see cref="Exception"/> is encountered while executing a queued operation on one the <see cref="Adapters"/>.
        /// </summary>
        /// <remarks>
        /// <see cref="EventArgs{T1,T2}.Argument1"/> is the adapter on which the operation was being executed.<br/>
        /// <see cref="EventArgs{T1,T2}.Argument2"/> is the <see cref="Exception"/> encountered when executing an operation on the adapter.
        /// </remarks>
        public event EventHandler<EventArgs<T, Exception>> OperationExecutionException;

        // Fields
        private string m_adapterDirectory;
        private bool m_watchForAdapters;
        private bool m_isolateAdapters;
        private bool m_monitorAdapters;
        private double m_allowableProcessMemoryUsage;
        private double m_allowableProcessProcessorUsage;
        private double m_allowableAdapterMemoryUsage;
        private double m_allowableAdapterProcessorUsage;
        private ObservableCollection<T> m_adapters;
        private FileSystemWatcher m_adapterWatcher;
        private ProcessQueue<object> m_operationQueue;
        private Dictionary<Type, bool> m_enabledStates;
        private Thread m_adapterMonitoringThread;
        private bool m_enabled;
        private bool m_disposed;
        private bool m_initialized;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="AdapterLoader{T}"/> class.
        /// </summary>
        public AdapterLoader()
        {
            m_adapterDirectory = DefaultAdapterDirectory;
            m_watchForAdapters = DefaultWatchForAdapters;
            m_isolateAdapters = DefaultIsolateAdapters;
            m_monitorAdapters = DefaultMonitorAdapters;
            m_allowableProcessMemoryUsage = DefaultAllowableProcessMemoryUsage;
            m_allowableProcessProcessorUsage = DefaultAllowableProcessProcessorUsage;
            m_allowableAdapterMemoryUsage = DefaultAllowableAdapterMemoryUsage;
            m_allowableAdapterProcessorUsage = DefaultAllowableAdapterProcessorUsage;
            m_adapters = new ObservableCollection<T>();
            m_adapters.CollectionChanged += Adapters_CollectionChanged;
            m_adapterWatcher = new FileSystemWatcher();
            m_adapterWatcher.Created += AdapterWatcher_Created;
            m_operationQueue = ProcessQueue<object>.CreateRealTimeQueue(ExecuteOperation);
            m_enabledStates = new Dictionary<Type, bool>();
        }

        /// <summary>
        /// Releases the unmanaged resources before the <see cref="AdapterLoader{T}"/> is reclaimed by <see cref="GC"/>.
        /// </summary>
        ~AdapterLoader()
        {
            Dispose(false);
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the directory where <see cref="Adapters"/> are located.
        /// </summary>
        /// <remarks>
        /// When an empty string is assigned to <see cref="AdapterDirectory"/>, <see cref="Adapters"/> are loaded from the directory where application is executing.
        /// </remarks>
        /// <exception cref="ArgumentNullException">The value being assigned is a null string.</exception>
        public string AdapterDirectory
        {
            get
            {
                return FilePath.GetAbsolutePath(m_adapterDirectory);
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                m_adapterDirectory = value;
            }
        }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether new assemblies added at runtime will be processed for <see cref="Adapters"/>.
        /// </summary>
        public bool WatchForAdapters
        {
            get
            {
                return m_watchForAdapters;
            }
            set
            {
                m_watchForAdapters = value;
            }
        }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether <see cref="Adapters"/> are loaded in seperate <see cref="AppDomain"/> for isolated execution.
        /// </summary>
        public bool IsolateAdapters
        {
            get
            {
                return m_isolateAdapters;
            }
            set
            {
                m_isolateAdapters = value;
            }
        }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether resource utilization of <see cref="Adapters"/> executing in <see cref="IsolateAdapters">isolation</see> is to be monitored.
        /// </summary>
        /// <remarks>
        /// Use <see cref="AllowableProcessMemoryUsage"/>, <see cref="AllowableProcessProcessorUsage"/>, <see cref="AllowableAdapterMemoryUsage"/> and 
        /// <see cref="AllowableAdapterProcessorUsage"/> properties to configure how adapter resource utilization is to be monitored.
        /// </remarks>
        /// <exception cref="InvalidOperationException"><see cref="Adapters"/> do not implement the <see cref="IAdapter"/> interface.</exception>
        public bool MonitorAdapters
        {
            get
            {
                return m_monitorAdapters;
            }
            set
            {
                if (!(typeof(T) == typeof(IAdapter) ||
                      typeof(T).GetInterface(typeof(IAdapter).FullName) != null))
                    throw new InvalidOperationException(string.Format("{0} does not implement {1} interface", typeof(T).Name, typeof(IAdapter).Name));

                m_monitorAdapters = value;
            }
        }

        /// <summary>
        /// Gets or sets the processor time in % the current process is allowed to use before the internal monitoring process starts looking for offending <see cref="Adapters"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value being assigned is zero or negative.</exception>
        public double AllowableProcessMemoryUsage
        {
            get
            {
                return m_allowableProcessMemoryUsage;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");

                m_allowableProcessMemoryUsage = value;
            }
        }

        /// <summary>
        /// Gets or sets the memory in bytes the current process is allowed to use before the internal monitoring process starts looking for offending <see cref="Adapters"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value being assigned is zero or negative.</exception>
        public double AllowableProcessProcessorUsage
        {
            get
            {
                return m_allowableProcessProcessorUsage;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");

                m_allowableProcessProcessorUsage = value;
            }
        }

        /// <summary>
        /// Gets or sets the processor time in % the <see cref="Adapters"/> are allowed to use before being flagged as culprits by the internal monitoring process.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value being assigned is zero or negative.</exception>
        public double AllowableAdapterMemoryUsage
        {
            get
            {
                return m_allowableAdapterMemoryUsage;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");

                m_allowableAdapterMemoryUsage = value;
            }
        }

        /// <summary>
        /// Gets or sets the memory in bytes the <see cref="Adapters"/> are allowed to use before being flagged as culprits by the internal monitoring process.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value being assigned is zero or negative.</exception>
        public double AllowableAdapterProcessorUsage
        {
            get
            {
                return m_allowableAdapterProcessorUsage;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");

                m_allowableAdapterProcessorUsage = value;
            }
        }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether the <see cref="AdapterLoader{T}"/> is currently enabled.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return m_enabled;
            }
            set
            {
                if (!m_initialized && value)
                {
                    // Initialize if uninitialized when enabled.
                    Initialize();
                }
                else
                {
                    // Change current state of various components.
                    if (value && !m_enabled)
                    {
                        // Enable - restore previously saved state.
                        m_adapterWatcher.EnableRaisingEvents = m_enabledStates[m_adapterWatcher.GetType()];
                        m_operationQueue.Enabled = m_enabledStates[m_operationQueue.GetType()];

                        bool savedState;
                        ISupportLifecycle implementingAdapter;
                        lock (m_adapters)
                        {
                            foreach (T adapter in m_adapters)
                            {
                                implementingAdapter = adapter as ISupportLifecycle;
                                if (implementingAdapter != null &&
                                    m_enabledStates.TryGetValue(implementingAdapter.GetType(), out savedState))
                                    implementingAdapter.Enabled = savedState;
                            }
                        }
                    }
                    else if (!value && m_enabled)
                    {
                        // Disable - save current state and disable.
                        SaveCurrentState();
                        m_adapterWatcher.EnableRaisingEvents = false;
                        m_operationQueue.Enabled = false;

                        ISupportLifecycle implementingAdapter;
                        lock (m_adapters)
                        {
                            foreach (T adapter in m_adapters)
                            {
                                implementingAdapter = adapter as ISupportLifecycle;
                                if (implementingAdapter != null)
                                    implementingAdapter.Enabled = false;
                            }
                        }
                    }
                }
                m_enabled = value;
            }
        }

        /// <summary>
        /// Gets a list of adapters loaded from the <see cref="AdapterDirectory"/>.
        /// </summary>
        public IList<T> Adapters
        {
            get
            {
                return m_adapters;
            }
        }

        /// <summary>
        /// Gets the unique identifier of the <see cref="AdapterLoader{T}"/>.
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        /// <summary>
        /// Gets the descriptive status of the <see cref="AdapterLoader{T}"/>.
        /// </summary>
        public string Status
        {
            get
            {
                StringBuilder status = new StringBuilder();
                status.Append("             Adapters type: ");
                status.Append(typeof(T).Name);
                status.AppendLine();
                status.Append("        Adapters directory: ");
                status.Append(FilePath.TrimFileName(AdapterDirectory, 30));
                status.AppendLine();
                status.Append("         Adapter isolation: ");
                status.Append(m_isolateAdapters ? "Enabled" : "Disabled");
                status.AppendLine();
                status.Append("        Adapter monitoring: ");
                status.Append(m_monitorAdapters ? "Enabled" : "Disabled");
                status.AppendLine();
                status.Append("           Dynamic loading: ");
                status.Append(m_adapterWatcher.EnableRaisingEvents ? "Enabled" : "Disabled");
                status.AppendLine();
                status.Append("          Operations queue: ");
                status.Append(m_operationQueue.Enabled ? "Enabled" : "Disabled");
                status.AppendLine();
                IProvideStatus implementingAdapter;
                lock (m_adapters)
                {
                    status.Append("     Total loaded adapters: ");
                    status.Append(m_adapters.Count);
                    status.AppendLine();
                    foreach (T adapter in m_adapters)
                    {
                        implementingAdapter = adapter as IProvideStatus;
                        if (implementingAdapter != null)
                        {
                            status.AppendLine();
                            status.Append("              Adapter name: ");
                            status.Append(implementingAdapter.Name);
                            status.AppendLine();
                            status.Append(implementingAdapter.Status);
                        }
                    }
                }

                return status.ToString();
            }
        }

        /// <summary>
        /// Gets the <see cref="FileSystemWatcher"/> object watching for new adapter assemblies added at runtime.
        /// </summary>
        protected FileSystemWatcher AdapterWatcher
        {
            get
            {
                return m_adapterWatcher;
            }
        }

        /// <summary>
        /// Gets the <see cref="ProcessQueue{T}"/> object to be used for queuing operations to be executed on <see cref="Adapters"/>.
        /// </summary>
        protected ProcessQueue<object> OperationQueue
        {
            get
            {
                return m_operationQueue;
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Releases all the resources used by the <see cref="AdapterLoader{T}"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Initializes the <see cref="AdapterLoader{T}"/>.
        /// </summary>
        public virtual void Initialize()
        {
            if (!m_initialized)
            {
                Initialize(typeof(T).LoadImplementations(Path.Combine(AdapterDirectory, "*.*")));
            }
        }

        /// <summary>
        /// Initializes the <see cref="AdapterLoader{T}"/>.
        /// </summary>
        /// <param name="adapterTypes">Collection of adapter <see cref="Type"/>s from which <see cref="Adapters"/> are to be created.</param>
        public virtual void Initialize(IEnumerable<Type> adapterTypes)
        {
            if (!m_initialized)
            {
                // Process adapters.
                foreach (Type type in adapterTypes)
                {
                    ProcessAdapter(type);
                }

                // Watch for adapters.
                if (m_watchForAdapters)
                {
                    m_adapterWatcher.Path = AdapterDirectory;
                    m_adapterWatcher.Filter = "*.dll";
                    m_adapterWatcher.EnableRaisingEvents = true;
                }

                // Save current state.
                SaveCurrentState();

                // Start adapter monitoring.
                if (m_monitorAdapters && m_isolateAdapters)
                {
                    // Following must be true in order for us to monitor adapter resources:
                    // - Adapter monitoring is enabled.
                    // - Adapter isolation is enabled.
                    // - Adapters must have implemented the IAdapter interface.
                    m_adapterMonitoringThread = new Thread(MonitorAdapterResources);
                    m_adapterMonitoringThread.Start();
                }

                m_enabled = true;       // Mark as enabled.
                m_initialized = true;   // Initialize only once.
            }
        }

        /// <summary>
        /// Processes the <paramref name="adapterType"/> by creating its instance and initializing it.
        /// </summary>
        /// <param name="adapterType"><see cref="Type"/> of the adapter to be instantiated and initialized.</param>
        protected virtual void ProcessAdapter(Type adapterType)
        {
            try
            {
                if (adapterType.GetConstructor(Type.EmptyTypes) != null)
                {
                    // Instantiate adapter instance.
                    T adapter;
                    if (m_isolateAdapters &&
                        (adapterType.GetRootType() == typeof(MarshalByRefObject) ||
                         adapterType.GetInterface(typeof(ISerializable).FullName) != null ||
                         adapterType.GetCustomAttributes(typeof(SerializableAttribute), false).Length > 0))
                    {
                        // Adapter isolation is enabled and possible.
                        AppDomain domain = AppDomain.CreateDomain(adapterType.Name);
                        adapter = (T)domain.CreateInstanceAndUnwrap(adapterType.Assembly.FullName, adapterType.FullName);
                    }
                    else
                    {
                        // Load adapter in the current executing domain.
                        adapter = (T)(Activator.CreateInstance(adapterType));
                    }
                    OnAdapterCreated(adapter);

                    // Initialize adapter if supported.
                    ISupportLifecycle initializableAdapter = adapter as ISupportLifecycle;
                    if (initializableAdapter != null)
                        initializableAdapter.Initialize();

                    // Add adapter and notify via event.
                    lock (m_adapters)
                    {
                        m_adapters.Add(adapter);
                    }
                }
            }
            catch (Exception ex)
            {
                OnAdapterLoadException(adapterType, ex);
            }
        }

        /// <summary>
        /// Monitors the resource utilization of <see cref="Adapters"/>.
        /// </summary>
        protected virtual void MonitorAdapterResources()
        {
            // Enable individual application domain resource tracking if it is enabled.
            if (!AppDomain.MonitoringIsEnabled)
                AppDomain.MonitoringIsEnabled = true;

            Process currentProcess;
            double processMemoryUsage;
            double processProcessorUsage;
            List<IAdapter> offendingAdapters = new List<IAdapter>();
            while (!m_disposed)
            {
                Thread.Sleep(5000);

                // Don't interfere if process memory and processor utlization is in check.
                currentProcess = Process.GetCurrentProcess();
                processMemoryUsage = GetMemoryUsage(currentProcess);
                processProcessorUsage = GetProcessorUsage(currentProcess);
                if (processMemoryUsage <= m_allowableProcessMemoryUsage &&
                    processProcessorUsage <= m_allowableProcessProcessorUsage)
                    continue;

                if (Monitor.TryEnter(m_adapters))
                {
                    try
                    {
                        // Force a full blocking GC so the memory usage of adapters is updated.
                        GC.Collect();

                        foreach (IAdapter adapter in m_adapters)
                        {
                            if (adapter.MemoryUsage > m_allowableAdapterMemoryUsage ||
                                adapter.ProcessorUsage > m_allowableAdapterProcessorUsage)
                                offendingAdapters.Add(adapter);
                        }
                    }
                    finally
                    {
                        Monitor.Exit(m_adapters);
                    }

                    // Notify about the offending adapters.
                    foreach (T adapter in offendingAdapters)
                    {
                        OnAdapterResourceUsageExceeded(adapter);
                    }
                    offendingAdapters.Clear();
                }
            }
        }

        /// <summary>
        /// Gets the memory usage in bytes of the specified <paramref name="process"/>.
        /// </summary>
        /// <param name="process">The <see cref="Process"/> whose memory usage is to be determined.</param>
        /// <returns>Memory usage in bytes of the specified <paramref name="process"/>.</returns>
        protected virtual double GetMemoryUsage(Process process)
        {
            return process.WorkingSet64;
        }

        /// <summary>
        /// Gets the % processor usage of the specified <paramref name="process"/>.
        /// </summary>
        /// <param name="process">The <see cref="Process"/> whose processot usage is to be determined.</param>
        /// <returns>Processor usage in % of the specified <paramref name="process"/>.</returns>
        protected virtual double GetProcessorUsage(Process process)
        {
            return process.TotalProcessorTime.TotalSeconds / Ticks.ToSeconds(DateTime.Now - process.StartTime) / Environment.ProcessorCount * 100;
        }

        /// <summary>
        /// Executes an operation on the <paramref name="adapter"/> with the given <paramref name="data"/>.
        /// </summary>
        /// <param name="adapter">Adapter on which an operation is to be executed.</param>
        /// <param name="data">Data to be used when executing an operation.</param>
        /// <exception cref="NotSupportedException">Always</exception>
        protected virtual void ExecuteAdapterOperation(T adapter, object data)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="AdapterLoader{T}"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                try
                {
                    // This will be done regardless of whether the object is finalized or disposed.				
                    if (disposing)
                    {
                        // This will be done only when the object is disposed by calling Dispose().
                        if (m_enabledStates != null)
                            m_enabledStates.Clear();

                        if (m_operationQueue != null)
                            m_operationQueue.Dispose();

                        if (m_adapterWatcher != null)
                        {
                            m_adapterWatcher.Created -= AdapterWatcher_Created;
                            m_adapterWatcher.Dispose();
                        }

                        if (m_adapters != null)
                        {
                            lock (m_adapters)
                            {
                                for (int i = 0; i < m_adapters.Count; i += 0)
                                {
                                    m_adapters.RemoveAt(0);
                                }
                            }
                            m_adapters.CollectionChanged -= Adapters_CollectionChanged;
                        }
                    }
                }
                finally
                {
                    m_enabled = false;  // Mark as disabled.
                    m_disposed = true;  // Prevent duplicate dispose.
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="AdapterCreated"/> event.
        /// </summary>
        /// <param name="adapter">Adapter instance to send to <see cref="AdapterCreated"/> event.</param>
        protected virtual void OnAdapterCreated(T adapter)
        {
            if (AdapterCreated != null)
                AdapterCreated(this, new EventArgs<T>(adapter));
        }

        /// <summary>
        /// Raises the <see cref="AdapterLoaded"/> event.
        /// </summary>
        /// <param name="adapter">Adapter instance to send to <see cref="AdapterLoaded"/> event.</param>
        protected virtual void OnAdapterLoaded(T adapter)
        {
            if (AdapterLoaded != null)
                AdapterLoaded(this, new EventArgs<T>(adapter));
        }

        /// <summary>
        /// Raises the <see cref="AdapterUnloaded"/> event.
        /// </summary>
        /// <param name="adapter">Adapter instance to send to <see cref="AdapterUnloaded"/> event.</param>
        protected virtual void OnAdapterUnloaded(T adapter)
        {
            try
            {
                // Dispose the adapter.
                IDisposable disposableAdapter = adapter as IDisposable;
                if (disposableAdapter != null)
                    disposableAdapter.Dispose();
            }
            catch { }

            try
            {
                // Unload adapter domain.
                IAdapter isolatedAdapter = adapter as IAdapter;
                if (isolatedAdapter != null && !isolatedAdapter.Domain.IsDefaultAppDomain())
                    AppDomain.Unload(isolatedAdapter.Domain);
            }
            catch { }

            if (AdapterUnloaded != null)
                AdapterUnloaded(this, new EventArgs<T>(adapter));
        }

        /// <summary>
        /// Raises the <see cref="AdapterResourceUsageExceeded"/> event.
        /// </summary>
        /// <param name="adapter">Adapter instance to send to <see cref="AdapterResourceUsageExceeded"/> event.</param>
        protected virtual void OnAdapterResourceUsageExceeded(T adapter)
        {
            if (AdapterResourceUsageExceeded != null)
                AdapterResourceUsageExceeded(this, new EventArgs<T>(adapter));
        }

        /// <summary>
        /// Raises the <see cref="AdapterLoadException"/> event.
        /// </summary>
        /// <param name="adapter"><see cref="Type"/> to send to <see cref="AdapterLoadException"/> event.</param>
        /// <param name="exception"><see cref="Exception"/> to send to <see cref="AdapterLoadException"/> event.</param>
        protected virtual void OnAdapterLoadException(Type adapter, Exception exception)
        {
            if (AdapterLoadException != null)
                AdapterLoadException(this, new EventArgs<Type, Exception>(adapter, exception));
        }

        /// <summary>
        /// Raises the <see cref="OperationExecutionException"/> event.
        /// </summary>
        /// <param name="adapter">Adapter instance to send to <see cref="OperationExecutionException"/> event.</param>
        /// <param name="exception"><see cref="Exception"/> to send to <see cref="OperationExecutionException"/> event.</param>
        protected virtual void OnOperationExecutionException(T adapter, Exception exception)
        {
            if (OperationExecutionException != null)
                OperationExecutionException(this, new EventArgs<T, Exception>(adapter, exception));
        }

        private void SaveCurrentState()
        {
            m_enabledStates[m_adapterWatcher.GetType()] = m_adapterWatcher.EnableRaisingEvents;
            m_enabledStates[m_operationQueue.GetType()] = m_operationQueue.Enabled;

            ISupportLifecycle implementingAdapter;
            lock (m_adapters)
            {
                foreach (T adapter in m_adapters)
                {
                    implementingAdapter = adapter as ISupportLifecycle;
                    if (implementingAdapter != null)
                        m_enabledStates[implementingAdapter.GetType()] = implementingAdapter.Enabled;
                }
            }
        }

        private void ExecuteOperation(object[] data)
        {
            foreach (object operationData in data)
            {
                lock (m_adapters)
                {
                    foreach (T adapter in m_adapters)
                    {
                        try
                        {
                            ExecuteAdapterOperation(adapter, operationData);
                        }
                        catch (Exception ex)
                        {
                            OnOperationExecutionException(adapter, ex);
                        }
                    }
                }
            }
        }

        private void AdapterWatcher_Created(object sender, FileSystemEventArgs e)
        {
            // Process newly added assemblies at runtime for adapters.
            foreach (Type type in typeof(T).LoadImplementations(e.FullPath))
            {
                ProcessAdapter(type);
            }
        }

        private void Adapters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    // Notify additions.
                    foreach (T adapter in e.NewItems)
                    {
                        OnAdapterLoaded(adapter);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    // Notify deletions.
                    foreach (T adapter in e.OldItems)
                    {
                        OnAdapterUnloaded(adapter);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    // Notify deletions.
                    foreach (T adapter in e.OldItems)
                    {
                        OnAdapterUnloaded(adapter);
                    }
                    // Notify additions.
                    foreach (T adapter in e.NewItems)
                    {
                        OnAdapterLoaded(adapter);
                    }
                    break;
            }
        }

        #endregion
    }
}
