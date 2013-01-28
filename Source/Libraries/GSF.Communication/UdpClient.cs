﻿//******************************************************************************************************
//  UdpClient.cs - Gbtc
//
//  Copyright © 2012, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the Eclipse Public License -v 1.0 (the "License"); you may
//  not use this file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://www.opensource.org/licenses/eclipse-1.0.php
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  07/06/2006 - Pinal C. Patel
//       Original version of source code generated.
//  09/06/2006 - J. Ritchie Carroll
//       Added bypass optimizations for high-speed socket access.
//  09/27/2007 - J. Ritchie Carroll
//       Added disconnect timeout overload.
//  09/29/2008 - J. Ritchie Carroll
//       Converted to C#.
//  07/08/2009 - J. Ritchie Carroll
//       Added WaitHandle return value from asynchronous connection.
//  07/09/2009 - Pinal C. Patel
//       Modified to attempt resuming reception on SocketException for non-Handshake enabled connection.
//  07/15/2009 - Pinal C. Patel
//       Modified Disconnect() to add error checking.
//  07/17/2009 - Pinal C. Patel
//       Added support to specify a specific interface address on a multiple interface machine.
//  07/20/2009 - Pinal C. Patel
//       Allowed for UDP endpoint to not be bound to a local interface by specifying -1 for port number.
//  09/14/2009 - Stephen C. Wills
//       Added new header and license agreement.
//  10/30/2009 - Pinal C. Patel
//       Added true multicast support by allowing for socket level subscription to a multicast group.
//  11/17/2009 - Pinal C. Patel
//       Fixed a bug in the creation of random server endpoint when server endpoint information is 
//       omitted from the ConnectionString.
//  03/24/2010 - Pinal C. Patel
//       Updated the interpretation of server property in ConnectionString to correctly interpret 
//       IPv6 IP addresses according to IETF - A Recommendation for IPv6 Address Text Representation.
//  11/29/2010 - Pinal C. Patel
//       Corrected the implementation of ConnectAsync() method.
//  02/13/2011 - Pinal C. Patel
//       Modified ConnectAsync() to handle loopback address resolution failure on IPv6 enabled OSes.
//  07/23/2012 - Stephen C. Wills
//       Performed a full refactor to use the SocketAsyncEventArgs API calls.
//  10/31/2012 - Stephen C. Wills
//       Replaced single-threaded BlockingCollection pattern with asynchronous loop pattern.
//  12/13/2012 - Starlynn Danyelle Gilliam
//       Modified Header.
//
//******************************************************************************************************

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using GSF.Configuration;

namespace GSF.Communication
{
    /// <summary>
    /// Represents a UDP-based communication server.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use <see cref="UdpClient"/> when the primary purpose is to receive data.
    /// </para>
    /// <para>
    /// The <see cref="UdpClient.Client"/> socket can be bound to a specified interface on a machine with multiple interfaces by 
    /// specifying the interface in the <see cref="ClientBase.ConnectionString"/> (Example: "Server=localhost:8888; Port=8989; Interface=127.0.0.1")
    /// </para>
    /// <para>
    /// The <see cref="UdpClient.Client"/> socket can be used just for transmitting data without being bound to a local interface 
    /// by specifying -1 for the port number in the <see cref="ClientBase.ConnectionString"/> (Example: "Server=localhost:8888; Port=-1")
    /// </para>
    /// </remarks>
    /// <example>
    /// This example shows how to use the <see cref="UdpClient"/> component:
    /// <code>
    /// using System;
    /// using GSF;
    /// using GSF.Communication;
    /// using GSF.Security.Cryptography;
    /// using GSF.IO.Compression;
    /// 
    /// class Program
    /// {
    ///     static UdpClient s_client;
    /// 
    ///     static void Main(string[] args)
    ///     {
    ///         // Initialize the client.
    ///         s_client = new UdpClient("Server=localhost:8888; Port=8989");
    ///         s_client.Handshake = false;
    ///         s_client.ReceiveTimeout = -1;
    ///         s_client.Encryption = CipherStrength.None;
    ///         s_client.Compression = CompressionStrength.NoCompression;
    ///         s_client.SecureSession = false;
    ///         s_client.Initialize();
    ///         // Register event handlers.
    ///         s_client.ConnectionAttempt += s_client_ConnectionAttempt;
    ///         s_client.ConnectionEstablished += s_client_ConnectionEstablished;
    ///         s_client.ConnectionTerminated += s_client_ConnectionTerminated;
    ///         s_client.ReceiveDataComplete += s_client_ReceiveDataComplete;
    ///         // Connect the client.
    ///         s_client.Connect();
    /// 
    ///         // Transmit user input to the server.
    ///         string input;
    ///         while (string.Compare(input = Console.ReadLine(), "Exit", true) != 0)
    ///         {
    ///             s_client.Send(input);
    ///         }
    /// 
    ///         // Disconnect the client on shutdown.
    ///         s_client.Dispose();
    ///     }
    /// 
    ///     static void s_client_ConnectionAttempt(object sender, EventArgs e)
    ///     {
    ///         Console.WriteLine("Client is connecting to server.");
    ///     }
    /// 
    ///     static void s_client_ConnectionEstablished(object sender, EventArgs e)
    ///     {
    ///         Console.WriteLine("Client connected to server.");
    ///     }
    /// 
    ///     static void s_client_ConnectionTerminated(object sender, EventArgs e)
    ///     {
    ///         Console.WriteLine("Client disconnected from server.");
    ///     }
    /// 
    ///     static void s_client_ReceiveDataComplete(object sender, EventArgs&lt;byte[], int&gt; e)
    ///     {
    ///         Console.WriteLine(string.Format("Received data - {0}.", s_client.TextEncoding.GetString(e.Argument1, 0, e.Argument2)));
    ///     }
    /// }
    /// </code>
    /// </example>
    public class UdpClient : ClientBase
    {
        #region [ Members ]

