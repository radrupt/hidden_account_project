using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoClient.Service.tools
{
    class DeviceTypecs
    {
        static Hashtable devicetype = new Hashtable(){
                                        {0x01,"HY8004HC-A "},
                                        {0x02,"HY8008HC-A "},
                                        {0x03,"HY8012HC-A "},
                                        {0x04,"HY8016HC-A "},
                                        {0x05,"HY8004HF-A "},
                                        {0x06,"HY8008HF-A "},
                                        {0x07,"HY8004HC-B "},
                                        {0x08,"HY8008HC-C/HY8008HC-D "},
                                        {0x09,"HY8016HC-C/HY8016HC-D "},
                                        {0x0A,"HY8004HT-C/HY8004HF-C "},
                                        {0x0B,"HY8008HT-C/HY8008HF-C "},
                                        {0x0C,"HY8016HT-C/HY8016HF-C"}, 
                                        {0x0D,"HY6001HF-B "},
                                        {0x0E,"HY6002HF-B "},
                                        {0x0F,"HY6004HC-B "},
                                        {0x10,"HY6101HF-B "},
                                        {0x11,"HY6102HF-B "},
                                        {0x12,"HY6104HC-B "},
                                        {0x30,"Mini7104HC-G1 "},
                                        {0x31,"Mini7108HC-G1 "},
                                        {0x36,"Mini7116HC-G1 "},
                                        {0x40,"HY4204HC-F "},
                                        {0x49,"HY4204HC-G "},
                                        {0x42,"HY4208HS-G "},
                                        {0x4A,"HY4208HC-G "},
                                        {0x43,"GK4204HC-G "},
                                        {0x44,"GK4208HC-G "},
                                        {0x45,"SS4204HC-G "},
                                        {0x46,"SS4208HC-G "},
                                        {0x47,"DVS6002F-G "},
                                        {0x48,"HY7116HC-G "},
                                        {0x4B,"HY4204HC-G1 "},
                                        {0x4C,"HY4208HC-G1 "},
                                        {0x4D,"HY7216HC-G1 "},
                                        {0x52,"HY5008HC-G "},
                                        {0x54,"HY5016HC-G "},
                                        {0x55,"GK5016HC-G "},
                                        {0x56,"HY5004HF-G "},
                                        {0x57,"HY5008HF-G "},
                                        {0x58,"HY5016HF-G "},
                                        {0x59,"HY5004HC-G1 "},
                                        {0x5A,"HY5008HC-G1 "},
                                        {0x5B,"HY5016HC-G1 "},
                                        {0x70,"MK标清编码模块 "},
                                        {0x71,"MK高清球机模块 "},
                                        {0x72,"VC-9100IP(百万像素网络摄像机(枪机))"} ,
                                        {0x73,"650TVL IPc "},
                                        {0x74,"720P IPC "},
                                        {0x75,"VGA IPC "},
                                        {0x92,"HY9008HF-G "},
                                        {0x94,"HY9016HF-G "},
                                        {0x95,"HY8004HT-G "},
                                        {0x96,"HY8008HT-G "},
                                        {0x97,"HY8016HT-G "},
                                        {0x99,"HY8008HC-G "},
                                        {0x9A,"HY8016HC-G "},
                                        {0x9B,"HY8008HS-G "},
                                        {0x9C,"HY8016HS-G "},
                                        {0x9D,"HY4216HC-G0 "},
                                        {0x9E,"HY8216HC-G0 "},
                                        {0x9F,"HY8216HT-G0 "}
};


        public static string GetDeviceType(Byte m_bytDevType)
        {
            object bytety = devicetype[(int)(m_bytDevType)];
            if ( bytety != null )
                return bytety.ToString();
            else
                return "无当前设备类型码对应的信息,类型码是: " + m_bytDevType;
        }
    }
}
