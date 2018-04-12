using System;
using System.IO;
using System.Text;


/*******************************************************
 * 
 *      This will handle decoding attachment objects
 *      
 * *****************************************************/

namespace TRCore.HelperClass
{
    /// <summary>
    /// Decodes a file attachment and saves it to a specified path.
    /// </summary>
    public class AttachmentDecoder
    {
        /// <summary>
        /// Retrieve a handle on the logging object for the plugin instance
        /// </summary>

        private const int SP1Header_Size = 20;
        private const int FIXED_HEADER = 16;

        private int fileSize;
        private int attachmentNameLength;
        private string attachmentName;
        private byte[] decodedAttachment;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="theBase64EncodedValue">The Base64 encoded string representing the attachment</param>
        public AttachmentDecoder(string theBase64EncodedValue)
        {
            
            byte[] theData = Convert.FromBase64String(theBase64EncodedValue);
            using (MemoryStream ms = new MemoryStream(theData))
            {
                BinaryReader theReader = new BinaryReader(ms);
                DecodeAttachment(theReader);
            }
        }

        /// <summary>
        /// Decode the attachment
        /// </summary>
        /// <param name="theReader">Binary Reader to read the byte array</param>
        private void DecodeAttachment(BinaryReader theReader)
        {
            //Position the reader to get the file size.
            byte[] headerData = new byte[FIXED_HEADER];
            headerData = theReader.ReadBytes(headerData.Length);

            fileSize = (int)theReader.ReadUInt32();
            attachmentNameLength = (int)theReader.ReadUInt32() * 2;

            byte[] fileNameBytes = theReader.ReadBytes(attachmentNameLength);
            //InfoPath uses UTF8 encoding.
            Encoding enc = Encoding.Unicode;
            attachmentName = enc.GetString(fileNameBytes, 0, attachmentNameLength - 2);
            decodedAttachment = theReader.ReadBytes(fileSize);
        }

        /// <summary>
        /// Saves the attachment
        /// </summary>
        /// <param name="saveLocation">The location to save the attachment to</param>
        public void SaveAttachment(string saveLocation)
        {
            if (saveLocation == null)
            {
                throw new ArgumentNullException("saveLocation");
            }
            else
            {
                FileStream fs = null;
                try
                {
                    string fullFileName = saveLocation;
                    if (!fullFileName.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        fullFileName += Path.DirectorySeparatorChar;
                    }

                    fullFileName += attachmentName;

                    if (File.Exists(fullFileName))
                        File.Delete(fullFileName);

                    fs = new FileStream(fullFileName, FileMode.CreateNew);

                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        bw.Write(decodedAttachment);
                    }
                }
                catch
                {
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// The filename of the attachment
        /// </summary>
        public string FileName
        {
            get { return attachmentName; }
        }

        // CA1819: Properties should not return arrays
        /// <summary>
        /// Retrieve the decoded byte array representing the attachment
        /// </summary>
        /// <returns>Byte array representing the attachment</returns>
        public byte[] GetDecodedAttachment()
        {
            return decodedAttachment;
        }
    }
}