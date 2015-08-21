//*******************************************************************************************************
//  Transport.cs
//  Copyright � 2008 - TVA, all rights reserved - Gbtc
//
//  Build Environment: C#, Visual Studio 2008
//  Primary Developer: Pinal C. Patel, Operations Data Architecture [TVA]
//      Office: PSO TRAN & REL, CHATTANOOGA - MR BK-C
//       Phone: 423/751-3024
//       Email: pcpatel@tva.gov
//
//  Code Modification History:
//  -----------------------------------------------------------------------------------------------------
//  06/01/2006 - Pinal C. Patel
//       Original version of source created.
//  09/29/2008 - James R Carroll
//       Converted to C#.
//
//*******************************************************************************************************

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using TVA.Security.Cryptography;
using TVA.IO.Compression;

namespace TVA.Communication
{
	internal static class Transport
	{	
		/// <summary>
		/// Gets an IP endpoint for the specified host name and port number.
		/// </summary>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <param name="port">The port number to be associated with the address.</param>
		/// <returns>IP endpoint for the specified host name and port number.</returns>
		public static IPEndPoint GetIpEndPoint(string hostNameOrAddress, int port)
		{
			try
			{
				return new IPEndPoint(Dns.GetHostEntry(hostNameOrAddress).AddressList[0], port);
			}
			catch (SocketException)
			{
				// SocketException will be thrown if the host is not found, so we'll try manual IP
				return new IPEndPoint(IPAddress.Parse(hostNameOrAddress), port);
			}
		}
		
		/// <summary>
		/// Determines whether the specified port is valid.
		/// </summary>
		/// <param name="port">The port number to be validated.</param>
		/// <returns>True if the port number is valid.</returns>
		public static bool IsValidPortNumber(string port)
		{
			int portNumber;

			if (int.TryParse(port, out portNumber))
			{
				// The specified port is a valid integer value.
                if (portNumber >= 0 && portNumber <= 65535)
                {
                    // The port number is within the valid range.
                    return true;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Port", "Port number must be between 0 and 65535.");
                }
			}
			else
			{
				throw new ArgumentException("Port number is not a valid number.");
			}
		}
		
		public static byte[] CompressData(byte[] data, int offset, int length, CompressionStrength compressionLevel)
		{
            // Using streaming compression since needed uncompressed size will be serialized into destination stream
		    return new MemoryStream(data, offset, length).Compress(compressionLevel).ToArray();
		}

        public static byte[] DecompressData(byte[] data, CompressionStrength compressionLevel)
		{
            // Using streaming decompression since needed uncompressed size was serialized into source stream
            return new MemoryStream(data).Decompress().ToArray();
		}

        public static byte[] EncryptData(byte[] data, int offset, int length, string encryptionKey, CipherStrength encryptionLevel)
		{
			byte[] key = Encoding.ASCII.GetBytes(encryptionKey);
            return data.Encrypt(offset, length, key, key, encryptionLevel);
		}

        public static byte[] DecryptData(byte[] data, string encryptionKey, CipherStrength encryptionLevel)
		{
            byte[] key = Encoding.ASCII.GetBytes(encryptionKey);
            return data.Decrypt(key, key, encryptionLevel);
        }
		
		public static bool IsDestinationReachable(IPEndPoint targetIPEndPoint)
		{
			try
			{
				// We'll check if the target endpoint exist by sending empty data to it and then wait for data from it.
				// If the endpoint doesn't exist then we'll receive a ConnectionReset socket exception.
				EndPoint targetEndPoint = (EndPoint) targetIPEndPoint;
				using (Socket targetChecker = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
				{
					targetChecker.ReceiveTimeout = 1;
					targetChecker.SendTo(new byte[] {}, targetEndPoint);
					targetChecker.ReceiveFrom(new byte[] {}, ref targetEndPoint);
				}
				
			}
			catch (SocketException ex)
			{
				switch (ex.SocketErrorCode)
				{
					case SocketError.ConnectionReset:
						// This means that the target endpoint is unreachable.
						return false;
				}
			}
			catch
			{
				// We'll ignore any other exceptions we might encounter.
			}
			
			return true;
		}
	}
}