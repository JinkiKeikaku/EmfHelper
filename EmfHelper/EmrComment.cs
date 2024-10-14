using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EmfHelper
{
    public struct EmrComment
    {
        public int DataSize;
        public byte[] CommentData;
        public int CommentTypeIdentifier
            => DataSize >= 4 ? BitConverter.ToInt32(CommentData, 0) : 0;
        public string CommentTypeName{
            get
            {
                string result = "";
                if (DataSize <= 0) return result;
                for (int i = 0; i < 4; i++) result += (char)CommentData[i];
                return result;
            }
        }
        public bool IsEmfPlus
            =>(DataSize >= 4 ? BitConverter.ToUInt32(CommentData, 0) == 0x2B464D45 : false); 
        public bool IsEmfPublic
            =>(DataSize > 4 ? BitConverter.ToUInt32(CommentData, 0) == 0x43494447 : false);
        public bool IsEmfSpool
            =>(DataSize > 4 ? BitConverter.ToUInt32(CommentData, 0) == 0x00000000 : false); 
        public UInt16 EmfPlusPreviewType(int recordStart)
            =>(DataSize >= 6 ? BitConverter.ToUInt16(CommentData, recordStart + 4) : (UInt16)0);
        public UInt16 EmfPlusPreviewFlags(int recordStart)
            =>(DataSize >= 8 ? BitConverter.ToUInt16(CommentData, recordStart + 6) : (UInt16)0);
        public UInt32 EmfPlusPreviewRecordSize(int recordStart)
            =>(DataSize >= 12 ? BitConverter.ToUInt32(CommentData, recordStart + 8) : (UInt32)0);
        public UInt32 EmfPlusPreviewTotalObjectSize(int recordStart)
            =>(DataSize >= 16 ? BitConverter.ToUInt32(CommentData, recordStart + 12) : (UInt32)0);

        public override string ToString()
        {
            return CommentTypeName;
        }
    }
}