        // Nested Types

        private class UdpClientPayload
        {
            public EndPoint Destination;
            public byte[] Data;
            public int Offset;
            public int Length;

            public ManualResetEventSlim WaitHandle;
        }

        // Constants

        /// <summary>
        /// Specifies the default value for the <see cref="ClientBase.ReceiveBufferSize"/> property.
        /// </summary>
        public new const int DefaultReceiveBufferSize = 65536;

        /// <summary>
        /// Specifies the default value for the <see cref="AllowDualStackSocket"/> property.
        /// </summary>
        public const bool DefaultAllowDualStackSocket = true;

        /// <summary>
        /// Specifies the default value for the <see cref="MaxSendQueueSize"/> property.
        /// </summary>
        public const int DefaultMaxSendQueueSize = -1;

        /// <summary>
        /// Specifies the default value for the <see cref="ClientBase.ConnectionString"/> property.
        /// </summary>
        public const string DefaultConnectionString = "Server=localhost:8888; Port=8989";

        /// <summary>
        /// Specifies the constant to be used for disabling <see cref="SocketError.ConnectionReset"/> when endpoint is not listening.
        /// </summary>
        private const int SIO_UDP_CONNRESET = -1744830452;

        // Events

        /// <summary>
        /// Occurs when unprocessed data has been received from the server.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This event can be used to receive a notification that server data has arrived. The <see cref="Read"/> method can then be used
        /// to copy data to an existing buffer. In many cases it will be optimal to use an existing buffer instead of subscribing to the
        /// <see cref="ReceiveDataFromComplete"/> event.
        /// </para>
        /// <para>
        /// <see cref="EventArgs{T1,T2}.Argument1"/> is the end-point that data has been received from.<br/>
        /// <see cref="EventArgs{T1,T2}.Argument2"/> is the number of bytes received in the buffer from the server.
        /// </para>
        /// </remarks>
        [Category("Data"),
        Description("Occurs when unprocessed data has been received from the server.")]
        public event EventHandler<EventArgs<EndPoint, IPPacketInformation, int>> ReceiveDataFrom;

        /// <summary>
        /// Occurs when data received from the server has been processed and is ready for consumption.
        /// </summary>
        /// <remarks>
        /// <see cref="EventArgs{T1,T2,T3}.Argument1"/> is the end-point that data has been received from.<br/>
        /// <see cref="EventArgs{T1,T2,T3}.Argument2"/> is a new buffer containing post-processed data received from the server starting at index zero.<br/>
        /// <see cref="EventArgs{T1,T2,T3}.Argument3"/> is the number of post-processed bytes received in the buffer from the server.
        /// </remarks>
        [Category("Data"),
        Description("Occurs when data received from the server has been processed and is ready for consumption.")]
        public event EventHandler<EventArgs<EndPoint, IPPacketInformation, byte[], int>> ReceiveDataFromComplete;

        // Fields
        //private bool m_destinationReachableCheck;
        private IPEndPoint m_udpServer;
        private TransportProvider<Socket> m_udpClient;
        private IPStack m_ipStack;
        private bool m_allowDualStackSocket;
        private int m_maxSendQueueSize;
        private Dictionary<string, string> m_connectData;
        private ManualResetEvent m_connectionHandle;
#if ThreadTracking
        private ManagedThread m_connectionThread;
#else
        private Thread m_connectionThread;
#endif

        private int m_sending;
        private SpinLock m_sendLock;
        private ConcurrentQueue<UdpClientPayload> m_sendQueue;
        private SocketAsyncEventArgs m_sendArgs;
        private SocketAsyncEventArgs m_receiveArgs;
        private EventHandler<SocketAsyncEventArgs> m_sendHandler;
        private EventHandler<SocketAsyncEventArgs> m_receiveHandler;
        private bool m_receivePacketInfo;

        private bool m_disposed;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="UdpClient"/> class.
        /// </summary>
        public UdpClient()
            : this(DefaultConnectionString)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UdpClient"/> class.
        /// </summary>
        /// <param name="connectString">Connect string of the <see cref="UdpClient"/>. See <see cref="DefaultConnectionString"/> for format.</param>
        public UdpClient(string connectString)
            : base(TransportProtocol.Udp, connectString)
        {
            base.ReceiveBufferSize = DefaultReceiveBufferSize;
            m_allowDualStackSocket = DefaultAllowDualStackSocket;
            m_maxSendQueueSize = DefaultMaxSendQueueSize;

            m_sendLock = new SpinLock();
            m_sendQueue = new ConcurrentQueue<UdpClientPayload>();
            m_sendHandler += (sender, args) => ProcessSend();
            m_receiveHandler += (sender, args) => ProcessReceive();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UdpClient"/> class.
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> object that contains the <see cref="UdpClient"/>.</param>
        public UdpClient(IContainer container)
            : this()
        {
            if (container != null)
                container.Add(this);
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets a boolean value that determines if dual-mode socket is allowed when endpoint address is IPv6.
        /// </summary>
        [Category("Settings"),
        DefaultValue(DefaultAllowDualStackSocket),
        Description("Determines if dual-mode socket is allowed when endpoint address is IPv6.")]
        public bool AllowDualStackSocket
        {
            get
            {
                return m_allowDualStackSocket;
            }
            set
            {
                m_allowDualStackSocket = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum size for the send queue before payloads are dumped from the queue.
        /// </summary>
        [Category("Settings"),
        DefaultValue(DefaultMaxSendQueueSize),
        Description("The maximum size for the send queue before payloads are dumped from the queue.")]
        public int MaxSendQueueSize
        {
            get
            {
                return m_maxSendQueueSize;
            }
            set
            {
                m_maxSendQueueSize = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="TransportProvider{Socket}"/> object for the <see cref="UdpClient"/>.
        /// </summary>
        [Browsable(false)]
        public TransportProvider<Socket> Client
        {
            get
            {
                return m_udpClient;
            }
        }

        /// <summary>
        /// Gets the server URI of the <see cref="UdpClient"/>.
        /// </summary>
        [Browsable(false)]
        public override string ServerUri
        {
            get
            {
                return string.Format("{0}://{1}", TransportProtocol, m_connectData["server"]).ToLower();
            }
        }

        /// <summary>
        /// Gets or sets the size of the buffer used by the client for receiving data from the server.
        /// </summary>
        /// <exception cref="ArgumentException">The value being assigned is either zero or negative.</exception>
        public override int ReceiveBufferSize
        {
            get
            {
                return base.ReceiveBufferSize;
            }
            set
            {
                base.ReceiveBufferSize = value;

                if ((object)m_udpClient != null)
                    m_udpClient.SetReceiveBuffer(value);
            }
        }

        /// <summary>
        /// Gets or sets the flag that determines whether the UDP client
        /// should attempt to receive packet info when receiving data
        /// from the socket.
        /// </summary>
        public bool ReceivePacketInfo
        {
            get
            {
                return m_receivePacketInfo;
            }
            set
            {
                m_receivePacketInfo = value;

                if ((object)m_udpClient != null && (object)m_udpClient.Provider != null)
                    m_udpClient.Provider.SetSocketOption(m_udpClient.Provider.AddressFamily == AddressFamily.InterNetworkV6 ? SocketOptionLevel.IPv6 : SocketOptionLevel.IP, SocketOptionName.PacketInformation, value);
            }
        }

        /// <summary>
        /// Gets the descriptive status of the client.
        /// </summary>
        public override string Status
        {
            get
            {
                StringBuilder statusBuilder = new StringBuilder(base.Status);

                if ((object)m_sendQueue != null)
                {
                    statusBuilder.AppendFormat("           Queued payloads: {0}", m_sendQueue.Count);
                    statusBuilder.AppendLine();
                }

                statusBuilder.AppendFormat("     Wait handle pool size: {0}", ReusableObjectPool<ManualResetEventSlim>.Default.GetPoolSize());
                statusBuilder.AppendLine();
                statusBuilder.AppendFormat("         Payload pool size: {0}", ReusableObjectPool<UdpClientPayload>.Default.GetPoolSize());
                statusBuilder.AppendLine();

                return statusBuilder.ToString();
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Reads a number of bytes from the current received data buffer and writes those bytes into a byte array at the specified offset.
        /// </summary>
        /// <param name="buffer">Destination buffer used to hold copied bytes.</param>
        /// <param name="startIndex">0-based starting index into destination <paramref name="buffer"/> to begin writing data.</param>
        /// <param name="length">The number of bytes to read from current received data buffer and write into <paramref name="buffer"/>.</param>
        /// <returns>The number of bytes read.</returns>
        /// <remarks>
        /// This function should only be called from within the <see cref="ClientBase.ReceiveData"/> event handler. Calling this method outside
        /// this event will have unexpected results.
        /// </remarks>
        /// <exception cref="InvalidOperationException">No received data buffer has been defined to read.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> or <paramref name="length"/> is less than 0 -or- 
        /// <paramref name="startIndex"/> and <paramref name="length"/> will exceed <paramref name="buffer"/> length.
        /// </exception>
        public override int Read(byte[] buffer, int startIndex, int length)
        {
            buffer.ValidateParameters(startIndex, length);

            if ((object)m_udpClient.ReceiveBuffer != null)
            {
                int sourceLength = m_udpClient.BytesReceived - ReadIndex;
                int readBytes = length > sourceLength ? sourceLength : length;
                Buffer.BlockCopy(m_udpClient.ReceiveBuffer, ReadIndex, buffer, startIndex, readBytes);

                // Update read index for next call
                ReadIndex += readBytes;

                if (ReadIndex >= m_udpClient.BytesReceived)
                    ReadIndex = 0;

                return readBytes;
            }

            throw new InvalidOperationException("No received data buffer has been defined to read.");
        }

        /// <summary>
        /// Saves <see cref="TcpServer"/> settings to the config file if the <see cref="ServerBase.PersistSettings"/> property is set to true.
        /// </summary>
        public override void SaveSettings()
        {
            base.SaveSettings();
            if (PersistSettings)
            {
                // Save settings under the specified category.
                ConfigurationFile config = ConfigurationFile.Current;
                CategorizedSettingsElementCollection settings = config.Settings[SettingsCategory];
                settings["AllowDualStackSocket", true].Update(m_allowDualStackSocket);
                settings["MaxSendQueueSize", true].Update(m_maxSendQueueSize);
                config.Save();
            }
        }

        /// <summary>
        /// Loads saved <see cref="TcpServer"/> settings from the config file if the <see cref="ServerBase.PersistSettings"/> property is set to true.
        /// </summary>
        public override void LoadSettings()
        {
            int maxSendQueueSize;

            base.LoadSettings();
            if (PersistSettings)
            {
                // Load settings from the specified category.
                ConfigurationFile config = ConfigurationFile.Current;
                CategorizedSettingsElementCollection settings = config.Settings[SettingsCategory];
                settings.Add("AllowDualStackSocket", m_allowDualStackSocket, "True if dual-mode socket is allowed when IP address is IPv6, otherwise False.");
                settings.Add("MaxSendQueueSize", m_maxSendQueueSize, "The maximum size of the send queue before payloads are dumped from the queue.");
                AllowDualStackSocket = settings["AllowDualStackSocket"].ValueAs(m_allowDualStackSocket);
                MaxSendQueueSize = settings["MaxSendQueueSize"].ValueAs(m_maxSendQueueSize);

                // Overwrite config file if max send queue size exists in connection string.
                if (m_connectData.ContainsKey("maxSendQueueSize") && int.TryParse(m_connectData["maxSendQueueSize"], out maxSendQueueSize))
                    m_maxSendQueueSize = maxSendQueueSize;
            }
        }

        /// <summary>
        /// Disconnects the <see cref="UdpClient"/> from the connected server synchronously.
        /// </summary>
        public override void Disconnect()
        {
            if (CurrentState != ClientState.Disconnected)
            {
                try
                {
                    if ((object)m_udpServer != null && (object)m_udpClient.Provider != null)
                    {
                        // If the IP specified for the server is a multicast IP, unsubscribe from the specified multicast group.
                        IPEndPoint serverEndpoint = (IPEndPoint)m_udpServer;

                        if (Transport.IsMulticastIP(serverEndpoint.Address))
                            DropMulticastMembership(serverEndpoint.Address, null, m_udpClient.MulticastMembershipAddresses);
                    }
                }
                catch (Exception ex)
                {
                    OnSendDataException(new InvalidOperationException(string.Format("Failed to drop multicast membership: {0}", ex.Message), ex));
                }

                m_udpClient.Reset();

                if ((object)m_connectionThread != null)
                    m_connectionThread.Abort();

                base.Disconnect();
            }
        }

        /// <summary>
        /// Connects the <see cref="UdpClient"/> to the server asynchronously.
        /// </summary>
        /// <exception cref="FormatException">Server property in <see cref="ClientBase.ConnectionString"/> is invalid.</exception>
        /// <exception cref="InvalidOperationException">Attempt is made to connect the <see cref="UdpClient"/> when it is not disconnected.</exception>
        /// <returns><see cref="WaitHandle"/> for the asynchronous operation.</returns>
        public override WaitHandle ConnectAsync()
        {
            m_connectionHandle = (ManualResetEvent)base.ConnectAsync();

            m_udpClient = new TransportProvider<Socket>();
            m_udpClient.SetReceiveBuffer(ReceiveBufferSize);

            // Create a server endpoint.
            if (m_connectData.ContainsKey("server"))
            {
                // Client has a server endpoint specified.
                Match endpoint = Regex.Match(m_connectData["server"], Transport.EndpointFormatRegex);

                if (endpoint != Match.Empty)
                    m_udpServer = Transport.CreateEndPoint(endpoint.Groups["host"].Value, int.Parse(endpoint.Groups["port"].Value), m_ipStack);
                else
                    throw new FormatException(string.Format("Server property in ConnectionString is invalid (Example: {0})", DefaultConnectionString));
            }
            else
            {
                // Create a random server endpoint since one is not specified.
                m_udpServer = Transport.CreateEndPoint(m_connectData["interface"], 0, m_ipStack);
            }

#if ThreadTracking
            m_connectionThread = new ManagedThread(OpenPort);
            m_connectionThread.Name = "GSF.Communication.UdpClient.OpenPort()";
#else
            m_connectionThread = new Thread(OpenPort);
#endif
            m_connectionThread.Start();

            return m_connectionHandle;
        }

        /// <summary>
        /// Adds a multicast membership to the UDP socket.
        /// </summary>
        /// <param name="serverAddress">Multicast address to join.</param>
        /// <param name="sourceAddress">Address which defines the source of the data or null if the membership is not source-specific.</param>
        public void AddMulticastMembership(IPAddress serverAddress, IPAddress sourceAddress)
        {
            byte[] multicastMembershipAddresses;
            AddMulticastMembership(serverAddress, sourceAddress, out multicastMembershipAddresses);
        }

        /// <summary>
        /// Drops a multicast membership from the UDP socket.
        /// </summary>
        /// <param name="serverAddress">Multicast address to drop.</param>
        /// <param name="sourceAddress">Address which defines the source of the data or null if the membership is not source-specific.</param>
        public void DropMulticastMembership(IPAddress serverAddress, IPAddress sourceAddress)
        {
            DropMulticastMembership(serverAddress, sourceAddress, null);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="UdpClient"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                try
                {
                    // This will be done regardless of whether the object is finalized or disposed.
                    if (disposing)
                    {
                        // This will be done only when the object is disposed by calling Dispose().
                        if ((object)m_connectionHandle != null)
                        {
                            m_connectionHandle.Dispose();
                            m_connectionHandle = null;
                        }

                        if ((object)m_sendArgs != null)
                        {
                            m_sendArgs.Dispose();
                            m_sendArgs = null;
                        }

                        if ((object)m_receiveArgs != null)
                        {
                            m_receiveArgs.Dispose();
                            m_receiveArgs = null;
                        }
                    }
                }
                finally
                {
                    m_disposed = true;          // Prevent duplicate dispose.
                    base.Dispose(disposing);    // Call base class Dispose().
                }
            }
        }

        /// <summary>
        /// Validates the specified <paramref name="connectionString"/>.
        /// </summary>
        /// <param name="connectionString">Connection string to be validated.</param>
        /// <exception cref="ArgumentException">Port property is missing.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Port property value is not between <see cref="Transport.PortRangeLow"/> and <see cref="Transport.PortRangeHigh"/>.</exception>
        protected override void ValidateConnectionString(string connectionString)
        {
            m_connectData = connectionString.ParseKeyValuePairs();

            // Derive desired IP stack based on specified "interface" setting, adding setting if it's not defined
            m_ipStack = Transport.GetInterfaceIPStack(m_connectData);

            // Backwards compatibility adjustments.
            // New Format: Server=localhost:8888; Port=8989
            // Old Format: Server=localhost; RemotePort=8888; LocalPort=8888
            if (m_connectData.ContainsKey("localport") && !m_connectData.ContainsKey("port"))
                m_connectData.Add("port", m_connectData["localport"]);

            if (m_connectData.ContainsKey("server") && m_connectData.ContainsKey("remoteport"))
                m_connectData["server"] = string.Format("{0}:{1}", m_connectData["server"], m_connectData["remoteport"]);

            // Check if 'port' property is missing.
            if (!m_connectData.ContainsKey("port"))
                throw new ArgumentException(string.Format("Port property is missing (Example: {0})", DefaultConnectionString));

            // Check if 'port' property is valid.
            if (!Transport.IsPortNumberValid(m_connectData["port"]) && int.Parse(m_connectData["port"]) != -1)
                throw new ArgumentOutOfRangeException("connectionString", string.Format("Port number must be {0} or between {1} and {2}", -1, Transport.PortRangeLow, Transport.PortRangeHigh));
        }

        /// <summary>
        /// Connects to the <see cref="UdpClient"/>.
        /// </summary>
        private void OpenPort()
        {
            int connectionAttempts = 0;

            while (MaxConnectionAttempts == -1 || connectionAttempts < MaxConnectionAttempts)
            {
                try
                {
                    OnConnectionAttempt();

                    // Disable SocketError.ConnectionReset exception from being thrown when the enpoint is not listening.
                    m_udpClient.Provider = Transport.CreateSocket(m_connectData["interface"], int.Parse(m_connectData["port"]), ProtocolType.Udp, m_ipStack, m_allowDualStackSocket);
                    m_udpClient.Provider.IOControl(SIO_UDP_CONNRESET, new byte[] { Convert.ToByte(false) }, null);

                    // If the IP specified for the server is a multicast IP, subscribe to the specified multicast group.
                    IPEndPoint serverEndpoint = (IPEndPoint)m_udpServer;

                    if (Transport.IsMulticastIP(serverEndpoint.Address))
                    {
                        string multicastSource;
                        byte[] multicastMembershipAddresses;
                        IPAddress sourceAddress = null;

                        if (m_connectData.TryGetValue("multicastSource", out multicastSource))
                            sourceAddress = IPAddress.Parse(multicastSource);

                        AddMulticastMembership(serverEndpoint.Address, sourceAddress, out multicastMembershipAddresses);
                        m_udpClient.MulticastMembershipAddresses = multicastMembershipAddresses;
                    }

                    // If the client requires packet info when
                    // receiving data, set the socket option now.
                    if (m_receivePacketInfo)
                    {
                        m_udpClient.Provider.SetSocketOption(serverEndpoint.AddressFamily == AddressFamily.InterNetworkV6 ? SocketOptionLevel.IPv6 : SocketOptionLevel.IP, SocketOptionName.PacketInformation, true);
                    }

                    // Listen for data to send.
                    using (SocketAsyncEventArgs sendArgs = m_sendArgs)
                    {
                        m_sendArgs = FastObjectFactory<SocketAsyncEventArgs>.CreateObjectFunction();
                    }

                    m_udpClient.SetSendBuffer(SendBufferSize);
                    m_sendArgs.SetBuffer(m_udpClient.SendBuffer, 0, m_udpClient.SendBufferSize);
                    m_sendArgs.Completed += m_sendHandler;

                    m_connectionHandle.Set();
                    OnConnectionEstablished();

                    // Listen for incoming data only if endpoint is bound to a local interface.
                    if (m_udpClient.Provider.LocalEndPoint != null)
                    {
                        using (SocketAsyncEventArgs receiveArgs = m_receiveArgs)
                        {
                            m_receiveArgs = FastObjectFactory<SocketAsyncEventArgs>.CreateObjectFunction();
                        }

                        m_receiveArgs.Completed += m_receiveHandler;
                        ReceivePayloadAsync();
                    }
                    break;
                }
                catch (ThreadAbortException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    connectionAttempts++;
                    OnConnectionException(ex);
                }
            }
        }

        /// <summary>
        /// Sends data to the server asynchronously.
        /// </summary>
        /// <param name="data">The buffer that contains the binary data to be sent.</param>
        /// <param name="offset">The zero-based position in the <paramref name="data"/> at which to begin sending data.</param>
        /// <param name="length">The number of bytes to be sent from <paramref name="data"/> starting at the <paramref name="offset"/>.</param>
        /// <returns><see cref="WaitHandle"/> for the asynchronous operation.</returns>
        protected override WaitHandle SendDataAsync(byte[] data, int offset, int length)
        {
            return SendDataToAsync(data, offset, length, m_udpServer);
        }

        /// <summary>
        /// Sends data to the server asynchronously.
        /// </summary>
        /// <param name="data">The buffer that contains the binary data to be sent.</param>
        /// <param name="offset">The zero-based position in the <paramref name="data"/> at which to begin sending data.</param>
        /// <param name="length">The number of bytes to be sent from <paramref name="data"/> starting at the <paramref name="offset"/>.</param>
        /// <param name="destination">The end point which identifies the destination for the data.</param>
        /// <returns><see cref="WaitHandle"/> for the asynchronous operation.</returns>
        public WaitHandle SendDataToAsync(byte[] data, int offset, int length, EndPoint destination)
        {
            UdpClientPayload payload;
            UdpClientPayload dequeuedPayload;
            ManualResetEventSlim handle;
            bool lockTaken = false;

            // Check to see if the client has reached the maximum send queue size.
            if (m_maxSendQueueSize > 0 && m_sendQueue.Count >= m_maxSendQueueSize)
            {
                for (int i = 0; i < m_maxSendQueueSize; i++)
                {
                    if (m_sendQueue.TryDequeue(out payload))
                    {
                        payload.WaitHandle.Set();
                        payload.WaitHandle.Dispose();
                        payload.WaitHandle = null;
                    }
                }

                throw new InvalidOperationException(string.Format("UDP client reached maximum send queue size. {0} payloads dumped from the queue.", m_maxSendQueueSize));
            }

            // Create payload and wait handle.
            payload = ReusableObjectPool<UdpClientPayload>.Default.TakeObject();
            handle = ReusableObjectPool<ManualResetEventSlim>.Default.TakeObject();

            payload.Destination = destination;
            payload.Data = data;
            payload.Offset = offset;
            payload.Length = length;
            payload.WaitHandle = handle;
            handle.Reset();

            // Queue payload for sending.
            m_sendQueue.Enqueue(payload);

            try
            {
                m_sendLock.Enter(ref lockTaken);

                // Send the next queued payload.
                if (Interlocked.CompareExchange(ref m_sending, 1, 0) == 0)
                {
                    if (m_sendQueue.TryDequeue(out dequeuedPayload))
                        ThreadPool.QueueUserWorkItem(state => SendPayload((UdpClientPayload)state), dequeuedPayload);
                    else
                        Interlocked.Exchange(ref m_sending, 0);
                }
            }
            finally
            {
                if (lockTaken)
                    m_sendLock.Exit();
            }

            // Notify that the send operation has started.
            OnSendDataStart();

            // Return the async handle that can be used to wait for the async operation to complete.
            return handle.WaitHandle;
        }

        /// <summary>
        /// Raises the <see cref="ClientBase.SendDataException"/> event.
        /// </summary>
        /// <param name="ex">Exception to send to <see cref="ClientBase.SendDataException"/> event.</param>
        protected override void OnSendDataException(Exception ex)
        {
            if (CurrentState != ClientState.Disconnected)
                base.OnSendDataException(ex);
        }

        /// <summary>
        /// Raises the <see cref="ClientBase.ReceiveDataException"/> event.
        /// </summary>
        /// <param name="ex">Exception to send to <see cref="ClientBase.ReceiveDataException"/> event.</param>
        protected override void OnReceiveDataException(Exception ex)
        {
            if (CurrentState != ClientState.Disconnected)
                base.OnReceiveDataException(ex);
        }

        /// <summary>
        /// Raises the <see cref="ClientBase.ConnectionTerminated"/> event.
        /// </summary>
        protected override void OnConnectionTerminated()
        {
            if (CurrentState != ClientState.Disconnected)
                base.OnConnectionTerminated();
        }

        /// <summary>
        /// Sends a payload on the socket.
        /// </summary>
        private void SendPayload(UdpClientPayload payload)
        {
            int copyLength;

            try
            {
                // Set the user token of the socket args.
                m_sendArgs.UserToken = payload;

                // Copy payload into send buffer.
                copyLength = Math.Min(payload.Length, m_udpClient.SendBufferSize);
                Buffer.BlockCopy(payload.Data, payload.Offset, m_udpClient.SendBuffer, 0, copyLength);

                // Set buffer and end point of send args.
                m_sendArgs.SetBuffer(0, copyLength);
                m_sendArgs.RemoteEndPoint = payload.Destination;

                // Update payload offset and length.
                payload.Offset += copyLength;
                payload.Length -= copyLength;

                // Send data over socket.
                if (!m_udpClient.Provider.SendToAsync(m_sendArgs))
                    ProcessSend();
            }
            catch (Exception ex)
            {
                OnSendDataException(ex);

                // Assume process send was not able
                // to continue the asynchronous loop.
                Interlocked.Exchange(ref m_sending, 0);
            }
        }

        /// <summary>
        /// Callback method for asynchronous send operation.
        /// </summary>
        private void ProcessSend()
        {
            UdpClientPayload payload = null;
            ManualResetEventSlim handle = null;
            bool lockTaken = false;

            try
            {
                // Get the payload and its wait handle.
                payload = (UdpClientPayload)m_sendArgs.UserToken;
                handle = payload.WaitHandle;

                // Determine whether we are finished with this
                // payload and, if so, set the wait handle.
                if (payload.Length <= 0)
                    handle.Set();

                // Check for errors during send operation.
                if (m_sendArgs.SocketError != SocketError.Success)
                    throw new SocketException((int)m_sendArgs.SocketError);

                // Update statistics.
                m_udpClient.Statistics.UpdateBytesSent(m_sendArgs.BytesTransferred);

                // Send operation is complete.
                if (payload.Length <= 0)
                    OnSendDataComplete();
            }
            catch (Exception ex)
            {
                // Send operation failed to complete.
                OnSendDataException(ex);
            }
            finally
            {
                try
                {
                    if (payload.Length > 0)
                    {
                        // Still more to send for this payload.
                        ThreadPool.QueueUserWorkItem(state => SendPayload((UdpClientPayload)state), payload);
                    }
                    else
                    {
                        payload.WaitHandle = null;

                        // Return payload and wait handle to their respective object pools.
                        ReusableObjectPool<UdpClientPayload>.Default.ReturnObject(payload);
                        ReusableObjectPool<ManualResetEventSlim>.Default.ReturnObject(handle);

                        // Begin sending next client payload.
                        if (m_sendQueue.TryDequeue(out payload))
                        {
                            ThreadPool.QueueUserWorkItem(state => SendPayload((UdpClientPayload)state), payload);
                        }
                        else
                        {
                            try
                            {
                                m_sendLock.Enter(ref lockTaken);

                                if (m_sendQueue.TryDequeue(out payload))
                                    ThreadPool.QueueUserWorkItem(state => SendPayload((UdpClientPayload)state), payload);
                                else
                                    Interlocked.Exchange(ref m_sending, 0);
                            }
                            finally
                            {
                                if (lockTaken)
                                    m_sendLock.Exit();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessage = string.Format("Exception encountered while attempting to send next payload: {0}", ex.Message);
                    OnSendDataException(new Exception(errorMessage, ex));
                    Interlocked.Exchange(ref m_sending, 0);
                }
            }
        }

        /// <summary>
        /// Initiate method for asynchronous receive operation of payload data.
        /// </summary>
        private void ReceivePayloadAsync()
        {
            // Set up event args for receive operation.
            m_receiveArgs.SetBuffer(m_udpClient.ReceiveBuffer, 0, m_udpClient.ReceiveBufferSize);
            m_receiveArgs.RemoteEndPoint = m_udpServer;

            if (!m_receivePacketInfo)
            {
                if (!m_udpClient.Provider.ReceiveFromAsync(m_receiveArgs))
                    ThreadPool.QueueUserWorkItem(state => ProcessReceive());
            }
            else
            {
                if (!m_udpClient.Provider.ReceiveMessageFromAsync(m_receiveArgs))
                    ThreadPool.QueueUserWorkItem(state => ProcessReceive());
            }
        }

        /// <summary>
        /// Callback method for asynchronous receive operation of payload data.
        /// </summary>
        private void ProcessReceive()
        {
            try
            {
                if (m_receiveArgs.SocketError != SocketError.Success)
                    throw new SocketException((int)m_receiveArgs.SocketError);

                // Update statistics and pointers.
                m_udpClient.Statistics.UpdateBytesReceived(m_receiveArgs.BytesTransferred);
                m_udpClient.BytesReceived = m_udpClient.Statistics.LastBytesReceived;

                // Notify of received data and resume receive operation.
                OnReceive(m_receiveArgs.RemoteEndPoint, m_receiveArgs.ReceiveMessageFromPacketInfo, m_udpClient.ReceiveBuffer, m_udpClient.BytesReceived);
                ReceivePayloadAsync();
            }
            catch (ObjectDisposedException)
            {
                // Make sure connection is terminated when client is disposed.
                TerminateConnection(true);
            }
            catch (SocketException ex)
            {
                try
                {
                    // Notify and resume receive.
                    OnReceiveDataException(ex);
                    ReceivePayloadAsync();
                }
                catch
                {
                    // Terminate connection if resuming receiving fails.
                    TerminateConnection(true);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    // For any other exception, notify and resume receive.
                    OnReceiveDataException(ex);
                    ReceivePayloadAsync();
                }
                catch
                {
                    // Terminate connection if resuming receiving fails.
                    TerminateConnection(true);
                }
            }
        }

        private void AddMulticastMembership(IPAddress serverAddress, IPAddress sourceAddress, out byte[] multicastMembershipAddresses)
        {
            multicastMembershipAddresses = null;

            try
            {
                if (!Transport.IsMulticastIP(serverAddress))
                    throw new InvalidOperationException("Cannot add multicast membership if server address is not a multicast IP.");

                if ((object)sourceAddress == null)
                {
                    // Execute multicast subscribe for any source
                    m_udpClient.Provider.SetSocketOption(serverAddress.AddressFamily == AddressFamily.InterNetworkV6 ? SocketOptionLevel.IPv6 : SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(serverAddress));
                }
                else
                {
                    IPAddress localAddress = ((IPEndPoint)m_udpClient.Provider.LocalEndPoint).Address;

                    if (sourceAddress.AddressFamily != serverAddress.AddressFamily)
                        throw new InvalidOperationException(string.Format("Source address \"{0}\" is not in the same IP format as server address \"{1}\"", sourceAddress, serverAddress));

                    if (localAddress.AddressFamily != serverAddress.AddressFamily)
                        throw new InvalidOperationException(string.Format("Local address \"{0}\" is not in the same IP format as server address \"{1}\"", localAddress, serverAddress));

                    MemoryStream membershipAddresses = new MemoryStream();

                    byte[] serverAddressBytes = serverAddress.GetAddressBytes();
                    byte[] sourceAddressBytes = sourceAddress.GetAddressBytes();
                    byte[] localAddressBytes = localAddress.GetAddressBytes();

                    membershipAddresses.Write(serverAddressBytes, 0, serverAddressBytes.Length);
                    membershipAddresses.Write(sourceAddressBytes, 0, sourceAddressBytes.Length);
                    membershipAddresses.Write(localAddressBytes, 0, localAddressBytes.Length);

                    multicastMembershipAddresses = membershipAddresses.ToArray();

                    // Execute multicast subscribe for specific source
                    m_udpClient.Provider.SetSocketOption(serverAddress.AddressFamily == AddressFamily.InterNetworkV6 ? SocketOptionLevel.IPv6 : SocketOptionLevel.IP, SocketOptionName.AddSourceMembership, multicastMembershipAddresses);
                }
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode != SocketError.InvalidArgument)
                    throw;
            }
        }

        private void DropMulticastMembership(IPAddress serverAddress, IPAddress sourceAddress, byte[] multicastMembershipAddresses)
        {
            try
            {
                if (!Transport.IsMulticastIP(serverAddress))
                    throw new InvalidOperationException("Cannot drop multicast membership if server address is not a multicast IP.");

                if ((object)sourceAddress == null && (object)multicastMembershipAddresses == null)
                {
                    // Execute multicast unsubscribe for any source
                    m_udpClient.Provider.SetSocketOption(serverAddress.AddressFamily == AddressFamily.InterNetworkV6 ? SocketOptionLevel.IPv6 : SocketOptionLevel.IP, SocketOptionName.DropMembership, new MulticastOption(serverAddress));
                }
                else
                {
                    if ((object)multicastMembershipAddresses == null)
                    {
                        IPAddress localAddress = ((IPEndPoint)m_udpClient.Provider.LocalEndPoint).Address;

                        if (sourceAddress.AddressFamily != serverAddress.AddressFamily)
                            throw new InvalidOperationException(string.Format("Source address \"{0}\" is not in the same IP format as server address \"{1}\"", sourceAddress, serverAddress));

                        if (localAddress.AddressFamily != serverAddress.AddressFamily)
                            throw new InvalidOperationException(string.Format("Local address \"{0}\" is not in the same IP format as server address \"{1}\"", localAddress, serverAddress));

                        MemoryStream membershipAddresses = new MemoryStream();
                        byte[] serverAddressBytes = serverAddress.GetAddressBytes();
                        byte[] sourceAddressBytes = sourceAddress.GetAddressBytes();
                        byte[] localAddressBytes = localAddress.GetAddressBytes();

                        membershipAddresses.Write(serverAddressBytes, 0, serverAddressBytes.Length);
                        membershipAddresses.Write(sourceAddressBytes, 0, sourceAddressBytes.Length);
                        membershipAddresses.Write(localAddressBytes, 0, localAddressBytes.Length);

                        multicastMembershipAddresses = membershipAddresses.ToArray();
                    }

                    // Execute multicast unsubscribe for specific source
                    m_udpClient.Provider.SetSocketOption(serverAddress.AddressFamily == AddressFamily.InterNetworkV6 ? SocketOptionLevel.IPv6 : SocketOptionLevel.IP, SocketOptionName.DropSourceMembership, multicastMembershipAddresses);
                }
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode != SocketError.InvalidArgument)
                    throw;
            }
        }

        /// <summary>
        /// Processes the termination of client.
        /// </summary>
        private void TerminateConnection(bool raiseEvent)
        {
            m_udpClient.Reset();

            if (raiseEvent)
                OnConnectionTerminated();
        }

        /// <summary>
        /// Calls all the receive handlers in sequence.
        /// </summary>
        private void OnReceive(EndPoint remoteEndPoint, IPPacketInformation packetInformation, byte[] data, int size)
        {
            OnReceiveDataFrom(remoteEndPoint, packetInformation, size);
            OnReceiveDataFromComplete(remoteEndPoint, packetInformation, data, size);
            OnReceiveDataComplete(data, size);
        }

        /// <summary>
        /// Raises the <see cref="ReceiveDataFrom"/> event.
        /// </summary>
        /// <param name="remoteEndPoint">End-point from which data has been received.</param>
        /// <param name="size">Number of bytes received from the client.</param>
        private void OnReceiveDataFrom(EndPoint remoteEndPoint, IPPacketInformation packetInformation, int size)
        {
            try
            {
                if ((object)ReceiveDataFrom != null)
                    ReceiveDataFrom(this, new EventArgs<EndPoint, IPPacketInformation, int>(remoteEndPoint, packetInformation, size));
            }
            catch (Exception userException)
            {
                OnUnhandledUserException(userException);
            }
        }

        /// <summary>
        /// Raises the <see cref="ReceiveDataFrom"/> event.
        /// </summary>
        /// <param name="remoteEndPoint">End-point from which data has been received.</param>
        /// <param name="data">Data received from the client.</param>
        /// <param name="size">Number of bytes received from the client.</param>
        private void OnReceiveDataFromComplete(EndPoint remoteEndPoint, IPPacketInformation packetInformation, byte[] data, int size)
        {
            try
            {
                if ((object)ReceiveDataFromComplete != null)
                    ReceiveDataFromComplete(this, new EventArgs<EndPoint, IPPacketInformation, byte[], int>(remoteEndPoint, packetInformation, data, size));
            }
            catch (Exception userException)
            {
                OnUnhandledUserException(userException);
            }
        }

        #endregion
    }
}